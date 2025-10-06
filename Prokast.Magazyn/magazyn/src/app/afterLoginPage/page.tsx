import ChoiceMenu from "@/components/choiceMenu";
import React from "react";

export default function AfterLogin() {
  const mockData = [
    { id: 1, col1: "Produkt A", col2: "Magazyn 1", col3: "20 szt.", col4: "Aktywny" },
    { id: 2, col1: "Produkt B", col2: "Magazyn 2", col3: "50 szt.", col4: "Aktywny" },
    { id: 3, col1: "Produkt C", col2: "Magazyn 3", col3: "15 szt.", col4: "Nieaktywny" },
    { id: 4, col1: "Produkt D", col2: "Magazyn 1", col3: "40 szt.", col4: "Aktywny" },
  ];

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
          <div className="bg-white shadow-lg rounded-2xl overflow-hidden border border-gray-200">
            <div className="grid grid-cols-6 bg-gray-100 text-gray-700 font-semibold p-4">
              <p>Produkt</p>
              <p>Magazyn</p>
              <p>Ilość</p>
              <p>Status</p>
              <p className="col-span-2 text-center">Akcje</p>
            </div>

            {mockData.map((row, i) => (
              <div
                key={row.id}
                className={`grid grid-cols-6 items-center p-4 ${
                  i % 2 === 0 ? "bg-gray-50" : "bg-white"
                } hover:bg-blue-50 transition`}
              >
                <p>{row.col1}</p>
                <p>{row.col2}</p>
                <p>{row.col3}</p>
                <p>{row.col4}</p>
                <div className="col-span-2 flex justify-center gap-4">
                  <button className="border-2 border-blue-600 text-blue-600 px-4 py-2 rounded-lg font-semibold hover:bg-blue-600 hover:text-white transition">
                    Edytuj
                  </button>
                  <button className="border-2 border-red-600 text-red-600 px-4 py-2 rounded-lg font-semibold hover:bg-red-600 hover:text-white transition">
                    Usuń
                  </button>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
