import { NextRequest, NextResponse } from "next/server";
import { cookies } from "next/headers";

export async function PUT(
  req: NextRequest,
  context: { params: Promise<{ id: string }> }
) {
  const { id } = await context.params; 

  const apiBase = process.env.API_URL_DOCKER_HOST;

  if (!apiBase) {
    console.error("Brak API_URL_DOCKER_HOST!");
    return NextResponse.json(
      { error: "Missing API base URL" },
      { status: 500 }
    );
  }

  const { searchParams } = new URL(req.url);
  const quantity = searchParams.get("quantity");

  if (!quantity) {
    return NextResponse.json(
      { error: "Missing quantity parameter" },
      { status: 400 }
    );
  }

  const backendUrl = `https://prokast-axgwbmd6cnezbmet.germanywestcentral-01.azurewebsites.net/api/storedproducts/quantity/${id}?quantity=${quantity}`;

  const token = await (await cookies()).get("token")?.value;
  console.log("TOKEN W ROUTE.TS:", token);

  try {
    const resp = await fetch(backendUrl, {
      method: "PUT",
      cache: "no-store",
      headers: {
        Accept: "*/*",
        Authorization: `Bearer ${token}`
      }
    });

    const data = await resp.text();
    return new NextResponse(data, { status: resp.status });

  } catch (err) {
    console.error("BACKEND ERROR:", err);
    return NextResponse.json(
      { error: "Failed to reach backend" },
      { status: 500 }
    );
  }
}
