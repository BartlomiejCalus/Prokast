import { useEffect, useState } from "react";
import { Photo } from "../../models/Photo";
import { FaPlus, FaTrash, FaPhotoVideo } from "react-icons/fa";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { set, useForm } from "react-hook-form";
import axios from "axios";
import Cookies from "js-cookie";

const API_URL = process.env.REACT_APP_API_URL;

const PhotoComponent = ({
  data,
  productId,
}: {
  data: Photo[];
  productId: string | undefined;
}) => {
  const [isViewOpen, setIsViewOpen] = useState(false);
  const [isDeleteOpen, setIsDeleteOpen] = useState(false);
  const [isAddOpen, setIsAddOpen] = useState(false);

  const [selectedPhoto, setSelectedPhoto] = useState<Photo | null>(null);
  const [photos, setPhotos] = useState<Photo[]>(data);

  const [preview, setPreview] = useState<string | null>(null);

  const priceSchema = yup.object().shape({
    id: yup.number().required(),
    name: yup.string().required("Nazwa jest wymagana"),
    value: yup.string().required("Wartość jest wymagana"),
    contentType: yup.string().required("Typ zawartości jest wymagany"),
  });

  const {
    register,
    handleSubmit,
    watch,
    setValue,
    reset,
    formState: { errors },
  } = useForm<Photo>({
    resolver: yupResolver(priceSchema),
    defaultValues: {
      id: 0,
      name: "",
      value: "",
      contentType: "",
    },
  });

  const fetchPhoto = () => {
    const token = Cookies.get("token");

    axios
      .get(`${API_URL}/api/photos/Product/${productId}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((res) => setPhotos(res.data.model));
  };

  const handlePhotoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    setPreview(URL.createObjectURL(file));

    setValue("contentType", file.type, {
      shouldValidate: true,
      shouldDirty: true,
    });

    const reader = new FileReader();
    reader.onloadend = () => {
      const base64 = (reader.result as string).split(",")[1];
      setValue("value", base64, {
        shouldValidate: true,
        shouldDirty: true,
      });
    };
    reader.readAsDataURL(file);
  };

  useEffect(() => {
    if (isAddOpen) {
      reset({
        id: 0,
        name: "",
        value: "",
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
          className="whitespace-nowrap
                    opacity-0 max-w-0
                    group-hover:opacity-100
                    group-hover:max-w-[200px]
                    group-hover:ml-2
                    transition-all duration-300"
        >
          Dodaj zdjęcie
        </span>
      </button>

      <table className="w-full mb-4">
        <thead>
          <tr>
            <th className="text-left p-2 border-b">Nazwa</th>
            <th className="text-left p-2 border-b">Akcje</th>
          </tr>
        </thead>
        <tbody>
          {photos.map((photo, index) => (
            <tr key={index}>
              <td className="p-2 border-b">{photo.name}</td>
              <td className="p-2 border-b">
                <button
                  type="button"
                  className="mr-2"
                  onClick={() => {
                    setSelectedPhoto(photo);
                    setIsViewOpen(true);
                  }}
                >
                  <FaPhotoVideo />
                </button>
                <button
                  type="button"
                  onClick={() => {
                    setSelectedPhoto(photo);
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

      {/*Add Photo Modal */}
      {isAddOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Dodaj nowe zdjęcie
            </h2>

            {/* Nazwa */}
            <div>
              <label className="block text-sm font-medium mb-1">
                Nazwa zdjęcia
              </label>
              <input
                {...register("name")}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {errors.name && (
                <p className="text-red-500 text-sm">{errors.name.message}</p>
              )}
            </div>

            {/* Zdjęcie*/}
            <div>
              <label className="block text-sm font-medium mb-1"></label>
              <input
                type="file"
                accept="image/*"
                onChange={handlePhotoChange}
                className="w-full border rounded px-3 py-2 focus:outline-blue-500"
              />
              {preview && (
                <img
                  src={preview}
                  alt="Podgląd zdjęcia"
                  className="mt-2 h-32 object-cover border"
                />
              )}

              {/* value i contentType są hidden bo wypełniamy je automatycznie */}
              <input type="hidden" {...register("value")} />
              <input type="hidden" {...register("contentType")} />
              {errors.value && (
                <p className="text-red-500 text-sm">{errors.value.message}</p>
              )}
            </div>

            {/* Buttons */}
            <div className="flex justify-end gap-3 pt-2">
              <button
                onClick={handleSubmit(async (newData) => {
                  console.log(newData);
                  const token = Cookies.get("token");

                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios.post(
                    `${API_URL}/api/photos/Product/${productId}`,
                    newData,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`,
                        Accept: "application/json",
                        "Content-Type": "application/json",
                      },
                    }
                  );
                  alert("Dodano zdjęcie!");
                  setIsAddOpen(false);
                  setPreview(null);
                  fetchPhoto();
                })}
                className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
              >
                Zapisz
              </button>

              <button
                onClick={() => {
                  setIsAddOpen(false);
                  setPreview(null);
                }}
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
            <p>Czy na pewno chcesz usunąć {selectedPhoto?.name}?</p>
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
                    `${API_URL}/api/photos/${selectedPhoto?.id}`,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`,
                      },
                    }
                  );

                  alert("Usunięto zdjęcie!");
                  setIsDeleteOpen(false);
                  fetchPhoto();
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

      {/*View Photo Modal */}
      {isViewOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 shadow-lg space-y-4 relative">
            <div className="flex justify-end">
              <button
                onClick={() => setIsViewOpen(false)}
                className="px-2 py-1 bg-red-500 text-white rounded hover:bg-red-600"
              >
                X
              </button>
            </div>

            <img
              src={selectedPhoto?.value}
              alt="Podgląd zdjęcia"
              className="mt-2 h-32 object-cover border"
            />
          </div>
        </div>
      )}
    </div>
  );
};

export default PhotoComponent;
