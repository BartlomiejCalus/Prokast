'use client';
import React, { useEffect, useState } from "react";
import axios from "axios";
import { storageResponse, storedProduct } from "@/models/Products";

export default function Tests() {
  const [data, setData] = useState<storageResponse | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      const res = await axios.get<storageResponse>("/api/storedProduct");
      setData(res.data);
      console.log("Stored Products Response:", res.data);
    };

    fetchData();
  }, []);

  return <div>

    <h1 className="text-2xl font-bold mb-4">Stored Products Data</h1>
     <ul className="list-disc pl-4">
        {data?.model?.map((item: storedProduct) => (
          <li key={item.id}>
            {item.productName} — ilość: {item.quantity} (min: {item.minQuantity})
          </li>
        ))}
      </ul>
  </div>;
}
