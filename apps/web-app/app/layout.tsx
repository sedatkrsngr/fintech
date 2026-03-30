import type { Metadata } from "next";
import "./globals.css";

export const metadata: Metadata = {
  title: "Fintech Web App",
  description: "Authentication and customer-facing flows for the fintech platform."
};

export default function RootLayout({
  children
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>{children}</body>
    </html>
  );
}
