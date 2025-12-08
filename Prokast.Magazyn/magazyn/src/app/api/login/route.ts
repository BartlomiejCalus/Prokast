import { NextResponse } from "next/server";
import axios, { AxiosError } from "axios";

export async function POST(req: Request) {
  
  const apilink = process.env.API_URL_DOCKER_HOST || "http://localhost:8080";
  try {
    const { Login, Password } = await req.json();

    console.log("Sending request to external API...");
    console.log(
      "Request URL:",
      "https://prokast-axgwbmd6cnezbmet.germanywestcentral-01.azurewebsites.net/api/login"
    );
    console.log("Request Headers:", {
      "Content-Type": "application/json",
    });
    console.log("Request Body:", { Login, Password });

    const response = await axios.post(
      "https://prokast-axgwbmd6cnezbmet.germanywestcentral-01.azurewebsites.net/api/login",
      { Login, Password },
      { headers: { "Content-Type": "application/json" } }
    );

    console.log("Response from external API:", response.data);

 
    const res = NextResponse.json(response.data, { status: 200 });

    res.cookies.set("token", response.data.token, {
      httpOnly: true,
      secure: true,
      sameSite: "strict",
      path: "/",
      maxAge: 60 * 60 * 24,
    });

    return res;

  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      const axiosErr = error as AxiosError;
      console.error("Axios error details:", {
        message: axiosErr.message,
        code: axiosErr.code,
        status: axiosErr.response?.status,
        data: axiosErr.response?.data,
      });
    }
    return NextResponse.json(
      { message: "An error occurred while processing the request." },
      { status: 500 }
    );
  }
}
