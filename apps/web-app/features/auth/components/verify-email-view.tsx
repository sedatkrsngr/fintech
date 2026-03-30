"use client";

import { useEffect, useState } from "react";
import { confirmEmailVerification } from "@/features/auth/api/auth-client";
import { AuthShell } from "@/features/auth/components/auth-shell";

type VerifyEmailViewProps = {
  token: string;
};

export function VerifyEmailView({ token }: VerifyEmailViewProps) {
  const [status, setStatus] = useState("Checking verification link.");
  const [statusType, setStatusType] = useState<"info" | "success" | "error">("info");

  useEffect(() => {
    async function verify() {
      if (!token) {
        setStatusType("error");
        setStatus("Verification token is missing. Open this page from the email link.");
        return;
      }

      try {
        await confirmEmailVerification(token);
        setStatusType("success");
        setStatus("Email verified successfully. You can return to login.");
      } catch (error) {
        setStatusType("error");
        setStatus(error instanceof Error ? error.message : "Email verification failed.");
      }
    }

    void verify();
  }, [token]);

  return (
    <AuthShell
      eyebrow="Verification"
      title="Verify Email"
      description="Your email verification link is being checked."
    >
      <div className={`form-status form-status-${statusType}`}>{status}</div>
    </AuthShell>
  );
}
