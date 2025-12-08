import React, { useEffect, useState } from "react";
import { AdditionalField } from "../../models/AdditionalField";
import Cookies from "js-cookie";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { FaEdit, FaPlus, FaTrash } from "react-icons/fa";
import { Price } from "../../models/Price";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { set, useForm } from "react-hook-form";
import { Console } from "console";
import { isEditable } from "@testing-library/user-event/dist/utils";

const API_URL = process.env.REACT_APP_API_URL;

const AdditionalDescriptionComponent = ({
  data,
  productId,
  onAdd,
}: {
  data: AdditionalField[];
  productId: string | undefined;
  onAdd: () => void;
}) => {
  const navigate = useNavigate();

  const [isUpdateOpen, setIsUpdateOpen] = useState<boolean>(false);
  const [isAddOpen, setIsAddOpen] = useState<boolean>(false);
  const [isDeleteOpen, setIsDeleteOpen] = useState<boolean>(false);

  const [toEditableDescription, setToEditableDescription] = useState<boolean>(false);

  const [selectedDescription, setSelectedDescription] = useState<AdditionalField | null>(null);

  const [regions, setRegions] = useState<{ id: number; name: string }[]>([]);

  const [descriptions, setDescriptions] = useState<AdditionalField[]>(data);

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

  const descriptionSchema = yup.object().shape({
    id: yup.number().required(),
    title: yup.string().required("Nazwa jest wymagana"),
    value: yup.string().required("Musi być większe od 0"),
    regionID: yup.number().required("Wybierz region"),
  });

  const {
    register,
    handleSubmit,
    watch,
    setValue,
    reset,
    formState: { errors },
  } = useForm<AdditionalField>({
    resolver: yupResolver(descriptionSchema),
    defaultValues: {
      id: 0,
      title: "",
      value: "",
      regionID: 0
    },
  });

  //const brutto = watch("brutto");
  //const vat = watch("vat");

  // useEffect(() => {
  //   if (brutto > 0 && vat >= 0) {
  //     const netto = brutto / (1 + vat / 100);
  //     setValue("netto", parseFloat(netto.toFixed(2)));
  //   }
  // }, [brutto, vat, setValue]);

  useEffect(() => {
    if (isUpdateOpen && selectedDescription) {
      reset(selectedDescription);
    }
  }, [isUpdateOpen, selectedDescription, reset]);

  useEffect(() => {
    if (isAddOpen) {
      reset({
      id: 0,
      title: "",
      value: "",
      regionID: 0
      });
    }
  }, [isAddOpen, reset]);

  const fetchDescriptions = () => {
    const token = Cookies.get("token");

    axios
      .get(`${API_URL}/api/additionaldescriptions/Product?ProductID=${productId}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((res) => setDescriptions(res.data.model));
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
          className="
                    whitespace-nowrap
                    opacity-0 max-w-0
                    group-hover:opacity-100
                    group-hover:max-w-[200px]
                    group-hover:ml-2
                    transition-all duration-300"
        >
        Dodaj opis
        </span>
      </button>
      <table className="w-full mb-4">
        <thead>
          <tr>
            <th className="text-left p-2 border-b">Nazwa opisu</th>
            <th className="text-left p-2 border-b">Opis</th>
            <th className="text-left p-2 border-b">Akcje</th>
          </tr>
        </thead>
        <tbody>
          {descriptions.map((description, index) => (
            <tr key={index}>
              <td className="p-2 border-b">{description.title}</td>
              <td className="p-2 border-b">{description.value}</td>
              <td className="p-2 border-b">
                <button
                  type="button"
                  className="mr-2"
                  onClick={() => {
                    setSelectedDescription(description);
                    setIsUpdateOpen(true);
                  }}
                >
                  <FaEdit />
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setSelectedDescription(description);
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
      

      {/*Add Description Modal */}
      {isAddOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Dodaj nowy opis
            </h2>

            {/* Nazwa */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Nazwa Opisu
              </label>
              <input
                {...register("title")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.title && (
                <p className="text-red-500 text-sm">{errors.title.message}</p>
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

            {/* Opis*/}
            <div>
              <label className="block text-sm font-medium mb-1">
                Opis
              </label>
              <textarea
                {...register("value")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.value && (
                <p className="text-red-500 text-sm">{errors.value.message}</p>
              )}
            </div>

            {/* VAT
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

             Netto
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
            </div>*/}

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
                    `${API_URL}/api/additionaldescriptions?ProductID=${productId}`,
                    newData,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`,
                        Accept: "application/json",
                        "Content-Type": "application/json",
                      },
                    }
                  );
                  alert("Dodano opis!");
                  setIsAddOpen(false);
                  fetchDescriptions();
                  await onAdd();
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

      {/* Delete Description Modal */}
      {isDeleteOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Potwierdzenie usunięcia
            </h2>
            <p>Czy na pewno chcesz usunąć {selectedDescription?.title}?</p>
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
                    `${API_URL}/api/additionaldescriptions/${selectedDescription?.id}`,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`,
                      },
                    }
                  );

                  alert("Usunięto opis!");
                  setIsDeleteOpen(false);
                  fetchDescriptions();
                  await onAdd();
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

      {/* Update Description Modal */}
      {isUpdateOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Edytuj opis
            </h2>

            {/* Nazwa */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Nazwa opisu
              </label>
              <input
                {...register("title")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableDescription ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableDescription}
              />
              {errors.title && (
                <p className="text-red-500 text-sm">{errors.title.message}</p>
              )}
            </div>

            {/* Region */}
            <div>
              <label className="block text-sm font-medium mb-1">Region</label>
              <select
                {...register("regionID")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableDescription ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableDescription}
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

            {/* Opis*/}
            <div>
              <label className="block text-sm font-medium mb-1">
                Opis
              </label>
              <textarea
                {...register("value")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableDescription ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableDescription}
              />
              {errors.value && (
                <p className="text-red-500 text-sm">{errors.value.message}</p>
              )}
            </div>

            {/* VAT
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

             Netto
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
            </div>*/}

            {/* Buttons */}

            <div className="flex justify-end gap-3 pt-2">
              {toEditableDescription === true ? (
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
                      `${API_URL}/api/additionaldescriptions/${selectedDescription?.id}`,
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
                    setToEditableDescription(false);
                    fetchDescriptions();
                    await onAdd();
                  })}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Zapisz
                </button>
              ) : (
                <button
                  type="button"
                  onClick={() => setToEditableDescription(true)}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Włącz edytowanie
                </button>
              )}

              <button
                type="button"
                onClick={() => {
                  setIsUpdateOpen(false);
                  setToEditableDescription(false);
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

export default AdditionalDescriptionComponent;
