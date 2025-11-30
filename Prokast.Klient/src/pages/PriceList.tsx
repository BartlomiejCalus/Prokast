import React, { useState, useEffect } from "react";
import Navbar from "../Components/Navbar";
import axios from "axios";
import Cookies from "js-cookie";
import PriceListComponent from "../Components/PriceListComponent";

const API_URL = process.env.REACT_APP_API_URL;

interface Price {
  id: number;
  name: string;
  netto: number;
  brutto: number;
  vat: number;
  regionID: number;
}

interface PriceListItem {
  id: number;
  name: string;
  prices: Price[];
}

const PriceList: React.FC = () => {
  const [priceLists, setPriceLists] = useState<PriceListItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const fetchPriceLists = async () => {
    try {
      setLoading(true);
      const token = Cookies.get("token");
      const res = await axios.get(`${API_URL}/api/priceLists`, {
        headers: token ? { Authorization: `Bearer ${token}` } : {},
      });

      const lists: PriceListItem[] = Array.isArray(res.data.model)
        ? res.data.model
        : [];
      setPriceLists(lists);
      setError("");
    } catch (err: any) {
      console.error(err);
      setError("Nie udało się pobrać cenników z API.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPriceLists();
  }, []);

  return (
    <div className="min-h-screen bg-gradient-to-br from-green-100 via-white to-green-200 p-4">
      <Navbar />

      <main className="max-w-5xl mx-auto mt-8 space-y-6">
        <h2 className="text-2xl font-bold mb-4 text-gray-800">Cenniki</h2>

        {loading ? (
          <p className="text-gray-600">Ładowanie cenników...</p>
        ) : error ? (
          <p className="text-red-600">{error}</p>
        ) : priceLists.length === 0 ? (
          <p className="text-gray-600">Brak cenników do wyświetlenia.</p>
        ) : (
          priceLists.map((pl) => (
            <PriceListComponent
              key={pl.id}
              data={pl}
              productId={pl.id.toString()}
            />
          ))
        )}
      </main>
    </div>
  );
};

export default PriceList;