import { NextResponse } from 'next/server';
import axios, { AxiosError } from 'axios';

export async function POST(req: Request) {
  console.log("Login API route accessed");

  try {
    const { Login, Password } = await req.json();

    // Log request details
    console.log("Sending request to external API...");
    console.log("Request URL:", 'https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/login');
    console.log("Request Headers:", {
      'Content-Type': 'application/json'
    });
    console.log("Request Body:", { Login, Password });

    // Send request to external API
    const response = await axios.post(
      'https://prokast-axgwbmd6cnezbmet.polandcentral-01.azurewebsites.net/api/login',
      { Login, Password },
      { headers: { 'Content-Type': 'application/json' } }
    );
    
    // Log response details on success
    console.log("Response received from external API:");
    console.log("Status:", response.status);
    console.log("Data:", response.data);

    // Return the response from the external API
    return NextResponse.json(response.data, { status: 200 });
  } catch (error: unknown) {
  if (axios.isAxiosError(error)) {
    const axiosError = error as AxiosError; // 👈 tutaj dopiero rzutowanie
    console.error("Axios error details:", {
      message: axiosError.message,
      code: axiosError.code,
      status: axiosError.response?.status,
      data: axiosError.response?.data,
    });
  } else {
    console.error("Non-Axios error:", error);
  }
}
}
