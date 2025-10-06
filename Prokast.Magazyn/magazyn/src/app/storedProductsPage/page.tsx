'use client';

import { useState } from "react";
import axios from "axios";
import ChoiceMenu from "@/components/choiceMenu";

interface StoredProduct {
  id: number;
  warehouseID: number;
  productID: number;
  quantity: number;
  minQuantity: number;
  lastUpdated: string;
}

export default function StoredProductsPage() {
  const [clientID, setClientID] = useState<string>("");
  const [warehouseID, setWarehouseID] = useState<string>("");
  const [data, setData] = useState<StoredProduct[] | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [showModal, setShowModal] = useState<boolean>(false);
  const [selectedProduct, setSelectedProduct] = useState<number | null>(null);
  const [newQuantity, setNewQuantity] = useState<number>(0);
  const [isFetched, setIsFetched] = useState<boolean>(false);

  const fetchStoredProducts = async () => {
    if (!clientID || !warehouseID) {
      setError("Both clientID and warehouseID are required");
      return;
    }
    setLoading(true);
    setError(null);
    setData(null);

    try {
      const response = await axios.get("/api/storedProducts", {
        params: { clientID, warehouseID },
      });
      setData(response.data.model);
      setIsFetched(true);
    } catch (err: any) {
      setError(err.response?.data?.error || "Something went wrong");
    } finally {
      setLoading(false);
    }
  };

  const handleDelivery = async () => {
    if (selectedProduct === null || newQuantity <= 0) return;

    try {
      await axios.put(`/api/storedProductsChangingQuantity/${selectedProduct}`, {
        clientID,
        quantity: newQuantity,
      }, {
        headers: { "Content-Type": "application/json" },
      });

      fetchStoredProducts();
      setShowModal(false);
    } catch (err: any) {
      setError(err.response?.data?.error || "Failed to update quantity");
    }
  };

  return (
    
    <div className=" min-h-screen" style={{ backgroundColor: '#F0F6FD', color: 'var(--foreground)' }}>
      <ChoiceMenu />
      <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Stored Products</h1>

      <div className="my-4 flex flex-wrap gap-2">
        <input
          type="text"
          placeholder="Client ID"
          value={clientID}
          onChange={(e) => setClientID(e.target.value)}
          className="border p-2 rounded w-48"
        />
        <input
          type="text"
          placeholder="Warehouse ID"
          value={warehouseID}
          onChange={(e) => setWarehouseID(e.target.value)}
          className="border p-2 rounded w-48"
        />
        <button
          onClick={fetchStoredProducts}
          className="bg-[#015183] hover:bg-[#013d63] text-white p-2 rounded"
        >
          Fetch Data
        </button>

        {isFetched && (
        <div className="text-center ">
          <button
            onClick={() => setShowModal(true)}
            className="bg-green-600 hover:bg-green-700 text-white p-2 rounded"
          >
            Dostawa
          </button>
        </div>
      )}
      
      </div>

      {loading && <p>Loading...</p>}
      {error && <p className="text-red-600">Error: {error}</p>}

      

      {data && (
        <div className="overflow-x-auto">
          <table className="border-collapse border w-full shadow-md rounded bg-white">
            <thead className="bg-[#a0c7d5] text-[#015183]">
              <tr>
                <th className="border p-2">Product ID</th>
                <th className="border p-2">Quantity</th>
                <th className="border p-2">Min Quantity</th>
                <th className="border p-2">Last Updated</th>
              </tr>
            </thead>
            <tbody>
              {data.map((item) => (
                <tr key={item.id} className="text-center">
                  <td className="border p-2">{item.productID}</td>
                  <td className="border p-2">{item.quantity}</td>
                  <td className="border p-2">{item.minQuantity}</td>
                  <td className="border p-2">
                    {new Date(item.lastUpdated).toLocaleDateString()}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      

      {showModal && (
        <div className="fixed inset-0 flex items-center justify-center bg-gray-800 bg-opacity-60">
          <div className="bg-[#a0c7d5] text-[#015183] p-6 rounded-lg shadow-xl w-96">
            <h2 className="text-xl font-semibold mb-3">Dodaj dostawę</h2>

            <select
              className="border p-2 w-full mb-3 rounded"
              onChange={(e) => setSelectedProduct(Number(e.target.value))}
            >
              <option value="">Wybierz produkt</option>
              {data?.map((item) => (
                <option key={item.id} value={item.id}>
                  {`Produkt ${item.productID}`}
                </option>
              ))}
            </select>

            <label className="block mb-1">Podaj ilość</label>
            <input
              type="number"
              placeholder="Nowa ilość"
              value={newQuantity}
              onChange={(e) => setNewQuantity(Number(e.target.value))}
              className="border p-2 w-full mb-4 rounded"
            />

            <div className="flex justify-between">
              <button
                onClick={() => setShowModal(false)}
                className="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded"
              >
                Anuluj
              </button>
              <button
                onClick={handleDelivery}
                className="bg-[#015183] hover:bg-[#013d63] text-white px-4 py-2 rounded"
              >
                Zapisz
              </button>
            </div>
          </div>
        </div>
      )}
      </div>
    </div>
  );
}
