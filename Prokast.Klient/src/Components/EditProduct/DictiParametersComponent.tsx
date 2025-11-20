import { useEffect, useState } from "react";
import { DictionaryParam } from "../../models/DictionaryParam";
import Cookies from "js-cookie";
import axios from "axios";

const API_URL = process.env.REACT_APP_API_URL;

const DictiParmametersComponent = ({
  data,
  productId,
}: {
  data: DictionaryParam[];
  productId: string | undefined;
}) => {
  const [params, setParams] = useState<DictionaryParam[]>(data || []);

  const [selectedParam, setSelectedParam] = useState<DictionaryParam | null>(
    null
  );

  const [isUpdateOpen, setIsUpdateOpen] = useState(false);
  const [isDeleteOpen, setIsDeleteOpen] = useState(false);
  const [isAddOpen, setIsAddOpen] = useState(false);

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

  return <div></div>;
};
export default DictiParmametersComponent;
