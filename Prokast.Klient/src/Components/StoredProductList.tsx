import axios from "axios";
import Cookies from "js-cookie";
import { useEffect, useState } from "react";
import { StoredProduct } from "../models/StoredProduct/StoredProduct";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { FaEdit, FaPlus, FaTrash } from "react-icons/fa";
import { StoredProductUpdate } from "../models/StoredProduct/StoredProductUpdate";
import { on } from "events";

const API_URL = process.env.REACT_APP_API_URL;

interface StoredProductListProps {
  data: StoredProduct[];
  warehouseId: string | null;
  onUpdate?: (data: StoredProductUpdate) => void;
  onDelete?: (sProductId: number) => void;
  onAdd?: (data: StoredProductUpdate, warehouseId: string | null) => void;
}

type ProductToAdd = {
  id: number;
  sku: string;
};

const StoredProductList = ({
  data,
  warehouseId,
  onUpdate,
  onDelete,
  onAdd,
}: StoredProductListProps) => {
  const [storedProducts, setStoredProducts] = useState<StoredProduct[]>(data);

  const [isUpdateOpen, setIsUpdateOpen] = useState(false);
  const [isDeleteOpen, setIsDeleteOpen] = useState(false);
  const [isAddOpen, setIsAddOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<StoredProduct | null>(
    null
  );
  const [prodToAdd, setProdToAdd] = useState<ProductToAdd[]>([]);

  const [toEditableParam, setToEditableParam] = useState(false);

  const paramSchema = yup.object().shape({
    quantity: yup.number().required("Ilość jest wymagana"),
    minQuantity: yup.number().required("Minimalna ilość jest wymagana"),
    productId: yup.number().required("Produkt jest wymagany"),
  });

  const {
    register,
    handleSubmit,
    watch,
    setValue,
    reset,
    getValues,
    formState: { errors },
  } = useForm<StoredProductUpdate>({
    resolver: yupResolver(paramSchema),
    defaultValues: {
      quantity: 0,
      minQuantity: 0,
      productId: 0,
    },
  });

  const fetchStoredProducts = async () => {
    const token = Cookies.get("token");

    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }

    if (!warehouseId) return;

    axios
      .get(`${API_URL}/api/storedproducts`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
        params: {
          warehouseID: warehouseId,
        },
      })

      .then((res) => {
        console.log("🔵 Odpowiedź z API:", res.data.model);
        setStoredProducts(res.data.model);
      })
      .catch((err) => {
        console.error("Błąd przy pobieraniu danych:", err);
      });
  };

  const fetchProducts = async () => {
    const token = Cookies.get("token");

    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }

    axios
      .get(`${API_URL}/Api/Warehouses/ProductToAdd/${warehouseId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
      })

      .then((res) => {
        console.log("🔵 Odpowiedź z API:", res.data.model);
        setProdToAdd(res.data.model);
      })
      .catch((err) => {
        console.error("Błąd przy pobieraniu danych:", err);
      });
  };

  useEffect(() => {
    if (isUpdateOpen && selectedProduct) {
      reset({
        quantity: selectedProduct.quantity,
        minQuantity: selectedProduct.minQuantity,
        productId: selectedProduct.id,
      });
    }
  }, [isUpdateOpen, selectedProduct, reset]);

  useEffect(() => {
    if (isAddOpen) {
      reset({
        quantity: 0,
        minQuantity: 0,
        productId: 0,
      });
    }
  }, [isAddOpen, reset]);

  useEffect(() => {
    fetchProducts();
  }, []);

  return (
    <div className="w-full p-2 border rounded-xl shadow-md">
      <button
        type="button"
        onClick={() => setIsAddOpen(true)}
        className="group flex items-center justify-center
                  bg-blue-600 text-white rounded-full
                  h-10 w-10
                  hover:w-64
                  transition-all duration-300
                  overflow-hidden
                  hover:bg-blue-700"
      >
        <FaPlus className="w-5 h-5 flex-shrink-0" />
        <span
          className="
                  whitespace-nowrap
                  opacity-0 max-w-0
                  group-hover:opacity-100
                  group-hover:max-w-[200px]
                  group-hover:ml-2
                  transition-all duration-300"
        >
          Dodaj produkt do magazynu
        </span>
      </button>
      <table className="w-full table-auto">
        <thead>
          <tr>
            <th className="text-left p-2 border-b">SKU</th>
            <th className="text-left p-2 border-b">Ilość</th>
            <th className="text-left p-2 border-b">Min. ilość</th>
            <th className="text-left p-2 border-b">Ostatnia Aktualizacja</th>
            <th className="text-left p-2 border-b">Akcje</th>
          </tr>
        </thead>
        <tbody>
          {storedProducts.map((product) => (
            <tr key={product.id}>
              <td className="p-2 border-b">{product.sku}</td>
              <td className="p-2 border-b">{product.quantity}</td>
              <td className="p-2 border-b">{product.minQuantity}</td>
              <td className="p-2 border-b">
                {new Date(product.lastUpdated).toLocaleString()}
              </td>
              <td className="p-2 border-b">
                <button
                  type="button"
                  className="mr-2"
                  onClick={() => {
                    setSelectedProduct(product);
                    setIsUpdateOpen(true);
                  }}
                >
                  <FaEdit />
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setSelectedProduct(product);
                    setIsDeleteOpen(true);
                  }}
                >
                  <FaTrash className="text-red-700" />
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      {isDeleteOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Potwierdzenie usunięcia
            </h2>
            <p>Czy na pewno chcesz usunąć {selectedProduct?.sku}?</p>
            <div className="flex justify-end gap-3 pt-2">
              <button
                type="button"
                onClick={async () => {
                  if (onDelete && selectedProduct) {
                    await onDelete(selectedProduct.id);
                  }
                  setIsDeleteOpen(false);
                  fetchStoredProducts();
                  fetchProducts();
                }}
                className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
              >
                Tak
              </button>
              <button
                type="button"
                onClick={() => setIsDeleteOpen(false)}
                className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700"
              >
                Nie
              </button>
            </div>
          </div>
        </div>
      )}

      {/*Add Stored Product Modal */}
      {isAddOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Dodaj nowy parametr
            </h2>

            {/* Ilość na magazynie */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Ilość na magazynie
              </label>
              <input
                {...register("quantity")}
                type="number"
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.quantity && (
                <p className="text-red-500 text-sm">
                  {errors.quantity.message}
                </p>
              )}
            </div>

            {/* Minimalna ilość */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Minimalna ilość
              </label>
              <input
                {...register("minQuantity")}
                type="number"
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.minQuantity && (
                <p className="text-red-500 text-sm">
                  {errors.minQuantity.message}
                </p>
              )}
            </div>

            {/* Produktu */}
            <div>
              <label className="block text-sm font-medium mb-1">Produktu</label>
              <select
                {...register("productId")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              >
                {prodToAdd.map(({ sku, id }) => (
                  <option key={id} value={id}>
                    {sku}
                  </option>
                ))}
              </select>
              {errors.productId && (
                <p className="text-red-500 text-sm">
                  {errors.productId.message}
                </p>
              )}
            </div>

            {/* Buttons */}
            <div className="flex justify-end gap-3 pt-2">
              <button
                onClick={async () => {
                  const form = getValues();

                  const data: StoredProductUpdate = {
                    productId: Number(form.productId),
                    quantity: Number(form.quantity),
                    minQuantity: Number(form.minQuantity),
                  };
                  if (onAdd && getValues()) {
                    await onAdd(data, warehouseId);
                  }
                  setIsAddOpen(false);
                  fetchStoredProducts();
                  fetchProducts();
                }}
                className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
              >
                Zapisz
              </button>

              <button
                onClick={() => setIsAddOpen(false)}
                className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
              >
                Zamknij
              </button>
            </div>
          </div>
        </div>
      )}

      {/*Edit Stored Product Modal */}
      {isUpdateOpen && selectedProduct && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Edytuj produkt w magazynie
            </h2>

            {/* Ilość na magazynie */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Ilość na magazynie
              </label>
              <input
                {...register("quantity")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableParam ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableParam}
              />
              {errors.quantity && (
                <p className="text-red-500 text-sm">
                  {errors.quantity.message}
                </p>
              )}
            </div>

            {/* Minimalna ilość */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Minimalna ilość
              </label>
              <input
                {...register("minQuantity")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableParam ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableParam}
              />
              {errors.minQuantity && (
                <p className="text-red-500 text-sm">
                  {errors.minQuantity.message}
                </p>
              )}
            </div>

            {/* Produkt */}
            <div>
              <label className="block text-sm font-medium mb-1">Produkt</label>
              <input
                {...register("productId")}
                type="text"
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableParam ? "bg-gray-100" : ""
                }`}
                value={selectedProduct?.sku}
                disabled
              />
              {errors.productId && (
                <p className="text-red-500 text-sm">
                  {errors.productId.message}
                </p>
              )}
            </div>

            {/* Buttons */}

            <div className="flex justify-end gap-3 pt-2">
              {toEditableParam === true ? (
                <button
                  type="button"
                  onClick={async () => {
                    const form = getValues();

                    const data: StoredProductUpdate = {
                      productId: Number(form.productId),
                      quantity: Number(form.quantity),
                      minQuantity: Number(form.minQuantity),
                    };
                    if (onUpdate && getValues()) {
                      await onUpdate(data);
                    }
                    setIsUpdateOpen(false);
                    setToEditableParam(false);
                    fetchStoredProducts();
                  }}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Zapisz
                </button>
              ) : (
                <button
                  type="button"
                  onClick={() => setToEditableParam(true)}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Włącz edytowanie
                </button>
              )}

              <button
                type="button"
                onClick={() => {
                  setIsUpdateOpen(false);
                  setToEditableParam(false);
                }}
                className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
              >
                Zamknij
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default StoredProductList;
