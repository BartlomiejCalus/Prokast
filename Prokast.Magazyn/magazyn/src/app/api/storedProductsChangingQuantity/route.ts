import { NextRequest, NextResponse } from "next/server";

export async function PUT(
  req: NextRequest,
  context: { params: { id: string } }
) {
  try {
    // Czekamy na `params` w asynchroniczny sposób
    const { id } = context.params;

    if (!id) {
      return NextResponse.json(
        { error: "Missing ID parameter" },
        { status: 400 }
      );
    }

    // Pobieramy dane z request body
    const { clientID, quantity } = await req.json();

    if (!clientID || !quantity) {
      return NextResponse.json(
        { error: "clientID and quantity are required" },
        { status: 400 }
      );
    }

    // Tworzenie URL API
    const apiUrl = `https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/storedproducts/${id}?clientID=${clientID}&quantity=${quantity}`;
    //const apiUrl = `https://localhost:7207/api/storedproducts/${id}?clientID=${clientID}&quantity=${quantity}`;


    // Wysłanie żądania do API
    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
    });

    if (!response.ok) {
      throw new Error("Failed to update product");
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (error: any) {
    return NextResponse.json(
      { error: error.message || "Internal server error" },
      { status: 500 }
    );
  }
}
