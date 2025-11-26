'use client';
import ChoiceMenu from "@/components/choiceMenu";
import React, { useEffect, useState } from "react";
import { storedProduct } from "@/models/Products";
import { storageResponse } from "@/models/Products";
import axios from "axios";
import StoredProductsTable from "@/components/storedProducts";
import DeliveryModal from "@/components/deliveryModal";




  

export default function TestsPage() {
  const [data, setData] = useState<storageResponse | null>(null);


  useEffect(() => {
    axios.get<storageResponse>("/api/storedProduct").then(res => setData(res.data));
  }, []);

  return (
    <div className="w-screen h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 flex flex-col">
      <ChoiceMenu />

      <div className="flex flex-row flex-grow">
        {/* Sidebar */}
        <div className="flex flex-col p-6 gap-6 w-1/6 h-full bg-white shadow-lg border-r border-gray-300">
          <h2 className="font-bold text-2xl text-gray-800">📦 Zestaw Statyczny</h2>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">
            Zestaw
          </button>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">
            Import
          </button>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">
            Dodatki
          </button>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">
            Ustawienia
          </button>
        </div>

        {/* Główna sekcja */}
        <div className="flex flex-col w-5/6 h-full p-6 gap-6">
          <h1 className="text-3xl font-bold text-gray-800">📊 Dane produktów</h1>

          {/* Tabela danych */}
          <div className="p-8">
            {data && <StoredProductsTable data={data.model} />}
          </div>
        </div>
      </div>
    </div>
  );
}
