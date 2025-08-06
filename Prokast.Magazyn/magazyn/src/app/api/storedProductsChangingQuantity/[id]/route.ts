import { NextResponse } from 'next/server';
import axios from 'axios';

export async function PUT(req: Request, { params }: { params: { id: string } }) {
  try {
    const { id } = params;
    const { quantity, clientID } = await req.json();

    if (!id || !quantity || !clientID) {
      return NextResponse.json({ error: "ID, clientID, and quantity are required" }, { status: 400 });
    }

    const apiUrl = `https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/storedproducts/${id}?clientID=${clientID}&quantity=${quantity}`;

    const response = await axios.put(apiUrl, null, {
      headers: { "Content-Type": "application/json" },
    });

    return NextResponse.json(response.data);
  } catch (error: any) {
    return NextResponse.json({ error: error.response?.data || "Internal server error" }, { status: 500 });
  }
}
