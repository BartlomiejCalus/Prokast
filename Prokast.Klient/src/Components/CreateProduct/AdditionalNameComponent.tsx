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

const AdditionalNameComponent = ({
    data,
    productId,
    onChange
}: {
    data: AdditionalField[];
    productId: string | undefined;
    onChange?: (items: AdditionalField[]) => void;
}) => {
    const navigate = useNavigate();

    const [isUpdateOpen, setIsUpdateOpen] = useState<boolean>(false);
    const [isAddOpen, setIsAddOpen] = useState<boolean>(false);
    const [isDeleteOpen, setIsDeleteOpen] = useState<boolean>(false);

    const [toEditableDescription, setToEditableDescription] = useState<boolean>(false);

    const [selectedDescription, setSelectedDescription] = useState<AdditionalField | null>(null);

    const [regions, setRegions] = useState<{ id: number; name: string }[]>([]);

    const [names, setNames] = useState<AdditionalField[]>(data);

    const [nextLocalId, setNextLocalId] = useState(1);

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
    useEffect(() => {
        if (onChange) {
            onChange(names);
        }
    }, [names]);
    const nameSchema = yup.object().shape({
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
        resolver: yupResolver(nameSchema),
        defaultValues: {
            id: 0,
            title: "",
            value: "",
            regionID: 0
        },
    });

    const updateLocalName = (updatedData: AdditionalField) => {
        setNames((prev) =>
            prev.map((desc) =>
                desc.localId === updatedData.localId ? { ...desc, ...updatedData } : desc
            )
        );
    };

    const removeLocalName = (localIdToRemove: number) => {
        setNames((prev) =>
            prev.filter((desc) => desc.localId !== localIdToRemove)
        );
    };
    console.log("Aktualne opisy:", names);
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
                regionID: 0,
                localId: 0
            });
        }
    }, [isAddOpen, reset]);

    // const fetchDescriptions = () => {
    //     setNames(names);
    // };

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
                        <th className="text-left p-2 border-b">Tytuł nazwy</th>
                        <th className="text-left p-2 border-b">Nazwa</th>
                        <th className="text-left p-2 border-b">Akcje</th>
                    </tr>
                </thead>
                <tbody>
                    {names.map((name, index) => (
                        <tr key={index}>
                            <td className="p-2 border-b">{name.title.substring(0,15)+
                                (name.title.length > 15 ? "...": "")}</td>
                            <td className="p-2 border-b">{name.value.substring(0,25)+
                                (name.value.length > 25 ? "...": "")}</td>
                            <td className="p-2 border-b">
                                <button
                                    type="button"
                                    className="mr-2"
                                    onClick={() => {
                                        setSelectedDescription(name);
                                        setIsUpdateOpen(true);
                                    }}
                                >
                                    <FaEdit />
                                </button>
                                <button
                                    type="button"
                                    onClick={() => {
                                        setSelectedDescription(name);
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
                                Tytuł nazwy
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
                                Nazwa
                            </label>
                            <textarea
                                {...register("value")}
                                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
                            />
                            {errors.value && (
                                <p className="text-red-500 text-sm">{errors.value.message}</p>
                            )}
                        </div>

                        {/* Buttons */}
                        <div className="flex justify-end gap-3 pt-2">
                            <button
                                onClick={handleSubmit((newData) => {
                                    const dataWithLocalId = { ...newData, localId: nextLocalId, id: 0 };
                                    setNames((prev) => {
                                        const updated = [...prev, dataWithLocalId];
                                        return updated;
                                    }); setNextLocalId((prev) => prev + 1);
                                    setIsAddOpen(false);
                                    console.log("Dodany element:", dataWithLocalId);
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
                                onClick={() => {
                                    if (!selectedDescription?.localId) {
                                        console.error("Brak wybranego elementu do usunięcia.");
                                        return;
                                    }
                                    removeLocalName(selectedDescription.localId);
                                    setIsDeleteOpen(false);
                                    console.log("Aktualne opisy po usunięciu:", names);
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
                                Tytuł nazwy
                            </label>
                            <input
                                {...register("title")}
                                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${!toEditableDescription ? "bg-gray-100" : ""
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
                                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${!toEditableDescription ? "bg-gray-100" : ""
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
                                Nazwa
                            </label>
                            <textarea
                                {...register("value")}
                                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${!toEditableDescription ? "bg-gray-100" : ""
                                    }`}
                                disabled={!toEditableDescription}
                            />
                            {errors.value && (
                                <p className="text-red-500 text-sm">{errors.value.message}</p>
                            )}
                        </div>

                        <div className="flex justify-end gap-3 pt-2">
                            {toEditableDescription === true ? (
                                <button
                                    type="button"
                                    onClick={handleSubmit((updateData) => {
                                        updateLocalName(updateData); // aktualizacja lokalna
                                        setIsUpdateOpen(false);
                                        setToEditableDescription(false);
                                        //fetchDescriptions();
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

export default AdditionalNameComponent;
