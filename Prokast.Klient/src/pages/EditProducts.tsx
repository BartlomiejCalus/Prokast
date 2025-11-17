import React, { useEffect, useState } from "react";
import axios from "axios";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";
import Navbar from "../Components/Navbar";

const EditProducts: React.FC = () => {
  const navigate = useNavigate();
  const [product, setProduct] = useState<any>(null);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    const storedProduct = localStorage.getItem("editProduct");
    if (storedProduct) {
      setProduct(JSON.parse(storedProduct));
    } else {
      setError("Nie znaleziono danych produktu.");
    }
  }, []);

  if (!product) {
    return (
      <div className="min-h-screen flex flex-col bg-gradient-to-br from-green-100 via-white to-green-200">
        <Navbar />
        <main className="flex flex-col items-center justify-center w-screen mt-10">
          <p className="text-gray-600 text-lg">Ładowanie produktu...</p>
          {error && <p className="text-red-600 mt-4">{error}</p>}
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

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);

    try {
      const token = Cookies.get("token");
      if (!token) {
        setError("Brak tokenu autoryzacyjnego.");
        return;
      }

      await axios.put(
        `https://localhost:7207/api/products/products/${product.id}`,
        {
          name: product.name,
          sku: product.sku,
          ean: product.ean,
          description: product.description,
          prices: product.prices || [],
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      localStorage.removeItem("editProduct");
      navigate("/ProductsList");
    } catch (err: any) {
      console.log("Błąd API:", err.response?.data ?? err);
      setError("Nie udało się zapisać zmian.");
    } finally {
      setSaving(false);
    }
  };

  const handleCancel = () => {
    localStorage.removeItem("editProduct");
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

          {error && <p className="text-red-600">{error}</p>}

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
              disabled={saving}
              className="flex-1 bg-green-500 hover:bg-green-600 text-white p-2 rounded-xl transition"
            >
              {saving ? "Zapisywanie..." : "Zapisz zmiany"}
            </button>
          </div>
        </form>
      </main>
    </div>
  );
};

export default EditProducts;
