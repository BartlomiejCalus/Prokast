import { NextResponse } from "next/server";

export async function POST(req: Request) {
  try {
    const body = await req.json();
    const { clientID, productName } = body;
    
    if (!clientID || !productName) {
      return NextResponse.json(
        { error: "clientID and productName are required" },
        { status: 400 }
      );
    }

    const apiUrl = `https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/products/Get?clientID=${clientID}`;

    const response = await fetch(apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ productName }),
    });

    if (!response.ok) {
      throw new Error(`Error fetching data: ${response.statusText}`);
    }

    const data = await response.json();
    return NextResponse.json(data, { status: 200 });
  } catch (error: unknown) {
    console.error("Error:", error);
    return NextResponse.json(
      { error: "Failed to fetch data from the API" },
      { status: 500 }
    );
  }
}
