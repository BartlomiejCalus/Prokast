'use client';

import { useState } from "react";
import axios from "axios";

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
  const [editingId, setEditingId] = useState<number | null>(null);
  const [newQuantity, setNewQuantity] = useState<number>(0);

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
    } catch (err: any) {
      setError(err.response?.data?.error || "Something went wrong");
    } finally {
      setLoading(false);
    }
  };

  const updateQuantity = async (id: number) => {
    try {
      const formData = new FormData();
      formData.append("quantity", newQuantity.toString());

      await axios.put(`/api/storedProductsChangingQuantity/${id}`, {
        clientID,
        quantity: newQuantity, // Wysyłamy nową wartość, a nie zmianę!
      }, {
        headers: { "Content-Type": "application/json" },
      });
      
      fetchStoredProducts();
      setEditingId(null);
    } catch (err: any) {
      setError(err.response?.data?.error || "Failed to update quantity");
    }
    
  };
  

  return (
    <div className="p-4">
      <h1 className="text-xl font-bold">Stored Products</h1>
      <div className="my-4">
        <input
          type="text"
          placeholder="Client ID"
          value={clientID}
          onChange={(e) => setClientID(e.target.value)}
          className="border p-2 mr-2"
        />
        <input
          type="text"
          placeholder="Warehouse ID"
          value={warehouseID}
          onChange={(e) => setWarehouseID(e.target.value)}
          className="border p-2 mr-2"
        />
        <button onClick={fetchStoredProducts} className="bg-blue-500 text-white p-2">
          Fetch Data
        </button>
      </div>

      {loading && <p>Loading...</p>}
      {error && <p className="text-red-500">Error: {error}</p>}
      {data && (
        <table className="border-collapse border w-full">
          <thead>
            <tr>
              <th className="border p-2">ID</th>
              <th className="border p-2">Warehouse ID</th>
              <th className="border p-2">Product ID</th>
              <th className="border p-2">Quantity</th>
              <th className="border p-2">Min Quantity</th>
              <th className="border p-2">Last Updated</th>
              <th className="border p-2">Actions</th>
            </tr>
          </thead>
          <tbody>
            {data.map((item) => (
              <tr key={item.id}>
                <td className="border p-2">{item.id}</td>
                <td className="border p-2">{item.warehouseID}</td>
                <td className="border p-2">{item.productID}</td>
                <td className="border p-2">
                  {editingId === item.id ? (
                    <input
                      type="number"
                      value={newQuantity}
                      onChange={(e) => setNewQuantity(Number(e.target.value))}
                      className="border p-1"
                    />
                  ) : (
                    item.quantity
                  )}
                </td>
                <td className="border p-2">{item.minQuantity}</td>
                <td className="border p-2">{new Date(item.lastUpdated).toLocaleDateString()}</td>
                <td className="border p-2">
                  {editingId === item.id ? (
                    <button onClick={() => updateQuantity(item.id)} className="bg-green-500 text-white p-1 mr-2">
                      Save
                    </button>
                  ) : (
                    <button onClick={() => { setEditingId(item.id); setNewQuantity(item.quantity); }} className="bg-yellow-500 text-white p-1">
                      Edit
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
    
  );
  
}
