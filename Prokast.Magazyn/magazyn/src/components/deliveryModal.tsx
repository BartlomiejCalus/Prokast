'use client';

import React, { useState } from "react";
import axios from "axios";

interface DeliveryModalProps {
  open: boolean;
  onClose: () => void;
  onSaved: () => void;
  productId: number | null;
}

export default function DeliveryModal({ open, onClose, onSaved, productId }: DeliveryModalProps) {
  const [amount, setAmount] = useState<number>(0);

  if (!open || productId === null) return null;

  const getAuthHeader = () => {
    const token = document.cookie
      .split("; ")
      .find((row) => row.startsWith("token="))
      ?.split("=")[1];

    return token ? `Bearer ${token}` : "";
  };

  const handleSave = async () => {
    if (amount <= 0) {
      alert("Podaj poprawną ilość (większą niż 0).");
      return;
    }

    try {
      await axios.put(
        `/api/quantityEdit/${productId}`,
        {},
        {
          params: { quantity: amount },
          headers: {
            Authorization: getAuthHeader(),
            Accept: "*/*"
          }
        }
      );

      onSaved(); // 🔥 ODŚWIEŻ TABELĘ + zamknij modal z rodzica

    } catch (err) {
      console.error("Błąd UPDATE:", err);
      alert("Błąd podczas aktualizacji ilości.");
    }
  };

  return (
    <div className="fixed inset-0 bg-black/40 backdrop-blur-sm flex items-center justify-center z-50">
      <div className="bg-white w-1/3 p-6 rounded-2xl shadow-xl border border-gray-200">
        <h2 className="text-2xl font-bold text-gray-800 mb-4">📦 Dostawa</h2>

        <p className="text-gray-700 mb-2">
          ID produktu: <b>{productId}</b>
        </p>

        <input
          type="number"
          value={amount}
          onChange={(e) => setAmount(Number(e.target.value))}
          placeholder="Podaj ilość dostawy"
          className="w-full border border-gray-300 rounded-lg px-3 py-2 mb-6"
        />

        <div className="flex justify-end gap-4">
          <button
            onClick={onClose}
            className="px-4 py-2 rounded-lg border-2 border-gray-400 text-gray-700 hover:bg-gray-100 transition"
          >
            Anuluj
          </button>

          <button
            onClick={handleSave}
            className="px-4 py-2 rounded-lg border-2 border-blue-600 text-blue-600 font-semibold hover:bg-blue-600 hover:text-white transition"
          >
            Zapisz
          </button>
        </div>
      </div>
    </div>
  );
}
