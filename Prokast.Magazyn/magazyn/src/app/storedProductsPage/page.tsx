"use client";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Cookies from 'js-cookie';
//import Navbar from '../Components/Navbar';

interface Product {
  id: number;
  name: string;
  sku: string;
  ean: string;
  description: string;
  additionalDescriptions?: { title: string; value: string; regionID: number }[];
  additionalNames?: { title: string; value: string; regionID: number }[];
}

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState<string>('Wszystko');
  const [selectedPriceRange, setSelectedPriceRange] = useState<string>('Wszystko');
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [newMinQuantity, setNewMinQuantity] = useState('');
  const [editingQuantityProduct, setEditingQuantityProduct] = useState<Product | null>(null);
  const [quantityChange, setQuantityChange] = useState(''); // liczba do dodania/odjęcia
  const [quantityAction, setQuantityAction] = useState<'add' | 'subtract'>('add'); // wybór akcji


  const fetchProducts = async () => {
    try {
      setLoading(true);
      const token = Cookies.get("token");

      if (!token) {
        setError("Brak tokenu autoryzacyjnego.");
        setLoading(false);
        return;
      }

      const warehouseID = prompt("Podaj ID magazynu (warehouseID):");

      if (!warehouseID) {
        setError("Nie podano ID magazynu.");
        setLoading(false);
        return;
      }

      const response = await axios.get(
        "http://localhost:8080/api/storedproducts",
        {
          headers: {
            Authorization: `Bearer ${token}`,
            Accept: "application/json",
          },
          params: {
            warehouseID: warehouseID,
          },
        }
      );

      const model = response.data?.model ?? response.data;

      if (!model || model.length === 0) {
        setError("Brak produktów dla podanego magazynu.");
        return;
      }

      setProducts(
        model.map((item: any) => ({
          id: item.id,
          name: item.productName,
          sku: item.productID,
          description: `Ilość: ${item.quantity}, minimalna ilość: ${item.minQuantity}`,
          ean: item.id.toString(),
        }))
      );

      setError("");
    } catch (err: any) {
      console.error("Błąd API:", err.response?.data ?? err);
      setError("Nie udało się pobrać listy produktów.");
    } finally {
      setLoading(false);
    }
  };


  useEffect(() => {
    fetchProducts();
  }, []);

  const getQuantityColor = (description: string) => {
    const quantityMatch = description.match(/Ilość: (\d+)/);
    const minQuantityMatch = description.match(/minimalna ilość: (\d+)/);

    if (!quantityMatch || !minQuantityMatch) return 'text-gray-600';

    const quantity = parseInt(quantityMatch[1], 10);
    const minQuantity = parseInt(minQuantityMatch[1], 10);

    return quantity < minQuantity ? 'text-red-600 font-bold' : 'text-gray-600';
  };

  const handleEditQuantityProduct = (product: Product) => {
    setEditingQuantityProduct(product);
    setQuantityChange('');       // czyścimy poprzednią wartość
    setQuantityAction('add');    // domyślnie Dodaj
  };

  const handleConfirmQuantityChange = async () => {
    if (!editingQuantityProduct) return;

    const token = Cookies.get("token");
    if (!token) {
      alert("Brak tokenu autoryzacyjnego.");
      return;
    }

    let amount = parseInt(quantityChange, 10);
    if (isNaN(amount)) {
      alert("Podaj poprawną liczbę całkowitą.");
      return;
    }
    if (isNaN(amount) || amount < 0) {
      alert("Podaj liczbę całkowitą większą lub równą zero.");
      return;
    }

    // Jeśli wybrano odejmowanie, zmieniamy znak na ujemny
    if (quantityAction === 'subtract') {
      amount = -amount;
    }


    try {
      await axios.put(
        `http://localhost:8080/api/storedproducts/quantity/${editingQuantityProduct.id}?quantity=${amount}`,
        {},
        {
          headers: { Authorization: `Bearer ${token}` }
        }
      );

      // Aktualizacja lokalna (tylko dla UI, jeśli chcesz)
      setProducts((prev) =>
        prev.map((p) =>
          p.id === editingQuantityProduct.id
            ? {
              ...p,
              description: p.description.replace(
                /Ilość: \d+/,
                `Ilość: ${parseInt(p.description.match(/Ilość: (\d+)/)?.[1] || '0') + amount}`
              ),
            }
            : p
        )
      );

      alert(`Zaktualizowano ilość produktu "${editingQuantityProduct.name}".`);
      setEditingQuantityProduct(null);
      setQuantityChange('');
      setQuantityAction('add');
    } catch (err: any) {
      console.error("Błąd podczas zmiany ilości:", err.response?.data ?? err);
      alert("Nie udało się zaktualizować ilości produktu.");
    }
  };

  const handleEditMinQuantityProduct = (product: Product) => {
    setEditingProduct(product);

    // Wyciągnięcie aktualnej minimalnej ilości z opisu tekstowego
    const match = product.description.match(/minimalna ilość: (\d+)/);
    const currentMin = match ? match[1] : "";

    setNewMinQuantity(currentMin);
  };

  const handleConfirmEdit = async () => {
    if (!editingProduct) return;

    const token = Cookies.get("token");
    if (!token) {
      alert("Brak tokenu autoryzacyjnego. Zaloguj się ponownie.");
      return;
    }

    const minQ = parseInt(newMinQuantity, 10);

    if (isNaN(minQ)) {
      alert("Podaj poprawną wartość minimalnej ilości (liczbę całkowitą).");
      return;
    }

    if (minQ < 0) {
      alert("Minimalna ilość nie może być mniejsza niż 0.");
      return;
    }

    try {
      // Wysyłamy PUT z minQuantity w query string
      await axios.put(
        `http://localhost:8080/api/storedproducts/minquantity/${editingProduct.id}?minQuantity=${minQ}`,
        {}, // body można zostawić puste
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      // Aktualizacja lokalnego stanu produktu
      setProducts((prev) =>
        prev.map((p) =>
          p.id === editingProduct.id
            ? {
              ...p,
              description: p.description.replace(
                /minimalna ilość: \d+/,
                `minimalna ilość: ${minQ}`
              ),
            }
            : p
        )
      );

      alert(`Zaktualizowano minimalną ilość produktu "${editingProduct.name}".`);
      setEditingProduct(null);
      setNewMinQuantity('');
    } catch (err: any) {
      console.error("Błąd podczas edycji produktu:", err.response?.data ?? err);
      alert("Nie udało się zaktualizować minimalnej ilości produktu.");
    }
  };

  const handleDeleteProduct = async (product: Product) => {
    const confirmed = window.confirm(`Czy na pewno chcesz usunąć produkt "${product.name}" z tego magazynu?`);
    if (!confirmed) return;

    try {
      const token = Cookies.get("token");
      if (!token) {
        setError("Brak tokenu autoryzacyjnego.");
        return;
      }

      // DELETE http://localhost:8080/api/products/{productID}
      await axios.delete(`http://localhost:8080/api/storedproducts/${product.id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
      });

      setProducts((prev) => prev.filter((p) => p.id !== product.id));

      alert(`Produkt "${product.name}" został usunięty.`);
    } catch (err: any) {
      console.error("Błąd podczas usuwania produktu:", err.response?.data ?? err);
      setError("Nie udało się usunąć produktu.");
    }
  };

  const handleAddToCart = (product: Product) => {
    alert(`Dodano produkt "${product.name}" do koszyka.`);
  };

  const handleViewDetails = (product: Product) => {
    alert(`Otwieranie szczegółów produktu: ${product.name}`);
  };

  const filteredProducts = products.filter((p) =>
    p.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center text-gray-700 text-lg">
        Wczytywanie produktu...
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen flex flex-col items-center justify-center text-center text-red-600">
        <p>{error}</p>
        <button
          onClick={fetchProducts}
          className="mt-4 px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700"
        >
          Spróbuj ponownie
        </button>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200">
      {/* <Navbar /> */}

      <div className="max-w-7xl mx-auto flex flex-col lg:flex-row mt-8 p-4 gap-6">
        {/* Pasek filtrów po lewej stronie */}
        <aside className="w-full lg:w-1/4 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 h-fit">
          <h2 className="text-xl font-bold text-gray-800 mb-4">Filtry</h2>

          <div className="mb-6">
            <label className="block text-gray-600 mb-2 font-semibold">Szukaj produktu</label>
            <input
              type="text"
              placeholder="np. Imbryczek"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="w-full p-3 border rounded-xl shadow-sm focus:ring focus:ring-blue-300"
            />
          </div>

          <div className="mb-6">
            <label className="block text-gray-600 mb-2 font-semibold">Kategoria</label>
            <select
              value={selectedCategory}
              onChange={(e) => setSelectedCategory(e.target.value)}
              className="w-full p-3 border rounded-xl shadow-sm"
            >
              <option>Wszystko</option>
              <option>AGD</option>
              <option>Elektronika</option>
              <option>Dom i ogród</option>
              <option>Inne</option>
            </select>
          </div>

          <div>
            <label className="block text-gray-600 mb-2 font-semibold">Zakres cen</label>
            <select
              value={selectedPriceRange}
              onChange={(e) => setSelectedPriceRange(e.target.value)}
              className="w-full p-3 border rounded-xl shadow-sm"
            >
              <option>Wszystko</option>
              <option>0 - 50 zł</option>
              <option>50 - 200 zł</option>
              <option>200 - 500 zł</option>
              <option>500+ zł</option>
            </select>
          </div>

          <div className="mt-6">
            <button
              onClick={fetchProducts}
              className="w-full bg-blue-600 text-white font-semibold py-3 rounded-xl hover:bg-blue-700 transition"
            >
              Zastosuj filtry
            </button>
          </div>
        </aside>

        {/* Lista produktów po prawej stronie */}
        <main className="flex-1">
          <h1 className="text-2xl font-bold text-gray-800 mb-6">Szczegóły produktu</h1>

          {filteredProducts.length > 0 ? (
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {filteredProducts.map((product, index) => (
                <div
                  key={index}
                  className="bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 hover:shadow-xl transition"
                >
                  <h2 className="text-2xl font-bold text-gray-800 mb-4">{product.name}</h2>
                  <p className={`${getQuantityColor(product.description)} mb-4`}>{product.description}</p>

                  {product.additionalDescriptions && product.additionalDescriptions.length > 0 && (
                    <div className="mb-4">
                      <h3 className="font-semibold">Dodatkowe opisy:</h3>
                      <ul className="list-disc pl-6 text-gray-700">
                        {product.additionalDescriptions.map((desc, i) => (
                          <li key={i}>
                            <strong>{desc.title}</strong>: {desc.value}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}

                  {product.additionalNames && product.additionalNames.length > 0 && (
                    <div className="mb-4">
                      <h3 className="font-semibold">Dodatkowe nazwy:</h3>
                      <ul className="list-disc pl-6 text-gray-700">
                        {product.additionalNames.map((n, i) => (
                          <li key={i}>
                            <strong>{n.title}</strong>: {n.value}
                          </li>
                        ))}
                      </ul>
                    </div>
                  )}

                  <div className="text-sm text-gray-500 mt-4">
                    <p><strong>SKU:</strong> {product.sku}</p>
                    <p><strong>EAN:</strong> {product.ean}</p>
                  </div>

                  <div className="flex gap-4 mt-6">

                    <button
                      onClick={() => handleEditQuantityProduct(product)}
                      className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                    >
                      Edytuj Ilość
                    </button>

                    <button
                      onClick={() => handleEditMinQuantityProduct(product)}
                      className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                    >
                      Edytuj Min. Ilość
                    </button>

                    <button
                      onClick={() => handleDeleteProduct(product)}
                      className="px-4 py-2 bg-red-600 text-white rounded-xl hover:bg-red-700 transition"
                    >
                      Usuń
                    </button>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-gray-600 text-center mt-10">Brak produktów do wyświetlenia.</p>
          )}
        </main>
      </div>
      {editingProduct && (
        <div className="fixed inset-0 bg-black/40 backdrop-blur-sm flex items-center justify-center z-50">
          <div className="bg-white rounded-2xl shadow-2xl p-8 w-96">
            <h2 className="text-xl font-bold mb-4 text-gray-800">
              Edytuj produkt: {editingProduct.name}
            </h2>

            <label className="block text-gray-700 font-semibold mb-2">
              Minimalna ilość
            </label>
            <input
              type="number"
              min="0"
              value={newMinQuantity}
              onChange={(e) => setNewMinQuantity(e.target.value)}
              className="w-full border p-3 rounded-xl shadow-sm focus:ring focus:ring-blue-300 mb-6"
            />

            <div className="flex justify-end gap-4">
              <button
                onClick={() => setEditingProduct(null)}
                className="px-4 py-2 bg-gray-300 rounded-xl hover:bg-gray-400"
              >
                Anuluj
              </button>

              <button
                onClick={handleConfirmEdit}
                className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700"
              >
                Potwierdź edycję
              </button>
            </div>
          </div>
        </div>
      )}
      {editingQuantityProduct && (
        <div className="fixed inset-0 bg-black/40 backdrop-blur-sm flex items-center justify-center z-50">
          <div className="bg-white rounded-2xl shadow-2xl p-8 w-96">
            <h2 className="text-xl font-bold mb-4 text-gray-800">
              Zmień ilość produktu: {editingQuantityProduct.name}
            </h2>

            <label className="block text-gray-700 font-semibold mb-2">
              Ilość
            </label>
            <input
              type="number"
              min="0"
              value={quantityChange}
              onChange={(e) => setQuantityChange(e.target.value)}
              className="w-full border p-3 rounded-xl shadow-sm focus:ring focus:ring-blue-300 mb-4"
            />

            <label className="block text-gray-700 font-semibold mb-2">Akcja</label>
            <select
              value={quantityAction}
              onChange={(e) => setQuantityAction(e.target.value as 'add' | 'subtract')}
              className="w-full p-3 border rounded-xl shadow-sm mb-6"
            >
              <option value="add">Dodaj</option>
              <option value="subtract">Odejmij</option>
            </select>

            <div className="flex justify-end gap-4">
              <button
                onClick={() => setEditingQuantityProduct(null)}
                className="px-4 py-2 bg-gray-300 rounded-xl hover:bg-gray-400"
              >
                Anuluj
              </button>

              <button
                onClick={handleConfirmQuantityChange}
                className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700"
              >
                Potwierdź
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ProductList;
