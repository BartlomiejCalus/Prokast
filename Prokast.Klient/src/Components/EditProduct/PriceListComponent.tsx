import React, { useEffect, useState } from "react";
import { PriceList } from "../../models/PriceList";
import Cookies from "js-cookie";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { FaEdit, FaPlus, FaTrash } from "react-icons/fa";
import { Price } from "../../models/Price";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { Console } from "console";

const API_URL = process.env.REACT_APP_API_URL;

const PriceListComponent = ({
  data,
  productId,
}: {
  data: PriceList;
  productId: string | undefined;
}) => {
  const [isUpdateOpen, setIsUpdateOpen] = useState<boolean>(false);
  const [isAddOpen, setIsAddOpen] = useState<boolean>(false);

  const [regions, setRegions] = useState<{ id: number; name: string }[]>([]);

  const [selectedPrice, setSelectedPrice] = useState<number | null>(null);

  const [newPrice, setNewPrice] = useState<Price>({
    name: "",
    regionID: 0,
    netto: 0,
    vat: 0,
    brutto: 0,
  });

  useEffect(() => {
    const token = Cookies.get("token");

    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }

    axios
      .get(`${API_URL}/api/others`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
      })
      .then((res) => setRegions(res.data.model));
  }, []);

  const priceSchema = yup.object().shape({
    name: yup.string().required("Nazwa jest wymagana"),
    netto: yup.number().positive("Musi być większe od 0").required(),
    brutto: yup.number().positive("Musi być większe od 0").required(),
    vat: yup.number().min(0).max(100).required("VAT 0-100%"),
    regionID: yup.number().required("Wybierz region"),
  });

  const {
    register,
    handleSubmit,
    watch,
    setValue,
    formState: { errors },
  } = useForm<Price>({
    resolver: yupResolver(priceSchema),
    defaultValues: {
      name: "",
      regionID: 0,
      netto: 0,
      vat: 23,
      brutto: 0,
    },
  });

  const brutto = watch("brutto");
  const vat = watch("vat");

  useEffect(() => {
    if (brutto > 0 && vat >= 0) {
      const netto = brutto / (1 + vat / 100);
      setValue("netto", parseFloat(netto.toFixed(2)));
    }
  }, [brutto, vat, setValue]);

  return (
    <div className="mt-4 p-4 border rounded-xl bg-white/70 shadow-md w-full">
      <table className="w-full mb-4">
        <thead>
          <tr>
            <th className="text-left p-2 border-b">Nazwa ceny</th>
            <th className="text-left p-2 border-b">Cena brutto</th>
            <th className="text-left p-2 border-b">Akcje</th>
          </tr>
        </thead>
        <tbody>
          {data.prices.map((price, index) => (
            <tr key={index}>
              <td className="p-2 border-b">{price.name}</td>
              <td className="p-2 border-b">{price.brutto}</td>
              <td className="p-2 border-b">
                <button type="button" className="mr-2" onClick={() => setIsUpdateOpen(true)}>
                  <FaEdit />
                </button>
                <button type="button" onClick={() => setIsUpdateOpen(true)}>
                  <FaTrash className="text-red-700"/>
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <button
        type="button"
        onClick={() => setIsAddOpen(true)}
        className="bg-blue-600 rounded-full p-2 text-white hover:bg-blue-700"
      >
        <FaPlus />
      </button>

      {isAddOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Dodaj nową cenę
            </h2>

            {/* Nazwa */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Nazwa ceny
              </label>
              <input
                {...register("name")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.name && (
                <p className="text-red-500 text-sm">{errors.name.message}</p>
              )}
            </div>

            {/* Region */}
            <div>
              <label className="block text-sm font-medium mb-1">Region</label>
              <select
                {...register("regionID")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              >
                <option value="">-- wybierz --</option>
                {regions.map((r) => (
                  <option key={r.id} value={r.id}>
                    {r.name}
                  </option>
                ))}
              </select>
              {errors.regionID && (
                <p className="text-red-500 text-sm">
                  {errors.regionID.message}
                </p>
              )}
            </div>

            {/* Brutto*/}
            <div>
              <label className="block text-sm font-medium mb-1">
                Cena brutto
              </label>
              <input
                type="number"
                step="0.01"
                {...register("brutto")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.brutto && (
                <p className="text-red-500 text-sm">{errors.brutto.message}</p>
              )}
            </div>

            {/* VAT*/}
            <div>
              <label className="block text-sm font-medium mb-1">VAT (%)</label>
              <input
                type="number"
                {...register("vat")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.vat && (
                <p className="text-red-500 text-sm">{errors.vat.message}</p>
              )}
            </div>

            {/* Netto*/}
            <div>
              <label className="block text-sm font-medium mb-1">
                Cena netto (liczona)
              </label>
              <input
                type="number"
                step="0.01"
                {...register("netto")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500 bg-gray-100"
                disabled
              />
            </div>

            {/* Buttons */}
            <div className="flex justify-end gap-3 pt-2">
              <button
                onClick={handleSubmit(async (updatedata) => {
                  console.log("Submitting new price:", updatedata);
                  console.log("For product ID:", productId);

                  const token = Cookies.get("token");

                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios.post(
                    `${API_URL}/api/priceLists/${productId}`,
                    updatedata,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`,
                        Accept: "application/json",
                        "Content-Type": "application/json",
                      },
                    }
                  );
                  alert("Dodano cenę!");
                  setIsAddOpen(false);
                })}
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

      {/* Delete Price Modal */}
      {isDeleteOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Potwierdzenie usunięcia
            </h2>
            <p>Czy na pewno chcesz usunąć tę cenę?</p>
            <div className="flex justify-end gap-3 pt-2">
              <button
                onClick={async () =>{

                  const token = Cookies.get("token");

                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios.delete(
                    `${API_URL}/api/priceLists/prices/${selectedPrice?.id}`,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`
                      },
                    }
                  );

                  alert("Usunięto cenę!");
                  setIsDeleteOpen(false);
                  navigate(`/EditProducts/${productId}`);
                }}
                className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
              >
                Tak
              </button>
              <button
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

export default PriceListComponent;
