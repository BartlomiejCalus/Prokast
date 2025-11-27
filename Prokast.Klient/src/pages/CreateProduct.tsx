import React, { useState, useEffect  } from "react";
import axios from "axios";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";
import Navbar from "../Components/Navbar";

import PriceListComponent from "../Components/CreateProduct/PriceListComponent";
import ParametersComponent from "../Components/CreateProduct/ParametersComponent";
import PhotoComponent from "../Components/CreateProduct/PhotoComponent";
import AdditionalDescriptionComponent from "../Components/CreateProduct/AdditionalDescriptionComponent";
import AdditionalNameComponent from "../Components/CreateProduct/AdditionalNameComponent";
import { ProductModel } from "../models/Product";

const API_URL = process.env.REACT_APP_API_URL;

type SectionKey = "desc" | "names" | "prices" | "params" | "photos";

const CreateProduct: React.FC = () => {
    const navigate = useNavigate();

    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");
    const [formErrors, setFormErrors] = useState<Record<string, string>>({});
    const [product, setProduct] = useState<ProductModel>({
        name: "",
        sku: "",
        ean: "",
        description: "",
        additionalDescriptions: [],
        additionalNames: [],
        dictionaryParams: [],
        customParams: [],
        photos: [],
        priceList: {
            name: "",
            prices: []
        }
    });
    useEffect(() => {
    setProduct(prev => ({
        ...prev,
        priceList: {
            ...prev.priceList,
            name: prev.name ? `Cennik - ${prev.name}` : ""
        }
    }));
}, [product.name]);
    const updateDescriptions = (items: typeof product.additionalDescriptions) => {
        setProduct(prev => ({ ...prev, additionalDescriptions: items }));
    };

    const updateNames = (items: typeof product.additionalNames) => {
        setProduct(prev => ({ ...prev, additionalNames: items }));
    };

    const updatePhotos = (items: typeof product.photos) => {
        setProduct(prev => ({ ...prev, photos: items }));
    };

    const updatePrices = (priceList: typeof product.priceList) => {
        setProduct(prev => ({ ...prev, priceList }));
    };

    const updateParams = (items: typeof product.customParams) => {
        setProduct(prev => ({ ...prev, customParams: items }));
    };

    const [openSections, setOpenSections] = useState<Record<SectionKey, boolean>>({
        desc: false,
        names: false,
        prices: false,
        params: false,
        photos: false
    });
    
    const toggleSection = (key: SectionKey) => {
        setOpenSections(prev => ({ ...prev, [key]: !prev[key] }));
    };

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        const { name, value } = e.target;
        setProduct(prev => ({
            ...prev,
            [name]: value,
        }));
    };
    console.log("Aktualny produkt:", product);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSaving(true);

        const errors = validateProduct(product);

        if (Object.keys(errors).length > 0) {
            setFormErrors(errors);
            setSaving(false);
            return;
        }
        setFormErrors({});
        try {
            const token = Cookies.get("token");
            if (!token) {
                setError("Brak tokenu autoryzacyjnego.");
                return;
            }

            await axios.post(
                `${API_URL}/api/products`,
                product,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        "Content-Type": "application/json",
                    },
                }
            );

            navigate("/ProductsList");
        } catch (err: any) {
            console.log("Błąd API:", err.response?.data ?? err);
            setError("Nie udało się stworzyć produktu.");
        } finally {
            setSaving(false);
        }
    };
    const validateProduct = (product: ProductModel) => {
        const errors: Record<string, string> = {};

        if (!product.name.trim())
            errors.name = "Nazwa produktu jest obowiązkowa.";

        if (!product.sku.trim())
            errors.sku = "SKU jest obowiązkowe.";

        if (!product.ean.trim())
            errors.ean = "EAN jest obowiązkowy.";

        if (!product.description.trim())
            errors.description = "Opis jest obowiązkowy.";

        if (product.photos.length === 0)
            errors.photos = "Dodaj przynajmniej jedno zdjęcie.";

        if (!product.priceList.name.trim())
            errors.priceListName = "Nazwa cennika jest obowiązkowa.";

        if (product.priceList.prices.length === 0)
            errors.priceListPrices = "Dodaj przynajmniej jedną cenę.";

        if (product.additionalNames.length === 0)
            errors.additionalNames = "Dodaj przynajmniej jedną dodatkową nazwę.";

        if (product.additionalDescriptions.length === 0)
            errors.additionalDescriptions = "Dodaj przynajmniej jeden dodatkowy opis.";

        if (product.customParams.length === 0)
            errors.customParams = "Dodaj przynajmniej jeden parametr.";

        return errors;
    };
    
    return (
        <div className="min-h-screen flex flex-col bg-gradient-to-br from-green-100 via-white to-green-200">
            <Navbar />
            <main className="flex flex-col items-center justify-center w-screen mt-10">
                <form
                    onSubmit={handleSubmit}
                    className="w-full max-w-2xl p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-3"
                >
                    <h2 className="text-2xl font-bold text-center text-gray-800">
                        Dodaj nowy produkt
                    </h2>

                    {error && <p className="text-red-600">{error}</p>}
                    {formErrors.name && (
                        <p className="text-red-600 text-sm mt-1">{formErrors.name}</p>
                    )}
                    <input
                        type="text"
                        name="name"
                        placeholder="Nazwa produktu"
                        value={product.name}
                        onChange={handleChange}
                        className="w-full p-2 border rounded-xl shadow-md"
                    />

                    {formErrors.sku && (
                        <p className="text-red-600 text-sm mt-1">{formErrors.sku}</p>
                    )}
                    <input
                        type="text"
                        name="sku"
                        placeholder="SKU"
                        value={product.sku}
                        onChange={handleChange}
                        className="w-full p-2 border rounded-xl shadow-md"
                    />

                    {formErrors.ean && (
                        <p className="text-red-600 text-sm mt-1">{formErrors.ean}</p>
                    )}
                    <input
                        type="text"
                        name="ean"
                        placeholder="EAN"
                        value={product.ean}
                        onChange={handleChange}
                        className="w-full p-2 border rounded-xl shadow-md"
                    />

                    {formErrors.description && (
                        <p className="text-red-600 text-sm mt-1">{formErrors.description}</p>
                    )}
                    <textarea
                        name="description"
                        placeholder="Opis produktu"
                        value={product.description}
                        onChange={handleChange}
                        className="w-full p-2 border rounded-xl shadow-md"
                    />



                    <div className="flex flex-col mt-4 gap-4">

                        {/* Opisy */}
                        {formErrors.additionalDescriptions && (
                            <p className="text-red-600 text-sm">{formErrors.additionalDescriptions}</p>
                        )}
                        <div className="bg-white/70 shadow-md p-4 rounded-xl">
                            <div className="flex justify-between items-center">
                                <h3 className="font-semibold">Opisy</h3>
                                <button
                                    type="button"
                                    onClick={() => toggleSection("desc")}
                                    className="bg-gray-300 hover:bg-gray-400 text-gray-800 p-2 rounded-xl transition"
                                >
                                    {openSections.desc ? "Ukryj" : "Wyświetl"}
                                </button>
                            </div>

                            {openSections.desc && (
                                <AdditionalDescriptionComponent
                                    data={product.additionalDescriptions}
                                    productId={undefined}
                                    onChange={updateDescriptions}
                                />
                            )}
                        </div>

                        {/* Nazwy */}
                        {formErrors.additionalNames && (
                            <p className="text-red-600 text-sm">{formErrors.additionalNames}</p>
                        )}
                        <div className="bg-white/70 shadow-md p-4 rounded-xl">
                            <div className="flex justify-between items-center">
                                <h3 className="font-semibold">Nazwy</h3>
                                <button
                                    type="button"
                                    onClick={() => toggleSection("names")}
                                    className="bg-gray-300 hover:bg-gray-400 text-gray-800 p-2 rounded-xl transition"
                                >
                                    {openSections.names ? "Ukryj" : "Wyświetl"}
                                </button>
                            </div>

                            {openSections.names && (
                                <div className="mt-3">
                                    <AdditionalNameComponent
                                        data={product.additionalNames}
                                        productId={undefined}
                                        onChange={updateNames}
                                    />
                                </div>
                            )}
                        </div>

                        {/* Cennik */}
                        {formErrors.priceListPrices && (
                            <p className="text-red-600 text-sm mt-1">{formErrors.priceListPrices}</p>
                        )}
                        <div className="bg-white/70 shadow-md p-4 rounded-xl">
                            <div className="flex justify-between items-center">
                                <h3 className="font-semibold">Cennik</h3>
                                <button
                                    type="button"
                                    onClick={() => toggleSection("prices")}
                                    className="bg-gray-300 hover:bg-gray-400 text-gray-800 p-2 rounded-xl transition"
                                >
                                    {openSections.prices ? "Ukryj" : "Wyświetl"}
                                </button>
                            </div>

                            {openSections.prices && (
                                <div className="mt-3">
                                    <PriceListComponent
                                        data={product.priceList}
                                        productId={undefined}
                                        productName={product.name}
                                        onChange={updatePrices}
                                    />
                                </div>
                            )}
                        </div>

                        {/* Parametry */}
                        {formErrors.customParams && (
                            <p className="text-red-600 text-sm">{formErrors.customParams}</p>
                        )}
                        <div className="bg-white/70 shadow-md p-4 rounded-xl">
                            <div className="flex justify-between items-center">
                                <h3 className="font-semibold">Parametry</h3>
                                <button
                                    type="button"
                                    onClick={() => toggleSection("params")}
                                    className="bg-gray-300 hover:bg-gray-400 text-gray-800 p-2 rounded-xl transition"
                                >
                                    {openSections.params ? "Ukryj" : "Wyświetl"}
                                </button>
                            </div>

                            {openSections.params && (
                                <div className="mt-3">
                                    <ParametersComponent
                                        data={product.customParams}
                                        productId={undefined}
                                        onChange={updateParams}
                                    />
                                </div>
                            )}
                        </div>

                        {/* Zdjęcia */}
                        {formErrors.photos && (
                            <p className="text-red-600 text-sm">{formErrors.photos}</p>
                        )}
                        <div className="bg-white/70 shadow-md p-4 rounded-xl">
                            <div className="flex justify-between items-center">
                                <h3 className="font-semibold">Zdjęcia</h3>
                                <button
                                    type="button"
                                    onClick={() => toggleSection("photos")}
                                    className="bg-gray-300 hover:bg-gray-400 text-gray-800 p-2 rounded-xl transition"
                                >
                                    {openSections.photos ? "Ukryj" : "Wyświetl"}
                                </button>
                            </div>

                            {openSections.photos && (
                                <div className="mt-3">
                                    <PhotoComponent
                                        data={product.photos}
                                        productId={undefined}
                                        onChange={updatePhotos}
                                    />
                                </div>
                            )}
                        </div>

                    </div>
                    {/* 🔽 BUTTONY – zapisz / anuluj */}
                    <div className="flex gap-3 mt-6">
                        <button
                            type="button"
                            onClick={() => navigate("/ProductsList")}
                            className="flex-1 bg-gray-300 hover:bg-gray-400 text-gray-800 p-2 rounded-xl transition"
                        >
                            Anuluj
                        </button>

                        <button
                            type="submit"
                            //onSubmit={handleSubmit}
                            disabled={saving}
                            className="flex-1 bg-green-500 hover:bg-green-600 text-white p-2 rounded-xl transition"
                        >
                            {saving ? "Zapisywanie..." : "Utwórz produkt"}
                        </button>
                    </div>

                </form>
            </main>
        </div>

    );
};

export default CreateProduct;