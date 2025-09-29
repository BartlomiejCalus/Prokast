import { NextResponse } from "next/server";

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

    const response = await fetch(apiUrl, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
    });

    if (!response.ok) {
      const text = await response.text();
      return NextResponse.json({ error: `Failed to update product: ${text}` }, { status: response.status });
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (err: unknown) {
    const errorMessage = err instanceof Error ? err.message : "Internal server error";
    return NextResponse.json({ error: errorMessage }, { status: 500 });
  }
}
