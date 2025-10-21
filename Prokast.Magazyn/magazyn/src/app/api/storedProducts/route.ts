import { NextResponse } from "next/server";
import axios from "axios";

export async function GET(req: Request) {
  try {
    const { searchParams } = new URL(req.url);
    const clientID = searchParams.get("clientID");
    const warehouseID = searchParams.get("warehouseID");
    
    if (!clientID || !warehouseID) {
      return NextResponse.json(
        { error: "clientID and warehouseID are required" },
        { status: 400 }
      );
    }

    const apiUrl = `https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/storedproducts`;
    //const apiUrl = `https://localhost:7207/api/storedproducts`;

    const response = await axios.get(apiUrl, {
      params: { clientID, warehouseID },
      headers: {
        "Content-Type": "application/json",
      },
    });

    return NextResponse.json(response.data, { status: 200 });
  } catch (error: unknown) {
    console.error("Server Error:", error);
    if (axios.isAxiosError(error) && error.response) {
      return NextResponse.json(
        { error: error.response.data },
        { status: error.response.status }
      );
    }
    return NextResponse.json(
      { error: "Unknown error" },
      { status: 500 }
    );
  }
}




