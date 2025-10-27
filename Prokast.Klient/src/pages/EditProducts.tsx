import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Navbar from "../Components/Navbar";

const EditProducts: React.FC = () => {
  const navigate = useNavigate();
  const [product, setProduct] = useState<any>(null);

  useEffect(() => {
    const storedProduct = localStorage.getItem("editProduct");
    if (storedProduct) {
      setProduct(JSON.parse(storedProduct));
    }
  }, []);

  if (!product) {
    return (
      <div className="min-h-screen flex flex-col bg-gradient-to-br from-green-100 via-white to-green-200">
        <Navbar />
        <main className="flex flex-col items-center justify-center w-screen mt-10">
          <p className="text-gray-600 text-lg">Ładowanie produktu...</p>
        </main>
      </div>
    );
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setProduct((prev: any) => ({
      ...prev,
      [name]: value,
    }));
  };


  const handlePriceChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseFloat(e.target.value);
    setProduct((prev: any) => ({
      ...prev,
      prices: prev.prices
        ? [
            {
              ...prev.prices[0],
              brutto: isNaN(value) ? 0 : value,
            },
          ]
        : [
            {
              name: "Cena detaliczna",
              regionID: 1,
              netto: 0,
              vat: 23,
              brutto: isNaN(value) ? 0 : value,
              priceListID: 1,
            },
          ],
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const storedProducts = localStorage.getItem("products");
    if (storedProducts) {
      const products = JSON.parse(storedProducts);

      // 🔸 Znajdź produkt po SKU lub nazwie
      const updatedProducts = products.map((p: any) =>
        p.sku === product.sku || p.name === product.name ? product : p
      );

      localStorage.setItem("products", JSON.stringify(updatedProducts));
    }

    localStorage.removeItem("editProduct");
    navigate("/ProductsList");
  };

  const handleCancel = () => {
    navigate("/ProductsList");
  };

  return (
    <div className="min-h-screen flex flex-col bg-gradient-to-br from-green-100 via-white to-green-200">
      <Navbar />
      <main className="flex flex-col items-center justify-center w-screen mt-10">
        <form
          onSubmit={handleSubmit}
          className="w-full max-w-2xl p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-3"
        >
          <h2 className="text-2xl font-bold text-center text-gray-800">
            Edytuj produkt
          </h2>

          {/* --- PODSTAWOWE DANE --- */}
          <input
            type="text"
            name="name"
            placeholder="Nazwa produktu"
            value={product.name}
            onChange={handleChange}
            className="w-full p-2 border rounded-xl"
          />
          <input
            type="text"
            name="sku"
            placeholder="SKU"
            value={product.sku}
            onChange={handleChange}
            className="w-full p-2 border rounded-xl"
          />
          <input
            type="text"
            name="ean"
            placeholder="EAN"
            value={product.ean}
            onChange={handleChange}
            className="w-full p-2 border rounded-xl"
          />
          <textarea
            name="description"
            placeholder="Opis produktu"
            value={product.description}
            onChange={handleChange}
            className="w-full p-2 border rounded-xl"
          />

         
          <h3 className="font-semibold mt-4">Cena (brutto)</h3>
          <input
            type="number"
            name="price"
            placeholder="Cena brutto"
            value={product.prices?.[0]?.brutto ?? ""}
            onChange={handlePriceChange}
            className="w-full p-2 border rounded-xl"
          />

        
          <h3 className="font-semibold mt-4">Dodatkowe nazwy</h3>
          <select className="w-full p-2 border rounded-xl">
            <option value="">-- Wybierz dodatkową nazwę --</option>
            <option value="__add_new__" className="text-green-600 font-semibold">
              + Dodaj nową nazwę
            </option>
          </select>

          <h3 className="font-semibold mt-4">Parametry słownikowe</h3>
          <select className="w-full p-2 border rounded-xl">
            <option value="">-- Wybierz parametr słownikowy --</option>
            <option value="__add_new__" className="text-green-600 font-semibold">
              + Dodaj nowy parametr
            </option>
          </select>

          <h3 className="font-semibold mt-4">Własne parametry</h3>
          <select className="w-full p-2 border rounded-xl">
            <option value="">-- Wybierz własny parametr --</option>
            <option value="__add_new__" className="text-green-600 font-semibold">
              + Dodaj nowy parametr
            </option>
          </select>

          <h3 className="font-semibold mt-4">Zdjęcia</h3>
          <select className="w-full p-2 border rounded-xl">
            <option value="">-- Wybierz zdjęcie --</option>
            <option value="__add_new__" className="text-green-600 font-semibold">
              + Dodaj nowe zdjęcie
            </option>
          </select>

          <h3 className="font-semibold mt-4">Ceny</h3>
          <select className="w-full p-2 border rounded-xl">
            <option value="">-- Wybierz cenę --</option>
            <option value="__add_new__" className="text-green-600 font-semibold">
              + Dodaj nową cenę
            </option>
          </select>

          <h3 className="font-semibold mt-4">Lista cenowa</h3>
          <select className="w-full p-2 border rounded-xl">
            <option value="">-- Wybierz listę cenową --</option>
          </select>

          {/* --- PRZYCISKI --- */}
          <div className="flex gap-3 mt-6">
            <button
              type="button"
              onClick={handleCancel}
              className="flex-1 bg-gray-300 hover:bg-gray-400 text-gray-800 p-2 rounded-xl transition"
            >
              Anuluj
            </button>
            <button
              type="submit"
              className="flex-1 bg-green-500 hover:bg-green-600 text-white p-2 rounded-xl transition"
            >
              Zapisz zmiany
            </button>
          </div>
        </form>
      </main>
    </div>
  );
};

export default EditProducts;
