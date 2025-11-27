import { useNavigate } from "react-router-dom";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { Warehouse } from "../models/Warehouse";
import Cookies from "js-cookie";
import Navbar from "../Components/Navbar";
import { useState } from "react";
import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

const AddWarehouse = () => {
  const navigate = useNavigate();

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

  return (
    <div className="min-h-screen flex flex-col bg-gradient-to-br from-green-100 via-white to-green-200">
      <Navbar />
      <main className="flex flex-col items-center justify-center w-screen mt-10">
        <div className="w-full max-w-2xl p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-3">
          <h1 className="text-2xl font-bold mb-4">Dodaj magazyn</h1>
          <div>
            <h2 className="mb-1">Nazwa magazynu</h2>
            <input
              type="text"
              {...register("name")}
              className="w-full p-2 border rounded-xl shadow-md"
            />
            {errors.name && (
              <p className="text-red-500 text-sm mt-1">{errors.name.message}</p>
            )}
          </div>

          <div>
            <h2 className="mb-1">Adres</h2>
            <input
              type="text"
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
              <h2 className="mb-1">Miasto</h2>
              <input
                type="text"
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
              <h2 className="mb-1">Kod pocztowy</h2>
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
            <h2 className="mb-1">Kraj</h2>
            <input
              type="text"
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
              <h2 className="mb-1">Numer telefonu</h2>
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

                  await axios.post(`${API_URL}/Api/Warehouses`, data, {
                    headers: {
                      Authorization: `Bearer ${token}`,
                      Accept: "application/json",
                      "Content-Type": "application/json",
                    },
                  });
                  alert("Zaktualizowanyo magazyn!");
                  navigate("/dashboard"); //TODOO: change route to warehouses list
                })}
              >
                Dodaj
              </button>
            </div>
          </div>
    
        </div>
      </main>
    </div>
  );
};
export default AddWarehouse;
