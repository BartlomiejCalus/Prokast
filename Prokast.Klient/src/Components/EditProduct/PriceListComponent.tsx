import React, { useState } from 'react'
import { PriceList } from '../../models/PriceList'
import Cookies from 'js-cookie';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { FaEdit } from 'react-icons/fa'

const API_URL = process.env.REACT_APP_API_URL;

const PriceListComponent = ({data}:{data: PriceList}) => {

  const navigate = useNavigate();

  const [updateData, setUpdateData] = useState<PriceList>(data);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");

    const handleChange = (
      e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
      const { name, value } = e.target;
      setUpdateData((prev: any) => ({
        ...prev,
        [name]: value,
      }));
    };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);

    try {
      const token = Cookies.get("token");
      if (!token) {
        setError("Brak tokenu autoryzacyjnego.");
        return;
      }

      await axios.put(
        `${API_URL}/api/products/products/`,
        {

        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      localStorage.removeItem("editProduct");
      navigate("/ProductsList");
    } catch (err: any) {
      console.log("Błąd API:", err.response?.data ?? err);
      setError("Nie udało się zapisać zmian.");
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className='mt-4 p-4 border rounded-xl bg-white/70 shadow-md w-full'>
        <table className='w-full mb-4'>
          <thead>
            <tr>
              <th className='text-left p-2 border-b'>Nazwa ceny</th>
              <th className='text-left p-2 border-b'>Cena brutto</th>
              <th className='text-left p-2 border-b'>Edytuj</th>
            </tr>
          </thead>
          <tbody>
            {updateData.prices.map((price, index) => (
              <tr key={index}>
                <td className='p-2 border-b'>{price.name}</td>
                <td className='p-2 border-b'>{price.brutto}</td>
                <td className='p-2 border-b'><button><FaEdit/></button></td>
              </tr>
            ))}
          </tbody>
        </table>

    </div>
  )
}

export default PriceListComponent