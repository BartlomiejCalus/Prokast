'use client'
import { useState } from "react";

interface Product {
  id: string;
  name: string;

}

export default function ProductsForm() {
  const [clientID, setClientID] = useState<string>("");
  const [productName, setProductName] = useState<string>("");
  const [data, setData] = useState<Product | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!clientID || !productName) {
      setError("Both clientID and productName are required");
      return;
    }

    setLoading(true);
    setError(null);
    setData(null);

    try {
      const response = await fetch("/api/storedProducts", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ clientID, productName }),
      });

      if (!response.ok) {
        throw new Error(`Error fetching data: ${response.statusText}`);
      }

      const result = await response.json();
      setData(result);
    } catch (err: unknown) {
      setError((err as Error).message || "Something went wrong");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h1>Fetch Products</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="clientID">ClientID:</label>
          <input
            id="clientID"
            type="text"
            value={clientID}
            onChange={(e) => setClientID(e.target.value)}
            
          />
        </div>
        <div>
          <label htmlFor="productName">Product Name:</label>
          <input
            id="productName"
            type="text"
            value={productName}
            onChange={(e) => setProductName(e.target.value)}
            
          />
        </div>
        <button type="submit">Fetch Data</button>
      </form>

      {loading && <p>Loading...</p>}
      {error && <p style={{ color: "red" }}>Error: {error}</p>}
      {data && (
        <div>
          <h2>Response Data:</h2>
          <pre>{JSON.stringify(data, null, 2)}</pre>
        </div>
      )}
    </div>
  );
}
