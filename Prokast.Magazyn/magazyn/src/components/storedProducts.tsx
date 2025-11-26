'use client';

import React, { useState } from "react";
import { storedProduct } from "@/models/Products";
import DeliveryModal from "@/components/deliveryModal";
import { useRouter } from "next/navigation";

interface StoredProductsTableProps {
  data: storedProduct[];
}

export default function StoredProductsTable({ data }: StoredProductsTableProps) {
  const [deliveryOpen, setDeliveryOpen] = useState(false);
  const [selectedProductId, setSelectedProductId] = useState<number | null>(null);

  const router = useRouter();

  const openDelivery = (id: number) => {
    setSelectedProductId(id);
    setDeliveryOpen(true);
  };

  const handleSaved = () => {
    router.refresh();       // 🔥 ODSWIEŻENIE STRONY
    setDeliveryOpen(false); // zamknięcie modala
  };

  return (
    <div className="bg-white shadow-lg rounded-2xl overflow-hidden border border-gray-200">
      <div className="grid grid-cols-6 bg-gray-100 text-gray-700 font-semibold p-4">
        <p>Produkt</p>
        <p>Magazyn</p>
        <p>Ilość</p>
        <p>Status</p>
        <p className="col-span-2 text-center">Akcje</p>
      </div>

      {data.map((row, i) => (
        <div
          key={row.id}
          className={`grid grid-cols-6 items-center p-4 ${
            i % 2 === 0 ? "bg-gray-50" : "bg-white"
          } hover:bg-blue-50 transition`}
        >
          <p>{row.productName}</p>
          <p>{row.warehouseID}</p>
          <p>{row.quantity}</p>

          <p>
            {row.quantity < row.minQuantity
              ? "Niski stan"
              : row.quantity > row.maxQuantity
              ? "Za dużo"
              : "OK"}
          </p>

          <div className="col-span-2 flex justify-center gap-4">
            <button
              className="border-2 border-blue-600 text-blue-600 px-4 py-2 rounded-lg font-semibold hover:bg-blue-600 hover:text-white transition"
              onClick={() => openDelivery(row.id)}
            >
              Dostawa
            </button>

            <button className="border-2 border-red-600 text-red-600 px-4 py-2 rounded-lg font-semibold hover:bg-red-600 hover:text-white transition">
              Usuń
            </button>
          </div>
        </div>
      ))}

      <DeliveryModal
        open={deliveryOpen}
        onClose={() => setDeliveryOpen(false)}
        onSaved={handleSaved}
        productId={selectedProductId}
      />
    </div>
  );
}
