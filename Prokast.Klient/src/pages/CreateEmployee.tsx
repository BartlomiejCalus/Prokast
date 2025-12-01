import React, { useEffect, useState } from 'react';
import Navbar from '../Components/Navbar';
import { useForm } from 'react-hook-form';
import { User } from '../models/User';
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { NewAccResp } from "../models/newAccResp";
import { Warehouse } from '../models/Warehouse';
import { Role } from '../models/Role';
import axios from 'axios';
import Cookies from 'js-cookie';
import { FaCheck, FaCopy } from 'react-icons/fa';

const API_URL = process.env.REACT_APP_API_URL; 

const CreateEmployee: React.FC = () => {

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
    <div className="min-h-screen bg-gradient-to-br from-green-100 via-white to-green-200 p-4">
      <Navbar />
      <div className="max-w-2xl mx-auto bg-white/80 backdrop-blur-md shadow-xl rounded-2xl p-6 mt-8">
      <h2 className="text-xl font-bold text-gray-800 mb-2">
              Dodaj użytkownika
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
            {addedSuccessfully && (
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
  );
};

export default CreateEmployee;
