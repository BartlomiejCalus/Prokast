import { NextResponse } from "next/server";
import axios, { AxiosError } from "axios";

export async function POST(req: Request) {
  console.log("Login API route accessed");

  try {
    const { Login, Password } = await req.json();

    console.log("Sending request to external API...");
    console.log(
      "Request URL:",
      "https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/login"
    );
    console.log("Request Headers:", {
      "Content-Type": "application/json",
    });
    console.log("Request Body:", { Login, Password });

    const response = await axios.post(
      "https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/login",
      { Login, Password },
      { headers: { "Content-Type": "application/json" } }
    );

    console.log("Response received from external API:");
    console.log("Status:", response.status);
    console.log("Data:", response.data);

    return NextResponse.json(response.data, { status: 200 });
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      const axiosErr = error as AxiosError;
      console.error("Axios error details:", {
        message: axiosErr.message,
        code: axiosErr.code,
        status: axiosErr.response?.status,
        data: axiosErr.response?.data,
      });
    } else {
      console.error("Non-Axios error:", error);
    }

    return NextResponse.json(
      { message: "An error occurred while processing the request." },
      { status: 500 }
    );
  }
}
