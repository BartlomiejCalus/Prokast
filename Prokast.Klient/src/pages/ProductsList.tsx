import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Navbar from '../Components/Navbar';

const ProductList: React.FC = () => {
  const navigate = useNavigate();
  const [selectedCategory, setSelectedCategory] = useState('');
  const [priceMin, setPriceMin] = useState('');
  const [priceMax, setPriceMax] = useState('');
  const [condition, setCondition] = useState('');
  const [availability, setAvailability] = useState('');
  const [sortOrder, setSortOrder] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [products, setProducts] = useState<any[]>([]);

  // 🔹 Inicjalne dane produktów
  const initialProducts = [
    {
      name: 'Czerwona kurtka zimowa',
      sku: 'KURTKA-001',
      ean: '5901234567890',
      description: 'Stylowa i ciepła kurtka idealna na zimowe dni.',
      additionalNames: [{ title: 'Winter Jacket', region: 1, value: 'EN' }],
      dictionaryParams: [
        { regionID: 1, name: 'Materiał', type: 'tekst', value: 'Poliester' },
        { regionID: 1, name: 'Kolor', type: 'tekst', value: 'Czerwony' },
      ],
      customParams: [
        { id: 1, name: 'Wodoodporność', type: 'boolean', value: 'Tak', clientID: 1 },
      ],
      photos: [
        { name: 'kurtka.jpg', value: 'https://via.placeholder.com/300x200.png?text=Kurtka' },
      ],
      prices: [
        { name: 'Cena detaliczna', regionID: 1, netto: 199.99, vat: 23, brutto: 245.99, priceListID: 1 },
      ],
      priceList: { name: 'Zimowa Kolekcja' },
    },
    {
      name: 'Smartfon Galaxy X10',
      sku: 'SMART-002',
      ean: '5909876543210',
      description: 'Nowoczesny smartfon z dużym ekranem i świetnym aparatem.',
      additionalNames: [{ title: 'Galaxy X10', region: 2, value: 'EN' }],
      dictionaryParams: [
        { regionID: 2, name: 'Pamięć RAM', type: 'liczba', value: '8 GB' },
        { regionID: 2, name: 'Kolor', type: 'tekst', value: 'Czarny' },
      ],
      customParams: [
        { id: 2, name: 'Dual SIM', type: 'boolean', value: 'Tak', clientID: 1 },
      ],
      photos: [
        { name: 'smartfon.jpg', value: 'https://via.placeholder.com/300x200.png?text=Smartfon' },
      ],
      prices: [
        { name: 'Cena regularna', regionID: 2, netto: 1299.0, vat: 23, brutto: 1599.0, priceListID: 2 },
      ],
      priceList: { name: 'Elektronika Premium' },
    },
    {
      name: 'Fotel gamingowy',
      sku: 'FOTEL-003',
      ean: '5901112223334',
      description: 'Wygodny fotel do grania z regulowanym oparciem i podłokietnikami.',
      additionalNames: [{ title: 'Gaming Chair', region: 3, value: 'EN' }],
      dictionaryParams: [
        { regionID: 3, name: 'Materiał', type: 'tekst', value: 'Skóra ekologiczna' },
        { regionID: 3, name: 'Kolor', type: 'tekst', value: 'Czarny' },
      ],
      customParams: [
        { id: 3, name: 'Regulacja wysokości', type: 'boolean', value: 'Tak', clientID: 1 },
      ],
      photos: [
        { name: 'fotel.jpg', value: 'https://via.placeholder.com/300x200.png?text=Fotel' },
      ],
      prices: [
        { name: 'Cena katalogowa', regionID: 3, netto: 449.99, vat: 23, brutto: 549.99, priceListID: 3 },
      ],
      priceList: { name: 'Meble i Komfort' },
    },
  ];

  // 🔸 Wczytanie z localStorage lub inicjalizacja domyślnych danych
  useEffect(() => {
    try {
      const saved = localStorage.getItem('products');
      if (saved) {
        const parsed = JSON.parse(saved);
        if (Array.isArray(parsed) && parsed.length > 0) {
          setProducts(parsed);
          return;
        }
      }
    } catch (err) {
      console.warn('Błąd przy wczytywaniu produktów:', err);
    }
    // jeśli localStorage jest puste lub błędne
    setProducts(initialProducts);
    localStorage.setItem('products', JSON.stringify(initialProducts));
  }, []);

  // 🔎 Filtrowanie produktów
  const filteredProducts = products.filter((product) =>
    product.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  // 🔧 Edycja produktu
  const handleEdit = (name: string) => {
    const productToEdit = products.find((p) => p.name === name);
    if (productToEdit) {
      localStorage.setItem('editProduct', JSON.stringify(productToEdit));
      navigate('/editproducts');
    }
  };

  // 🔧 Usuwanie produktu (na razie tylko alert)
  const handleDelete = (name: string) => {
    alert(`Usuń produkt: ${name}`);
  };
  
  // Zamykanie modala
  const handleCloseModal = () => {
    setIsModalOpen(false);
    setCurrentProduct(null);
  };
  
  const handleFormSubmit = (productData: Product) => {
    if (modalMode === 'add') {
      // Dodaj nowy produkt na początek listy
      setProducts([productData, ...products]);
    } else {
      // Zaktualizuj istniejący produkt
      setProducts(products.map(p => p.title === currentProduct?.title ? productData : p));
    }
    handleCloseModal();
  };

  const handleDelete = (title: string) => {
  if (window.confirm(`Czy na pewno chcesz usunąć produkt: "${title}"?`)) {
    setProducts(prevProducts => prevProducts.filter(p => p.title !== title));
    alert(`Produkt "${title}" został usunięty.`);
  }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />

      <div className="max-w-6xl mx-auto mt-8">
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-2xl font-bold text-gray-800">Lista produktów</h1>
          <button
            onClick={() => navigate('/AddProducts')}
            className="px-4 py-2 bg-green-500 text-white rounded-xl hover:bg-green-600 transition"
          >
            Dodaj produkt
          </button>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* 🔹 Panel filtrów (pozostaje bez zmian) */}
          <div className="lg:col-span-1 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6">
            <h2 className="text-xl font-bold text-gray-800 mb-4">Filtry</h2>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="category">Kategoria:</label>
            <select
              id="category"
              className="w-full p-2 mb-4 border rounded-xl"
              value={selectedCategory}
              onChange={(e) => setSelectedCategory(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="odziez">Odzież</option>
              <option value="elektronika">Elektronika</option>
              <option value="dom">Dom i ogród</option>
            </select>

            <label className="block mb-2 font-medium text-gray-700">Cena (zł):</label>
            <div className="flex gap-2 mb-4">
              <input
                type="number"
                placeholder="Od"
                className="w-1/2 p-2 border rounded-xl"
                value={priceMin}
                onChange={(e) => setPriceMin(e.target.value)}
              />
              <input
                type="number"
                placeholder="Do"
                className="w-1/2 p-2 border rounded-xl"
                value={priceMax}
                onChange={(e) => setPriceMax(e.target.value)}
              />
            </div>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="condition">Stan produktu:</label>
            <select
              id="condition"
              className="w-full p-2 mb-4 border rounded-xl"
              value={condition}
              onChange={(e) => setCondition(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="nowy">Nowy</option>
              <option value="uzywany">Używany</option>
            </select>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="availability">Dostępność:</label>
            <select
              id="availability"
              className="w-full p-2 mb-4 border rounded-xl"
              value={availability}
              onChange={(e) => setAvailability(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="dostepny">W magazynie</option>
              <option value="brak">Brak w magazynie</option>
            </select>

            <label className="block mb-2 font-medium text-gray-700" htmlFor="sort">Sortuj według:</label>
            <select
              id="sort"
              className="w-full p-2 border rounded-xl"
              value={sortOrder}
              onChange={(e) => setSortOrder(e.target.value)}
            >
              <option value="">-- Wybierz --</option>
              <option value="cena-rosnaco">Cena: od najniższej</option>
              <option value="cena-malejaco">Cena: od najwyższej</option>
              <option value="najnowsze">Najnowsze</option>
            </select>
          </div>

          {/* 🔹 Lista produktów */}
          <div className="lg:col-span-2 flex flex-col gap-6">
            <div className="flex flex-col sm:flex-row gap-4">
              <input
                type="text"
                placeholder="Szukaj po nazwie produktu..."
                className="w-full p-3 border rounded-xl shadow-sm"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
              />
              <button
                onClick={handleOpenAddModal}
                className="px-6 py-3 bg-green-500 text-white font-semibold rounded-xl hover:bg-green-600 transition shadow-sm whitespace-nowrap"
              >
                Dodaj nowy produkt
              </button>
            </div>


            {filteredProducts.map((product, index) => (
              <div
                key={index}
                className="bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-4 flex flex-col sm:flex-row gap-4 items-start"
              >
                <img
                  src={product.photos[0]?.value}
                  alt={product.name}
                  className="w-full sm:w-60 h-auto rounded-xl object-cover"
                />
                <div className="flex-1">
                  <h2 className="text-xl font-semibold text-gray-800">{product.name}</h2>
                  <p className="text-gray-600 mt-2">{product.description}</p>

                  <div className="mt-3 text-sm text-gray-700">
                    <p><strong>SKU:</strong> {product.sku}</p>
                    <p><strong>EAN:</strong> {product.ean}</p>
                    <p><strong>Cennik:</strong> {product.priceList.name}</p>
                  </div>

                  <p className="text-blue-700 font-bold text-lg mt-4">
                    {product.prices[0].brutto.toFixed(2)} zł
                  </p>

                  <div className="mt-4 flex gap-3">
                    <button
                      onClick={() => handleEdit(product.name)}
                      className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                    >
                      Edytuj
                    </button>
                    <button
                      onClick={() => handleDelete(product.name)}
                      className="px-4 py-2 bg-red-500 text-white rounded-xl hover:bg-red-600 transition"
                    >
                      Usuń
                    </button>
                  </div>
                </div>
              </div>
            ))}

            {filteredProducts.length === 0 && (
              <p className="text-gray-600 text-center">
                Brak wyników pasujących do wyszukiwania.
              </p>
            )}
          </div>
        </div>
      </div>
      
      {/* --- NOWY ELEMENT: MODAL --- */}
      {isModalOpen && (
        <ProductModal
            mode={modalMode}
            onClose={handleCloseModal}
            onSubmit={handleFormSubmit}
            productData={currentProduct}
        />
      )}
    </div>
  );
};

interface ProductModalProps {
    mode: 'add' | 'edit';
    onClose: () => void;
    onSubmit: (product: Product) => void;
    productData: Product | null;
}

const ProductModal: React.FC<ProductModalProps> = ({ mode, onClose, onSubmit, productData }) => {
    const [formData, setFormData] = useState<Omit<Product, 'date' | 'image'>>({
        title: '',
        description: '',
        price: '',
        location: '',
    });

    
    useEffect(() => {
        if (mode === 'edit' && productData) {
            setFormData({
                title: productData.title,
                description: productData.description,
                price: productData.price.replace(' zł', ''),
                location: productData.location,
            });
        }
    }, [mode, productData]);


    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        const finalProduct: Product = {
            ...formData,
            price: `${formData.price} zł`,
            date: new Date().toLocaleDateString('pl-PL', { day: 'numeric', month: 'long', year: 'numeric'}),
            image: productData?.image || `https://via.placeholder.com/300x200.png?text=${formData.title.replace(' ', '+')}`
        };
        onSubmit(finalProduct);
    };

    return (
        <div 
            className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50"
            onClick={onClose} 
        >
            <div 
                className="bg-gradient-to-br from-blue-100 via-white to-blue-200 p-8 rounded-2xl shadow-2xl w-full max-w-lg m-4 relative"
                onClick={(e) => e.stopPropagation()} 
            >
                <button 
                    onClick={onClose}
                    className="absolute top-4 right-4 text-gray-500 hover:text-gray-800 text-2xl"
                >
                    &times;
                </button>

                <h2 className="text-2xl font-bold text-gray-800 mb-6 text-center">
                    {mode === 'add' ? 'Dodaj nowy produkt' : 'Edytuj produkt'}
                </h2>

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label htmlFor="title" className="block mb-1 font-medium text-gray-700">Tytuł</label>
                        <input type="text" name="title" id="title" value={formData.title} onChange={handleChange} className="w-full p-2 border rounded-xl" required />
                    </div>
                    <div>
                        <label htmlFor="description" className="block mb-1 font-medium text-gray-700">Opis</label>
                        <textarea name="description" id="description" value={formData.description} onChange={handleChange as any} className="w-full p-2 border rounded-xl h-24" required />
                    </div>
                    <div className="flex gap-4">
                        <div className="w-1/2">
                            <label htmlFor="price" className="block mb-1 font-medium text-gray-700">Cena (zł)</label>
                            <input type="number" step="0.01" name="price" id="price" value={formData.price} onChange={handleChange} className="w-full p-2 border rounded-xl" required />
                        </div>
                        <div className="w-1/2">
                            <label htmlFor="location" className="block mb-1 font-medium text-gray-700">Lokalizacja</label>
                            <input type="text" name="location" id="location" value={formData.location} onChange={handleChange} className="w-full p-2 border rounded-xl" required />
                        </div>
                    </div>
                    <div className="flex justify-end gap-4 pt-4">
                        <button type="button" onClick={onClose} className="px-6 py-2 bg-gray-300 text-gray-800 rounded-xl hover:bg-gray-400 transition">
                            Anuluj
                        </button>
                        <button type="submit" className="px-6 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition">
                            Zapisz
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default ProductList;