import { useEffect, useState } from "react";
import { PriceList } from "../../models/PriceList";
import Cookies from "js-cookie";
import axios from "axios";
import { FaEdit, FaPlus, FaTrash } from "react-icons/fa";
import { Price } from "../../models/Price";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";

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
  const [isDeleteOpen, setIsDeleteOpen] = useState<boolean>(false);

  const [toEditablePrice, setToEditablePrice] = useState<boolean>(false);

  const [selectedPrice, setSelectedPrice] = useState<Price | null>(null);

  const [regions, setRegions] = useState<{ id: number; name: string }[]>([]);

  const [prices, setPrices] = useState<Price[]>(data.prices);

  //#region get regions
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
  //#endregion

  const priceSchema = yup.object().shape({
    id: yup.number().required(),
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
    reset,
    formState: { errors },
  } = useForm<Price>({
    resolver: yupResolver(priceSchema),
    defaultValues: {
      id: 0,
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

  useEffect(() => {
    if (isUpdateOpen && selectedPrice) {
      reset(selectedPrice);
    }
  }, [isUpdateOpen, selectedPrice, reset]);

  useEffect(() => {
    if (isAddOpen) {
      reset({
        id: 0,
        name: "",
        regionID: 1,
        netto: 0,
        vat: 23,
        brutto: 0,
      });
    }
  }, [isAddOpen, reset]);

  const fetchPrices = () => {
    const token = Cookies.get("token");

    axios
      .get(`${API_URL}/api/priceLists/prices/byProduct/${productId}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((res) => setPrices(res.data.model));
  };

  return (
    <div className="mt-4 p-4 border rounded-xl bg-white/70 shadow-md w-full">

        <button
        type="button"
        onClick={() => setIsAddOpen(true)}
        className="group flex items-center justify-center
            bg-blue-600 text-white rounded-full
            h-10 w-10
            hover:w-40
            transition-all duration-300
            overflow-hidden
            hover:bg-blue-700"
      >
        <FaPlus className="w-5 h-5 flex-shrink-0" />
        <span
          className="ml-2
            whitespace-nowrap
            opacity-0 max-w-0
            group-hover:opacity-100
            group-hover:max-w-[200px]
            transition-all duration-300"
        >
          Dodaj cenę
        </span>
      </button>

      <table className="w-full mb-4">
        <thead>
          <tr>
            <th className="text-left p-2 border-b">Nazwa ceny</th>
            <th className="text-left p-2 border-b">Cena brutto</th>
            <th className="text-left p-2 border-b">Akcje</th>
          </tr>
        </thead>
        <tbody>
          {prices.map((price, index) => (
            <tr key={index}>
              <td className="p-2 border-b">{price.name}</td>
              <td className="p-2 border-b">{price.brutto}</td>
              <td className="p-2 border-b">
                <button
                  type="button"
                  className="mr-2"
                  onClick={() => {
                    setSelectedPrice(price);
                    setIsUpdateOpen(true);
                  }}
                >
                  <FaEdit />
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setSelectedPrice(price);
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

      {/*Add Price Modal */}
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
                onClick={handleSubmit(async (newData) => {
                  const token = Cookies.get("token");

                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios.post(
                    `${API_URL}/api/priceLists/${productId}`,
                    newData,
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
                  fetchPrices();
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
            <p>Czy na pewno chcesz usunąć {selectedPrice?.name}?</p>
            <div className="flex justify-end gap-3 pt-2">
              <button
                type="button"
                onClick={async () => {
                  const token = Cookies.get("token");

                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios.delete(
                    `${API_URL}/api/priceLists/prices/${selectedPrice?.id}`,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`,
                      },
                    }
                  );

                  alert("Usunięto cenę!");
                  setIsDeleteOpen(false);
                  fetchPrices();
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

      {/* Update Price Modal */}
      {isUpdateOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Edytuj cenę
            </h2>

            {/* Nazwa */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Nazwa ceny
              </label>
              <input
                {...register("name")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditablePrice ? "bg-gray-100" : ""
                }`}
                disabled={!toEditablePrice}
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
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditablePrice ? "bg-gray-100" : ""
                }`}
                disabled={!toEditablePrice}
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
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditablePrice ? "bg-gray-100" : ""
                }`}
                disabled={!toEditablePrice}
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
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditablePrice ? "bg-gray-100" : ""
                }`}
                disabled={!toEditablePrice}
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
              {toEditablePrice === true ? (
                <button
                  type="button"
                  onClick={handleSubmit(async (updateData) => {
                    const token = Cookies.get("token");

                    console.log(updateData);

                    if (!token) {
                      console.error("Brak tokenu autoryzacyjnego.");
                      return;
                    }

                    await axios.put(
                      `${API_URL}/api/priceLists/prices/${selectedPrice?.id}`,
                      updateData,
                      {
                        headers: {
                          Authorization: `Bearer ${token}`,
                          Accept: "application/json",
                          "Content-Type": "application/json",
                        },
                      }
                    );
                    setIsUpdateOpen(false);
                    setToEditablePrice(false);
                    fetchPrices();
                  })}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Zapisz
                </button>
              ) : (
                <button
                  type="button"
                  onClick={() => setToEditablePrice(true)}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Włącz edytowanie
                </button>
              )}

              <button
                type="button"
                onClick={() => {
                  setIsUpdateOpen(false);
                  setToEditablePrice(false);
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

export default PriceListComponent;
