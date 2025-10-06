import { NextResponse } from "next/server";
import axios, { AxiosError } from "axios";

export async function PUT(req: Request, { params }: { params: Promise<{ id: string }> }) {
  try {
    const { id } = await params;
    const body = await req.json();
    const { clientID, quantity } = body ?? {};

    if (!id || !clientID || quantity == null) {
      return NextResponse.json(
        { error: "ID, clientID i quantity są wymagane" },
        { status: 400 }
      );
    }

    const apiUrl = `https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/storedproducts/${encodeURIComponent(
      id
    )}?clientID=${encodeURIComponent(clientID)}&quantity=${encodeURIComponent(quantity)}`;

    const response = await axios.put(apiUrl, null, {
      headers: { "Content-Type": "application/json" },
    });

    return NextResponse.json(response.data);
  } catch (err: unknown) {
    if (axios.isAxiosError(err)) {
      const axiosErr = err as AxiosError;
      const msg = axiosErr.response?.data ?? axiosErr.message ?? "Internal server error";
      const status = axiosErr.response?.status ?? 500;
      return NextResponse.json({ error: msg }, { status });
    }


    const genericError = err as Error;
    return NextResponse.json(
      { error: genericError.message ?? "Internal server error" },
      { status: 500 }
    );
  }
}
