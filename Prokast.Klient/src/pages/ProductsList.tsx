import React, { useState, FormEvent, useEffect } from 'react';
import Navbar from '../Components/Navbar';

// Interfejs dla typu produktu dla lepszej kontroli typów
interface Product {
  title: string;
  description: string;
  price: string;
  image: string;
  location: string;
  date: string;
}

const ProductList: React.FC = () => {
  // Początkowe dane produktów
  const initialProducts: Product[] = [
    {
      title: 'Czerwona kurtka zimowa',
      description: 'Stylowa i ciepła kurtka idealna na zimowe dni.',
      price: '249.99 zł',
      image: 'https://via.placeholder.com/300x200.png?text=Kurtka',
      location: 'Warszawa',
      date: '21 lipca 2025',
    },
    {
      title: 'Smartfon Galaxy X10',
      description: 'Nowoczesny smartfon z dużym ekranem i świetnym aparatem.',
      price: '1599.00 zł',
      image: 'https://via.placeholder.com/300x200.png?text=Smartfon',
      location: 'Kraków',
      date: '20 lipca 2025',
    },
    {
      title: 'Fotel gamingowy',
      description: 'Wygodny fotel do grania z regulowanym oparciem i podłokietnikami.',
      price: '549.99 zł',
      image: 'https://via.placeholder.com/300x200.png?text=Fotel',
      location: 'Poznań',
      date: '19 lipca 2025',
    },
  ];

  // Stan przechowujący listę produktów
  const [products, setProducts] = useState<Product[]>(initialProducts);
  
  // Stany do zarządzania modalem (popupem)
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMode, setModalMode] = useState<'add' | 'edit'>('add');
  const [currentProduct, setCurrentProduct] = useState<Product | null>(null);

  // --- STARE STANY ---
  const [selectedCategory, setSelectedCategory] = useState('');
  const [priceMin, setPriceMin] = useState('');
  const [priceMax, setPriceMax] = useState('');
  const [condition, setCondition] = useState('');
  const [availability, setAvailability] = useState('');
  const [sortOrder, setSortOrder] = useState('');
  const [searchTerm, setSearchTerm] = useState('');

  // Filtrowanie jest teraz oparte na stanie `products`
  const filteredProducts = products.filter((product) =>
    product.title.toLowerCase().includes(searchTerm.toLowerCase())
  );




  const handleOpenAddModal = () => {
    setModalMode('add');
    setCurrentProduct(null); // Resetuj produkt, aby formularz był pusty
    setIsModalOpen(true);
  };


  const handleOpenEditModal = (product: Product) => {
    setModalMode('edit');
    setCurrentProduct(product); // Ustaw produkt do edycji
    setIsModalOpen(true);
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

  // Usunięcie produktu
  const handleDelete = (title: string) => {
    if (window.confirm(`Czy na pewno chcesz usunąć produkt: ${title}?`)) {
        setProducts(products.filter(p => p.title !== title));
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />

      <div className="max-w-6xl mx-auto mt-8">
        <h1 className="text-2xl font-bold text-center text-gray-800 mb-6">Lista produktów</h1>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* Sidebar z filtrami */}
          <div className="lg:col-span-1 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 h-fit">
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

          {/* Lista produktów z wyszukiwarką */}
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
                  src={product.image}
                  alt={product.title}
                  className="w-full sm:w-60 h-auto rounded-xl object-cover"
                />
                <div className="flex-1">
                  <h2 className="text-xl font-semibold text-gray-800">{product.title}</h2>
                  <p className="text-gray-600 mt-2">{product.description}</p>
                  <p className="text-blue-700 font-bold text-lg mt-4">{product.price}</p>
                  <div className="text-sm text-gray-500 mt-2">
                    <p>{product.location}</p>
                    <p>{product.date}</p>
                  </div>

                  {/* Zaktualizowane przyciski z nowymi handlerami */}
                  <div className="mt-4 flex gap-3">
                    <button
                      onClick={() => handleOpenEditModal(product)}
                      className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                    >
                      Edytuj
                    </button>
                    <button
                      onClick={() => handleDelete(product.title)}
                      className="px-4 py-2 bg-red-500 text-white rounded-xl hover:bg-red-600 transition"
                    >
                      Usuń
                    </button>
                  </div>
                </div>
              </div>
            ))}

            {filteredProducts.length === 0 && (
              <p className="text-gray-600 text-center">Brak wyników pasujących do wyszukiwania.</p>
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