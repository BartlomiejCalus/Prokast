import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Cookies from 'js-cookie';
import Navbar from '../Components/Navbar';
import { useNavigate } from 'react-router-dom';
import { FaEdit, FaPlus, FaTrash } from "react-icons/fa";

const API_URL = process.env.REACT_APP_API_URL;

interface Warehouse {
    id: number;
    name: string;
    city: string;
    country: string;
}

const WarehouseList: React.FC = () => {
    const [warehouses, setWarehouses] = useState<Warehouse[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [searchTerm, setSearchTerm] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(10);
    const [isDeleteOpen, setIsDeleteOpen] = useState(false);
    const [warehouseToDelete, setWarehouseToDelete] = useState<Warehouse | null>(null);
    const navigate = useNavigate();

    const fetchWarehouses = async () => {
        try {
            setLoading(true);
            const token = Cookies.get("token");
            if (!token) {
                setError("Brak tokenu autoryzacyjnego.");
                setLoading(false);
                return;
            }

            const response = await axios.get(
                `${API_URL}/api/warehouses/Minimal`,
                {
                    headers: { Authorization: `Bearer ${token}`, Accept: "application/json" },
                    params: { pageNumber: currentPage, pageSize: itemsPerPage }
                }
            );

            const model = response.data?.model ?? response.data;
            const totalItems = response.data?.totalItems ?? 0;
            setTotalPages(Math.ceil(totalItems / itemsPerPage));

            if (!model || model.length === 0) {
                setError("Brak magazynów do wyświetlenia.");
                setWarehouses([]);
                return;
            }

            setWarehouses(
                model.map((item: any) => ({
                    id: item.id,
                    name: item.name,
                    city: item.city,
                    country: item.country
                }))
            );
            setError('');
        } catch (err) {
            setError("Nie udało się pobrać listy magazynów.");
        } finally {
            setLoading(false);
        }
    };

    const openDeleteDialog = (warehouse: Warehouse) => {
        setWarehouseToDelete(warehouse);
        setIsDeleteOpen(true);
    };

    const confirmDelete = async () => {
        if (!warehouseToDelete) return;
        try {
            const token = Cookies.get("token");
            if (!token) return;

            await axios.delete(`${API_URL}/api/warehouses/${warehouseToDelete.id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });

            setIsDeleteOpen(false);
            setWarehouseToDelete(null);
            fetchWarehouses();
        } catch (err) {
            console.error("Błąd podczas usuwania magazynu:", err);
            alert("Nie udało się usunąć magazynu.");
        }
    };

    useEffect(() => { fetchWarehouses(); }, [currentPage, itemsPerPage]);

    const filteredWarehouses = warehouses.filter((w) =>
        w.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const getPageNumbers = (current: number, total: number, visibleAround = 2) => {
        const pages: (number | string)[] = [];
        const start = Math.max(2, current - visibleAround);
        const end = Math.min(total - 1, current + visibleAround);

        pages.push(1);
        if (start > 2) pages.push('...');
        for (let i = start; i <= end; i++) pages.push(i);
        if (end < total - 1) pages.push('...');
        if (total > 1) pages.push(total);

        return pages;
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-green-100 via-white to-green-200">
            <Navbar />
            <div className="max-w-7xl mx-auto flex flex-col lg:flex-row mt-8 p-4 gap-6">

                <aside className="w-full lg:w-1/4 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 h-fit">
                    <h2 className="text-xl font-bold text-gray-800 mb-4">Filtry</h2>
                    <div className="mb-6">
                        <label className="block text-gray-600 mb-2 font-semibold">Szukaj magazynu</label>
                        <input
                            type="text"
                            placeholder="np. Magazyn"
                            value={searchTerm}
                            onChange={(e) => setSearchTerm(e.target.value)}
                            className="w-full p-3 border rounded-xl shadow-sm focus:ring focus:ring-blue-300"
                        />
                    </div>
                    <div className="mb-6">
                        <label className="block text-gray-600 mb-2 font-semibold">Ilość magazynów na stronę</label>
                        <select
                            value={itemsPerPage}
                            onChange={(e) => { setItemsPerPage(Number(e.target.value)); setCurrentPage(1); }}
                            className="w-full p-3 border rounded-xl shadow-sm focus:ring focus:ring-blue-300"
                        >
                            <option value={1}>1</option>
                            <option value={2}>2</option>
                            <option value={5}>5</option>
                            <option value={10}>10</option>
                            <option value={25}>25</option>
                        </select>
                    </div>
                    <div className="mt-6">
                        <button
                            onClick={fetchWarehouses}
                            className="w-full bg-blue-600 text-white font-semibold py-3 rounded-xl hover:bg-blue-700 transition"
                        >
                            Zastosuj filtry
                        </button>
                    </div>
                </aside>

                <main className="flex-1">
                    <div className="flex items-center justify-between mb-6">
                        <h1 className="text-2xl font-bold text-gray-800">Lista magazynów</h1>
                        <button
                            onClick={() => navigate("/AddWarehouse")}
                            className="flex items-center gap-2 bg-green-600 text-white px-4 py-2 rounded-xl hover:bg-green-700 transition"
                        >
                            <FaPlus /> Dodaj magazyn
                        </button>
                    </div>

                    {filteredWarehouses.length > 0 ? (
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                            {filteredWarehouses.map((warehouse) => (
                                <div
                                    key={warehouse.id}
                                    className="bg-white/80 backdrop-blur-md shadow-lg rounded-2xl p-6 hover:shadow-xl transition"
                                >
                                    <h2 className="text-2xl font-bold text-gray-800 mb-2">{warehouse.name}</h2>
                                    <div className="text-gray-600 mb-4">
                                        <p>{warehouse.city},</p>
                                        <p>{warehouse.country}</p>
                                    </div>
                                    <div className="flex gap-4 mt-6">
                                        <button
                                            onClick={() => openDeleteDialog(warehouse)}
                                            className="px-4 py-2 bg-red-600 text-white rounded-xl hover:bg-red-700 transition"
                                        >
                                            Usuń
                                        </button>
                                        <button
                                            className="px-4 py-2 bg-blue-600 text-white rounded-xl hover:bg-blue-700 transition"
                                            onClick={() => navigate(`/EditWarehouse/${warehouse.id}`)}
                                        >
                                            Edytuj
                                        </button>
                                    </div>
                                </div>
                            ))}
                        </div>
                    ) : (
                        <p className="text-gray-600 text-center mt-10">Brak magazynów do wyświetlenia.</p>
                    )}

                    <div className="flex justify-center mt-10 gap-2">
                        <button
                            disabled={currentPage === 1}
                            onClick={() => setCurrentPage(currentPage - 1)}
                            className="px-3 py-2 bg-gray-300 rounded disabled:opacity-50"
                        >←</button>

                        {getPageNumbers(currentPage, totalPages).map((page, idx) =>
                            typeof page === 'number' ? (
                                <button
                                    key={idx}
                                    onClick={() => setCurrentPage(page)}
                                    className={`px-3 py-2 rounded ${currentPage === page ? "bg-blue-600 text-white" : "bg-gray-200"}`}
                                >
                                    {page}
                                </button>
                            ) : (
                                <span key={idx} className="px-3 py-2">{page}</span>
                            )
                        )}

                        <button
                            disabled={currentPage === totalPages}
                            onClick={() => setCurrentPage(currentPage + 1)}
                            className="px-3 py-2 bg-gray-300 rounded disabled:opacity-50"
                        >→</button>
                    </div>
                </main>

                {isDeleteOpen && warehouseToDelete && (
                    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
                        <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
                            <h2 className="text-xl font-bold text-gray-800 mb-2">Potwierdzenie usunięcia</h2>
                            <p>Czy na pewno chcesz usunąć magazyn <strong>{warehouseToDelete.name}</strong>?</p>
                            <div className="flex justify-end gap-3 pt-2">
                                <button onClick={confirmDelete} className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700">Tak</button>
                                <button onClick={() => setIsDeleteOpen(false)} className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700">Nie</button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};

export default WarehouseList;