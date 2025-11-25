import axios from "axios";
import { cookies } from "next/headers";
import { NextResponse } from "next/server";

export async function GET() {
    
    const apilink = process.env.API_URL_DOCKER_HOST || "http://localhost:8080";
    try{
        const token = (await cookies()).get("token")?.value;
        console.log("Retrieved token from cookies:", token);

        if (!token) {
            return NextResponse.json({ message: "Brak tokena w ciasteczkach" }, { status: 401 });
        }

       const response = await axios.get(`${apilink}/api/storedproducts`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
          params: {
            warehouseID: 1,
          }
        });

                console.log("Wysyłam do backendu:", {
          url: `${apilink}/api/storedproducts`,
          token,
          params: { warehouseID: 1 }
        });
        console.log("Otrzymałem od backendu:", response.data);
        return NextResponse.json(response.data, { status: 200 });
    }
     catch (error) {
        console.error("Error fetching stored products:", error);
        return NextResponse.json(
            { message: "Nie udało się pobrać danych z magazynu." },
            { status: 500 }
        );
     }
}