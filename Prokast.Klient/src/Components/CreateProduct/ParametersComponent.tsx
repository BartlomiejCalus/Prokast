import { useEffect, useState } from "react";
import { CustomParam } from "../../models/CustomParam";
import { FaEdit, FaPlus, FaTrash } from "react-icons/fa";
import axios from "axios";
import Cookies from "js-cookie";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

const API_URL = process.env.REACT_APP_API_URL;

const ParametersComponent = ({
    data,
    productId,
    onChange,
}: {
    data: CustomParam[];
    productId: string | undefined;
    onChange?: (items: CustomParam[]) => void;
}) => {
    const [params, setParams] = useState<CustomParam[]>(data || []);

    const [selectedParam, setSelectedParam] = useState<CustomParam | null>(null);

    const [isUpdateOpen, setIsUpdateOpen] = useState(false);
    const [isDeleteOpen, setIsDeleteOpen] = useState(false);
    const [isAddOpen, setIsAddOpen] = useState(false);
    const [nextLocalId, setNextLocalId] = useState(1);

    const [toEditableParam, setToEditableParam] = useState(false);

    //#region get regions

    const [regions, setRegions] = useState<{ id: number; name: string }[]>([]);

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
            onChange(params);
        }
    }, [params]);
    const paramSchema = yup.object().shape({
        id: yup.number().required(),
        name: yup.string().required("Nazwa jest wymagana"),
        type: yup.string().required("Typ jest wymagany"),
        value: yup.string().required("Wartość jest wymagana"),
        regionID: yup.number().required("Wybierz region"),
    });

    const {
        register,
        handleSubmit,
        watch,
        setValue,
        reset,
        formState: { errors },
    } = useForm<CustomParam>({
        resolver: yupResolver(paramSchema),
        defaultValues: {
            id: 0,
            name: "",
            regionID: 1,
            value: "",
            type: "String",
        },
    });

    //   const fetchParams = () => {
    //     const token = Cookies.get("token");

    //     axios
    //       .get(`${API_URL}/api/params/product/${productId}`, {
    //         headers: { Authorization: `Bearer ${token}` },
    //       })
    //       .then((res) => setParams(res.data.model));
    //   };

    const selectedType = watch("type");

    useEffect(() => {
        setValue("value", "");
    }, [selectedType]);

    useEffect(() => {
        if (isUpdateOpen && selectedParam) {
            reset(selectedParam);
        }
    }, [isUpdateOpen, selectedParam, reset]);

    const updateLocalDescription = (updatedData: CustomParam) => {
        setParams((prev) =>
            prev.map((param) =>
                param.localId === updatedData.localId ? { ...param, ...updatedData } : param
            )
        );
    };

    const removeLocalDescription = (localIdToRemove: number) => {
        setParams((prev) =>
            prev.filter((param) => param.localId !== localIdToRemove)
        );
    };

    useEffect(() => {
        if (isAddOpen) {
            reset({
                id: 0,
                name: "",
                regionID: 1,
                value: "",
                type: "String",
                localId: 0,
            });
        }
    }, [isAddOpen, reset]);

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
                    Dodaj parametr
                </span>
            </button>

            <table className="w-full mb-4">
                <thead>
                    <tr>
                        <th className="text-left p-2 border-b">Nazwa parametru</th>
                        <th className="text-left p-2 border-b">Wartość</th>
                        <th className="text-left p-2 border-b">Akcje</th>
                    </tr>
                </thead>
                <tbody>
                    {params.map((param, index) => (
                        <tr key={index}>
                            <td className="p-2 border-b">{param.name}</td>
                            <td className="p-2 border-b">
                                {param.value.substring(0, 35) +
                                    (param.value.length > 35 ? "..." : "")}
                            </td>
                            <td className="p-2 border-b">
                                <button
                                    type="button"
                                    className="mr-2"
                                    onClick={() => {
                                        setSelectedParam(param);
                                        setIsUpdateOpen(true);
                                    }}
                                >
                                    <FaEdit />
                                </button>
                                <button
                                    type="button"
                                    onClick={() => {
                                        setSelectedParam(param);
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

            {/*Add Param Modal */}
            {isAddOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
                    <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
                        <h2 className="text-xl font-bold text-gray-800 mb-2">
                            Dodaj nowy parametr
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

                        {/* Type*/}
                        <div>
                            <label className="block text-sm font-medium mb-1">Typ</label>
                            <select
                                {...register("type")}
                                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
                            >
                                <option value="String">TEXT</option>
                                <option value="Number">LICZBA</option>
                                <option value="Boolean">PRAWDA/FAŁSZ</option>
                            </select>
                            {errors.type && (
                                <p className="text-red-500 text-sm">{errors.type.message}</p>
                            )}
                        </div>

                        {/* Value*/}
                        <div>
                            <label className="block text-sm font-medium mb-1">Wartość</label>
                            {selectedType !== "Boolean" && (
                                <input
                                    {...register("value")}
                                    className="w-full border rounded px-3 py-2 focus:outline-blue-500"
                                    type={selectedType === "Number" ? "number" : "text"}
                                />
                            )}

                            {selectedType === "Boolean" && (
                                <select
                                    {...register("value")}
                                    className="w-full border rounded px-3 py-2"
                                >
                                    <option value="true">TRUE</option>
                                    <option value="false">FALSE</option>
                                </select>
                            )}
                            {errors.value && (
                                <p className="text-red-500 text-sm">{errors.value.message}</p>
                            )}
                        </div>

                        {/* Buttons */}
                        <div className="flex justify-end gap-3 pt-2">
                            <button
                                onClick={handleSubmit((newData) => {
                                    const dataWithLocalId = { ...newData, localId: nextLocalId, id: 0 };
                                    setParams((prev) => {
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

            {/* Delete Price Modal */}
            {isDeleteOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
                    <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
                        <h2 className="text-xl font-bold text-gray-800 mb-2">
                            Potwierdzenie usunięcia
                        </h2>
                        <p>Czy na pewno chcesz usunąć {selectedParam?.name}?</p>
                        <div className="flex justify-end gap-3 pt-2">
                            <button
                                type="button"
                                onClick={() => {
                                    if (!selectedParam?.localId) {
                                        console.error("Brak wybranego elementu do usunięcia.");
                                        return;
                                    }
                                    removeLocalDescription(selectedParam.localId);
                                    setIsDeleteOpen(false);
                                    console.log("Aktualne opisy po usunięciu:", params);
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
                                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${!toEditableParam ? "bg-gray-100" : ""
                                    }`}
                                disabled={!toEditableParam}
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
                                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${!toEditableParam ? "bg-gray-100" : ""
                                    }`}
                                disabled={!toEditableParam}
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

                        {/* Type*/}
                        <div>
                            <label className="block text-sm font-medium mb-1">Typ</label>
                            <select
                                {...register("type")}
                                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${!toEditableParam ? "bg-gray-100" : ""
                                    }`}
                                disabled={!toEditableParam}
                            >
                                <option value="String">TEXT</option>
                                <option value="Number">LICZBA</option>
                                <option value="Boolean">PRAWDA/FAŁSZ</option>
                            </select>
                            {errors.type && (
                                <p className="text-red-500 text-sm">{errors.type.message}</p>
                            )}
                        </div>

                        {/* Value*/}
                        <div>
                            <label className="block text-sm font-medium mb-1">Wartość</label>
                            {selectedType !== "Boolean" && (
                                <input
                                    {...register("value")}
                                    className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${!toEditableParam ? "bg-gray-100" : ""
                                        }`}
                                    type={selectedType === "Number" ? "number" : "text"}
                                    disabled={!toEditableParam}
                                />
                            )}

                            {selectedType === "Boolean" && (
                                <select
                                    {...register("value")}
                                    className="w-full border rounded px-3 py-2"
                                >
                                    <option value="true">TRUE</option>
                                    <option value="false">FALSE</option>
                                </select>
                            )}
                            {errors.value && (
                                <p className="text-red-500 text-sm">{errors.value.message}</p>
                            )}
                        </div>

                        {/* Buttons */}

                        <div className="flex justify-end gap-3 pt-2">
                            {toEditableParam === true ? (
                                <button
                                    type="button"
                                    onClick={handleSubmit((updateData) => {
                                        updateLocalDescription(updateData); // aktualizacja lokalna
                                        setIsUpdateOpen(false);
                                        setToEditableParam(false);
                                    })}
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

export default ParametersComponent;
