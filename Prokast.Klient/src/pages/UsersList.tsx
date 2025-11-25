import Navbar from "../Components/Navbar";
import { useEffect, useState } from "react";
import { User } from "../models/User";
import Cookies from "js-cookie";
import axios from "axios";
import { Warehouse } from "../models/Warehouse";
import { Role } from "../models/Role";
import { FaEdit, FaPlus, FaTrash, FaCopy, FaCheck } from "react-icons/fa";
import { set, useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { NewAccResp } from "../models/newAccResp";

const API_URL = process.env.REACT_APP_API_URL;

const UsersList = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [roles, setRoles] = useState<Role[]>([]);
  const [warehouses, setWarehouses] = useState<Warehouse[]>([]);

  const [isUpdateOpen, setIsUpdateOpen] = useState<boolean>(false);
  const [isAddOpen, setIsAddOpen] = useState<boolean>(false);
  const [isDeleteOpen, setIsDeleteOpen] = useState<boolean>(false);

  const [toEditableUser, setToEditableUser] = useState<boolean>(false);

  const [selectedUser, setSelectedUser] = useState<User | null>(null);

  const [email, setEmail] = useState<string>("");
  const [errorEmail, setEmailError] = useState<string>("");

  const [responseMessage, setResponseMessage] = useState<NewAccResp | null>(null);
  const [addedSuccessfully, setAddedSuccessfully] = useState<boolean>(false);

  const [copied, setCopied] = useState<boolean>(false);

  const validateEmail = (value: string) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(value);
  };

  //#region some fetchers
  useEffect(() => {
    const token = Cookies.get("token");

    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }

    axios
      .get(`${API_URL}/api/login`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
      })
      .then((res) => setUsers(res.data.model));
  }, []);

  useEffect(() => {
    const token = Cookies.get("token");

    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }

    axios
      .get(`${API_URL}/Api/Warehouses`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
      })
      .then((res) => setWarehouses(res.data.model));
  }, []);

  useEffect(() => {
    const token = Cookies.get("token");

    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }
    axios
      .get(`${API_URL}/api/login/Role`, {
        headers: {
          Authorization: `Bearer ${token}`,
          Accept: "application/json",
        },
      })
      .then((res) => setRoles(res.data.model));
  }, []);

  const fetchUsers = async () => {
    const token = Cookies.get("token");
    if (!token) {
      console.error("Brak tokenu autoryzacyjnego.");
      return;
    }
    const response = await axios.get(`${API_URL}/api/login`, {
      headers: {
        Authorization: `Bearer ${token}`,
        Accept: "application/json",
      },
    });
    setUsers(response.data.model);
  };
  //#endregion

  const userSchema = yup.object().shape({
    id: yup.number().required(),
    firstName: yup.string().required("Imię jest wymagane"),
    lastName: yup.string().required("Nazwisko jest wymagane"),
    roleId: yup.number().required("Rola jest wymagana"),
    warehouseID: yup.number().required("ID magazynu jest wymagane"),
  });

  const {
    register,
    handleSubmit,
    watch,
    setValue,
    reset,
    formState: { errors },
  } = useForm<User>({
    resolver: yupResolver(userSchema),
    defaultValues: {
      id: 0,
      firstName: "",
      lastName: "",
      roleId: 1,
      warehouseID: 1,
    },
  });

  useEffect(() => {
    if (isUpdateOpen && selectedUser) {
      reset(selectedUser);
    }
  }, [isUpdateOpen, selectedUser, reset]);

  useEffect(() => {
    if (isAddOpen) {
      reset({
        id: 0,
        firstName: "",
        lastName: "",
        roleId: 0,
        warehouseID: 0,
      });
    }
  }, [isAddOpen, reset]);

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 p-4">
      <Navbar />
      <div className="max-w-7xl mx-auto flex flex-col lg:flex-row mt-8 p-4 gap-6">
        <main className="flex-1">
          <button
            type="button"
            onClick={() => setIsAddOpen(true)}
            className="group flex items-center justify-center
                        bg-blue-600 text-white rounded-full
                        h-10 w-10
                        hover:w-48
                        transition-all duration-300
                        overflow-hidden
                        hover:bg-blue-700
                        hover:px-4
                        mb-3"
          >
            <FaPlus className="w-5 h-5 flex-shrink-0" />
            <span
              className="
                        whitespace-nowrap
                        opacity-0 max-w-0
                        group-hover:opacity-100
                        group-hover:max-w-[200px]
                        group-hover:ml-3
                        transition-all duration-300"
            >
              Dodaj użytkownika
            </span>
          </button>

          <table className="min-w-full bg-white/80 backdrop-blur-md shadow-xl">
            <thead>
              <tr>
                <th className="py-3 px-6 bg-blue-200 text-left text-sm font-semibold text-gray-700">
                  Imię
                </th>
                <th className="py-3 px-6 bg-blue-200 text-left text-sm font-semibold text-gray-700">
                  Nazwisko
                </th>
                <th className="py-3 px-6 bg-blue-200 text-left text-sm font-semibold text-gray-700">
                  Rola
                </th>
                <th className="py-3 px-6 bg-blue-200 text-left text-sm font-semibold text-gray-700">
                  Akcje
                </th>
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.id} className="border-b hover:bg-blue-50">
                  <td className="py-4 px-6 text-sm text-gray-800">
                    {user.firstName}
                  </td>
                  <td className="py-4 px-6 text-sm text-gray-800">
                    {user.lastName}
                  </td>
                  <td className="py-4 px-6 text-sm text-gray-800">
                    {roles.find((r) => r.id === user.roleId)?.roleName}
                  </td>
                  <td className="py-4 px-6 text-sm text-gray-800">
                    <button
                      type="button"
                      className="mr-2"
                      onClick={() => {
                        setSelectedUser(user);
                        setIsUpdateOpen(true);
                      }}
                    >
                      <FaEdit />
                    </button>
                    <button
                      type="button"
                      onClick={() => {
                        setSelectedUser(user);
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
        </main>
      </div>

      {/* Delete Price Modal */}
      {isDeleteOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[400px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Potwierdzenie usunięcia
            </h2>
            <p>
              Czy na pewno chcesz usunąć konto użytkownika{" "}
              {selectedUser?.firstName} {selectedUser?.lastName}?
            </p>
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
                    `${API_URL}/api/login/${selectedUser?.id}`,
                    {
                      headers: {
                        Authorization: `Bearer ${token}`,
                      },
                    }
                  );

                  alert("Usunięto użytkownika!");
                  setIsDeleteOpen(false);
                  fetchUsers();
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

      {/* Update User Modal */}
      {isUpdateOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Edytuj użytkownika
            </h2>

            {/* imie */}
            <div>
              <label className="block text-sm font-medium mb-1">Imię</label>
              <input
                {...register("firstName")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableUser ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableUser}
              />
              {errors.firstName && (
                <p className="text-red-500 text-sm">
                  {errors.firstName.message}
                </p>
              )}
            </div>

            {/* nazwisko */}
            <div>
              <label className="block text-sm font-medium mb-1">Nazwisko</label>
              <input
                {...register("lastName")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableUser ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableUser}
              />
              {errors.lastName && (
                <p className="text-red-500 text-sm">
                  {errors.lastName.message}
                </p>
              )}
            </div>

            {/* Rola */}
            <div>
              <label className="block text-sm font-medium mb-1">Rola</label>
              <select
                {...register("roleId")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableUser ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableUser}
              >
                {roles.map((r) => (
                  <option key={r.id} value={r.id}>
                    {r.roleName}
                  </option>
                ))}
              </select>
              {errors.roleId && (
                <p className="text-red-500 text-sm">{errors.roleId.message}</p>
              )}
            </div>

            {/* Magazyn */}
            <div>
              <label className="block text-sm font-medium mb-1">Magazyn</label>
              <select
                {...register("warehouseID")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500 ${
                  !toEditableUser ? "bg-gray-100" : ""
                }`}
                disabled={!toEditableUser}
              >
                {warehouses.map((r) => (
                  <option key={r.id} value={r.id}>
                    {r.name}
                  </option>
                ))}
              </select>
              {errors.warehouseID && (
                <p className="text-red-500 text-sm">
                  {errors.warehouseID.message}
                </p>
              )}
            </div>

            {/* Buttons */}

            <div className="flex justify-end gap-3 pt-2">
              {toEditableUser === true ? (
                <button
                  type="button"
                  onClick={handleSubmit(async (updateData) => {
                    const token = Cookies.get("token");

                    console.log(updateData);

                    if (!token) {
                      console.error("Brak tokenu autoryzacyjnego.");
                      return;
                    }

                    await axios.put(`${API_URL}/api/login`, updateData, {
                      headers: {
                        Authorization: `Bearer ${token}`,
                        Accept: "application/json",
                        "Content-Type": "application/json",
                      },
                    });
                    setIsUpdateOpen(false);
                    setToEditableUser(false);
                    fetchUsers();
                  })}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Zapisz
                </button>
              ) : (
                <button
                  type="button"
                  onClick={() => setToEditableUser(true)}
                  className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                >
                  Włącz edytowanie
                </button>
              )}

              <button
                type="button"
                onClick={() => {
                  setIsUpdateOpen(false);
                  setToEditableUser(false);
                }}
                className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
              >
                Zamknij
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Create User Modal */}
      {isAddOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
          <div className="bg-white rounded-lg p-6 w-[450px] shadow-lg space-y-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">
              Edytuj użytkownika
            </h2>

            {/* imie */}
            <div>
              <label className="block text-sm font-medium mb-1">Imię</label>
              <input
                {...register("firstName")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500`}
                disabled={addedSuccessfully}
              />
              {errors.firstName && (
                <p className="text-red-500 text-sm">
                  {errors.firstName.message}
                </p>
              )}
            </div>

            {/* nazwisko */}
            <div>
              <label className="block text-sm font-medium mb-1">Nazwisko</label>
              <input
                {...register("lastName")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500`}
                disabled={addedSuccessfully}
              />
              {errors.lastName && (
                <p className="text-red-500 text-sm">
                  {errors.lastName.message}
                </p>
              )}
            </div>

            {/* email */}
            <div>
              <label className="block text-sm font-medium mb-1">Email</label>
              <input
                type="mail"
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500`}
                disabled={addedSuccessfully}
                onChange={(e) => {
                  setEmail(e.target.value);

                  if (e.target.value && !validateEmail(e.target.value)) {
                    setEmailError("Nieprawidłowy email");
                  } else {
                    setEmailError("");
                  }
                }}
                onBlur={(e) => {
                  if (!validateEmail(e.target.value)) {
                    setEmailError("Nieprawidłowy email");
                  }
                }}
              />
              <p className="text-red-500 text-sm">{errorEmail}</p>
            </div>

            {/* Rola */}
            <div>
              <label className="block text-sm font-medium mb-1">Rola</label>
              <select
                {...register("roleId")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500`}
                disabled={addedSuccessfully}
              >
                {roles.map((r) => (
                  <option key={r.id} value={r.id}>
                    {r.roleName}
                  </option>
                ))}
              </select>
              {errors.roleId && (
                <p className="text-red-500 text-sm">{errors.roleId.message}</p>
              )}
            </div>

            {/* Magazyn */}
            <div>
              <label className="block text-sm font-medium mb-1">Magazyn</label>
              <select
                {...register("warehouseID")}
                className={`w-full border rounded px-3 py-2 focus:outline-blue-500`}
                disabled={addedSuccessfully}
              >
                {warehouses.map((r) => (
                  <option key={r.id} value={r.id}>
                    {r.name}
                  </option>
                ))}
              </select>
              {errors.warehouseID && (
                <p className="text-red-500 text-sm">
                  {errors.warehouseID.message}
                </p>
              )}
            </div>

            {/* Dane do logowania */}
            {!addedSuccessfully && (
              <div className="p-4 bg-green-100 border border-green-400 rounded">
                <h3 className="text-lg font-semibold mb-2 text-green-800">
                  POPRAWNIE DODANO UŻYTKOWNIKA. <br/>
                  Dane do logowania:
                </h3>
                <p>Login: {responseMessage?.login}</p>
                <p>Hasło: {responseMessage?.password}</p>

                <button
                  type="button"
                  onClick={() => {
                    navigator.clipboard.writeText(
                      `Login: ${responseMessage?.login}\nHasło: ${responseMessage?.password}`
                    );
                    setCopied(true);
                    setTimeout(() => setCopied(false), 2000);
                  }}
                  className={`px-3 py-1 text-white rounded ${copied ? "bg-green-600 cursor-not-allowed" : "bg-blue-600 hover:bg-blue-700"}`}
                >
                  {!copied ? <FaCopy /> : <FaCheck/>}
                </button>
              </div>
            )}

            {/* Buttons */}
            <div className="flex justify-end gap-3 pt-2">
              <button
                onClick={handleSubmit(async (newData) => {
                  const token = Cookies.get("token");
                  console.log(newData);
                  if (!token) {
                    console.error("Brak tokenu autoryzacyjnego.");
                    return;
                  }

                  await axios
                    .post(`${API_URL}/api/login/create`, newData, {
                      headers: {
                        Authorization: `Bearer ${token}`,
                        Accept: "application/json",
                        "Content-Type": "application/json",
                      },
                      params: { mail: email },
                    })
                    .then((response) => {
                      setResponseMessage(response.data.model);
                      console.log(responseMessage);
                      console.log(response.data);
                    });
                  setAddedSuccessfully(true);
                })}
                className={`px-4 py-2 bg-green-600 text-white rounded ${addedSuccessfully ? "opacity-50 cursor-not-allowed" : "hover:bg-green-700"} `}
                disabled={addedSuccessfully}
              >
                Zapisz
              </button>

              <button
                onClick={() => {
                  fetchUsers();
                  setIsAddOpen(false);
                  setAddedSuccessfully(false);
                  setResponseMessage(null);
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

export default UsersList;
