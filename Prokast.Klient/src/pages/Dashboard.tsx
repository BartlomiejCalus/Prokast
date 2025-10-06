import React from "react";
import Navbar from "../Components/Navbar";
import {
  PieChart,
  Pie,
  Cell,
  Tooltip,
  ResponsiveContainer,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
} from "recharts";

const Dashboard: React.FC = () => {
  const warehouses = 5;
  const productsCount = 120;

  const warehouseUsage = [
    { name: "Magazyn A", value: 40 },
    { name: "Magazyn B", value: 25 },
    { name: "Magazyn C", value: 20 },
    { name: "Magazyn D", value: 15 },
  ];

  const productStats = [
    { name: "Produkt A", qty: 30 },
    { name: "Produkt B", qty: 50 },
    { name: "Produkt C", qty: 20 },
    { name: "Produkt D", qty: 20 },
  ];

  const COLORS = ["#3b82f6", "#22c55e", "#f59e0b", "#ef4444"];

  return (
    <div className="min-w-full min-h-screen text-center bg-gradient-to-br from-blue-100 via-white to-blue-200 p-6">
      <Navbar />
      <h1 className="text-3xl font-bold mt-6">📊 Panel użytkownika</h1>

      {/* Karty */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mt-8">
        <div className="bg-white rounded-2xl shadow-lg p-6">
          <h2 className="text-xl font-semibold">Magazyny</h2>
          <p className="text-3xl font-bold mt-2">{warehouses}</p>
        </div>

        <div className="bg-white rounded-2xl shadow-lg p-6">
          <h2 className="text-xl font-semibold">Produkty</h2>
          <p className="text-3xl font-bold mt-2">{productsCount}</p>
        </div>

        <div className="bg-white rounded-2xl shadow-lg p-6">
          <h2 className="text-xl font-semibold">Top produkt</h2>
          <p className="text-lg mt-2">Produkt B (50 szt.)</p>
        </div>

        <div className="bg-white rounded-2xl shadow-lg p-6">
          <h2 className="text-xl font-semibold">Ostatnia dostawa</h2>
          <p className="text-lg mt-2">Produkt C - 20 szt.</p>
        </div>
      </div>

      {/* Wykresy */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-8 mt-12">
        <div className="bg-white rounded-2xl shadow-lg p-6">
          <h2 className="text-xl font-semibold mb-4">Zajętość magazynów</h2>
          <ResponsiveContainer width="100%" height={250}>
            <PieChart>
              <Pie
                data={warehouseUsage}
                dataKey="value"
                nameKey="name"
                cx="50%"
                cy="50%"
                outerRadius={90}
                label
              >
                {warehouseUsage.map((entry, index) => (
                  <Cell
                    key={`cell-${index}`}
                    fill={COLORS[index % COLORS.length]}
                  />
                ))}
              </Pie>
              <Tooltip />
            </PieChart>
          </ResponsiveContainer>
        </div>

        <div className="bg-white rounded-2xl shadow-lg p-6">
          <h2 className="text-xl font-semibold mb-4">Ilość produktów</h2>
          <ResponsiveContainer width="100%" height={250}>
            <BarChart data={productStats}>
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="name" />
              <YAxis />
              <Tooltip />
              <Bar dataKey="qty" fill="#3b82f6" radius={[6, 6, 0, 0]} />
            </BarChart>
          </ResponsiveContainer>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
