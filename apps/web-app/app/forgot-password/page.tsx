"use client";

import Link from "next/link";
import { requestPasswordReset } from "@/features/auth/api/auth-client";
import { AuthForm } from "@/features/auth/components/auth-form";
import { AuthShell } from "@/features/auth/components/auth-shell";
import { authRoutes } from "@/features/auth/constants/auth-routes";

export default function ForgotPasswordPage() {
  return (
    <AuthShell
      eyebrow="Recovery"
      title="Forgot Password"
      description="Enter your email address and we will send a password reset link."
    >
      <AuthForm
        fields={[{ id: "email", label: "Email", type: "email", placeholder: "you@example.com" }]}
        submitLabel="Send Reset Link"
        helperText="Use the same email address registered in identity-service."
        initialValues={{ email: "" }}
        onSubmit={async (values) => {
          await requestPasswordReset(values.email);

          return "If the email exists, a reset link was sent.";
        }}
        footer={
          <div className="form-links">
            <p className="form-footer">
              Remembered your password? <Link href={authRoutes.login}>Login here</Link>.
            </p>
          </div>
        }
      />
    </AuthShell>
  );
}
