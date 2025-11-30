import Navbar from "../Components/Navbar";
import { useEffect, useState } from "react";
import { Warehouse } from "../models/Warehouse";
import axios from "axios";
import Cookies from "js-cookie";
import { useNavigate, useParams } from "react-router-dom";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { FaUndo } from "react-icons/fa";
import StoredProduct from "../Components/StoredProductList";
import StoredProductList from "../Components/StoredProductList";
import { StoredProductUpdate } from "../models/StoredProduct/StoredProductUpdate";

const API_URL = process.env.REACT_APP_API_URL;

const WarehouseEdit = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [warehouse, setWarehouse] = useState<Warehouse | null>(null);

  const [isDeleteOpen, setIsDeleteOpen] = useState(false);
  const [isStoredProductsOpen, setIsStoredProductsOpen] = useState(false);

  const warehouseSchema = yup.object().shape({
    id: yup.number().required(),
    name: yup.string().required("Nazwa magazynu jest wymagana"),
    address: yup.string().required("Adres jest wymagany"),
    postalCode: yup
      .string()
      .required("Kod pocztowy jest wymagany")
      .matches(/^\d{2}-\d{3}$/, "Kod pocztowy musi mieć format 00-000"),
    city: yup.string().required("Miasto jest wymagane"),
    country: yup.string().required("Kraj jest wymagany"),
    phoneNumber: yup
      .string()
      .required("Numer telefonu jest wymagany")
      .matches(/^\d{3}-\d{3}-\d{3}$/, "Numer musi mieć format 000-000-000"),
  });

  const {
    register,
    handleSubmit,
    setValue,
    reset,
    formState: { errors },
  } = useForm<Warehouse>({
    resolver: yupResolver(warehouseSchema),
    defaultValues: {
      id: 0,
      name: "",
      address: "",
      postalCode: "",
      city: "",
      country: "",
      phoneNumber: "",
      storedProducts: [],
    },
  });

  const formatPhone = (value: string) => {
    const digits = value.replace(/\D/g, "").slice(0, 9);
    return digits.replace(/(\d{3})(?=\d)/g, "$1-");
  };

  const formatPostalCode = (value: string) => {
    const digits = value.replace(/\D/g, "").slice(0, 5);
    if (digits.length >= 3) {
      return digits.slice(0, 2) + "-" + digits.slice(2);
    }
    return digits;
  };

  const fetchWarehouseData = async () => {
    const token = Cookies.get("token");

    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }

    axios
      .get(`${API_URL}/Api/Warehouses/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
      })

      .then((res) => {
        console.log("🔵 Odpowiedź z API:", res.data.model);
        setWarehouse(res.data.model);
        reset(res.data.model);
      })
      .catch((err) => {
        console.error("Błąd przy pobieraniu danych:", err);
      });
  };

  const onAddStoredProduct = async (
    data: StoredProductUpdate,
    warehouseId: string | null
  ) => {
    const token = Cookies.get("token");
    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }

    console.log("Dodawany produkt:", data, "do magazynu:", warehouseId);
    await axios.post(`${API_URL}/api/storedproducts/${warehouseId}`, data, {
      headers: {
        Authorization: `Bearer ${token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    });
    alert("Dodano produkt do magazynu!");
  };

  const onUpdateStoredProduct = async (data: StoredProductUpdate) => {
    const token = Cookies.get("token");
    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }
    await axios.put(`${API_URL}/api/storedproducts/storedproducts`, data, {
      headers: {
        Authorization: `Bearer ${token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    });
    alert("Zaktualizowano produkt w magazynie!");
  };

  const onDeleteStoredProduct = async (storedProductId: number) => {
    const token = Cookies.get("token");
    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }
    await axios.delete(`${API_URL}/api/storedproducts/${storedProductId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    });
    alert("Usunięto produkt z magazynu!");
  };

  useEffect(() => {
    fetchWarehouseData();
  }, []);

  return (
    <div className="min-h-screen flex flex-col bg-gradient-to-br from-green-100 via-white to-green-200">
      <Navbar />
      <main className="flex flex-col items-center justify-center w-screen mt-10">
        <div className="w-full max-w-2xl p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-3">
          <h1 className="text-2xl font-bold mb-4">Edycja magazynu</h1>
          <div>
            <div className="flex items-center gap-1">
              <h2 className="">Nazwa</h2>
              <button
                type="button"
                onClick={() => setValue("name", warehouse?.name || "")}
                className="px-2 text-sm bg-white hover:bg-gray-300 rounded-lg"
              >
                <FaUndo />
              </button>
            </div>
            <input
              type="text"
              defaultValue={warehouse?.name}
              {...register("name")}
              className="w-full p-2 border rounded-xl shadow-md"
            />
            {errors.name && (
              <p className="text-red-500 text-sm mt-1">{errors.name.message}</p>
            )}
          </div>

          <div>
            <div className="flex items-center gap-1">
              <h2 className="">Adres</h2>
              <button
                type="button"
                onClick={() => setValue("address", warehouse?.address || "")}
                className="px-2 text-sm bg-white hover:bg-gray-300 rounded-lg"
              >
                <FaUndo />
              </button>
            </div>
            <input
              type="text"
              defaultValue={warehouse?.address}
              {...register("address")}
              className="w-full p-2 border rounded-xl shadow-md"
            />
            {errors.address && (
              <p className="text-red-500 text-sm mt-1">
                {errors.address.message}
              </p>
            )}
          </div>

          <div className="grid grid-cols-3 gap-4">
            <div className="col-span-2">
              <div className="flex items-center gap-1">
                <h2 className="">Miasto</h2>
                <button
                  type="button"
                  onClick={() => setValue("city", warehouse?.city || "")}
                  className="px-2 text-sm bg-white hover:bg-gray-300 rounded-lg"
                >
                  <FaUndo />
                </button>
              </div>
              <input
                type="text"
                defaultValue={warehouse?.city}
                {...register("city")}
                className="w-full p-2 border rounded-xl shadow-md"
              />
              {errors.city && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.city.message}
                </p>
              )}
            </div>

            <div>
              <div className="flex items-center gap-1">
                <h2 className="">Kod pocztowy</h2>
                <button
                  type="button"
                  onClick={() =>
                    setValue(
                      "postalCode",
                      formatPostalCode(warehouse?.postalCode || "")
                    )
                  }
                  className="px-2 text-sm bg-white hover:bg-gray-300 rounded-lg"
                >
                  <FaUndo />
                </button>
              </div>
              <input
                type="text"
                {...register("postalCode")}
                onChange={(e) => {
                  const formatted = formatPostalCode(e.target.value);
                  setValue("postalCode", formatted);
                }}
                className="w-full p-2 border rounded-xl shadow-md"
              />

              {errors.postalCode && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.postalCode.message}
                </p>
              )}
            </div>
          </div>

          <div>
            <div className="flex items-center gap-1">
              <h2 className="">Kraj</h2>
              <button
                type="button"
                onClick={() => setValue("country", warehouse?.country || "")}
                className="px-2 text-sm bg-white hover:bg-gray-300 rounded-lg"
              >
                <FaUndo />
              </button>
            </div>
            <input
              type="text"
              defaultValue={warehouse?.country}
              {...register("country")}
              className="w-full p-2 border rounded-xl shadow-md"
            />
            {errors.country && (
              <p className="text-red-500 text-sm mt-1">
                {errors.country.message}
              </p>
            )}
          </div>
          <div className="grid grid-cols-2">
            <div className="col-span-1">
              <div className="flex items-center gap-1">
                <h2 className="">Numer telefonu</h2>
                <button
                  type="button"
                  onClick={() =>
                    setValue(
                      "phoneNumber",
                      formatPhone(warehouse?.phoneNumber || "")
                    )
                  }
                  className="px-2 text-sm bg-white hover:bg-gray-300 rounded-lg"
                >
                  <FaUndo />
                </button>
              </div>
              <input
                type="text"
                {...register("phoneNumber")}
                onChange={(e) => {
                  const formatted = formatPhone(e.target.value);
                  setValue("phoneNumber", formatted);
                }}
                className="w-full p-2 border rounded-xl shadow-md"
              />
              {errors.phoneNumber && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.phoneNumber.message}
                </p>
              )}
            </div>
            <div className="col-span-1 flex space-x-4 justify-end items-end">
              <button
                className="px-6 py-2 bg-green-500 text-white rounded-xl shadow-md hover:bg-green-600"
                onClick={handleSubmit(async (data) => {
                  console.log("Dane do wysłania:", data);
                  const token = Cookies.get("token");
                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios.put(`${API_URL}/Api/Warehouses/${id}`, data, {
                    headers: {
                      Authorization: `Bearer ${token}`,
                      Accept: "application/json",
                      "Content-Type": "application/json",
                    },
                  });
                  alert("Zaktualizowanyo magazyn!");
                  fetchWarehouseData();
                  navigate("/WarehouseList");
                })}
              >
                Zapisz
              </button>
              <button
                className="px-6 py-2 bg-red-500 text-white rounded-xl shadow-md hover:bg-red-600"
                onClick={() => setIsDeleteOpen(true)}
              >
                Usuń
              </button>
            </div>
          </div>
          <button
            className="w-full px-4 py-2 bg-blue-500 text-white rounded-xl shadow-md hover:bg-blue-600"
            onClick={() => setIsStoredProductsOpen(!isStoredProductsOpen)}
          >
            {isStoredProductsOpen
              ? "Ukryj produkty w magazynie"
              : "Pokaż produkty w magazynie"}
          </button>
          {isStoredProductsOpen && id && (
            <StoredProductList
              data={warehouse?.storedProducts || []}
              warehouseId={id}
              onAdd={onAddStoredProduct}
              onDelete={onDeleteStoredProduct}
              onUpdate={onUpdateStoredProduct}
            />
          )}
        </div>
      </main>

      {isDeleteOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Potwierdzenie usunięcia
            </h2>
            <p>Czy na pewno chcesz usunąć {warehouse?.name}?</p>
            <div className="flex justify-end gap-3 pt-2">
              <button
                type="button"
                onClick={async () => {
                  const token = Cookies.get("token");

                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios.delete(`${API_URL}/Api/Warehouses/${id}`, {
                    headers: {
                      Authorization: `Bearer ${token}`,
                    },
                  });

                  alert("Usunięto magazyn!");
                  setIsDeleteOpen(false);
                  fetchWarehouseData();
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
    </div>
  );
};

export default WarehouseEdit;
