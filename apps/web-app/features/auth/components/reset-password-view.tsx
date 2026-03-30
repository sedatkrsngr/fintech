"use client";

import Link from "next/link";
import { confirmPasswordReset } from "@/features/auth/api/auth-client";
import { authRoutes } from "@/features/auth/constants/auth-routes";
import { AuthForm } from "@/features/auth/components/auth-form";
import { AuthShell } from "@/features/auth/components/auth-shell";

type ResetPasswordViewProps = {
  token: string;
};

export function ResetPasswordView({ token }: ResetPasswordViewProps) {
  return (
    <AuthShell
      eyebrow="Recovery"
      title="Create a New Password"
      description="Set a new password to restore access to your account."
    >
      <AuthForm
        fields={[
          { id: "newPassword", label: "New Password", type: "password", placeholder: "New password" }
        ]}
        submitLabel="Update Password"
        helperText={
          token
            ? "Your reset link is valid. Set a new password to continue."
            : "Reset token is missing. Open this page from the reset email."
        }
        initialValues={{ newPassword: "" }}
        onSubmit={async (values) => {
          if (!token) {
            throw new Error("Reset token is missing in the URL.");
          }

          await confirmPasswordReset({
            token,
            newPassword: values.newPassword
          });

          return "Password updated successfully. You can now sign in.";
        }}
        footer={
          <div className="form-links">
            <p className="form-footer">
              Want to go back? <Link href={authRoutes.login}>Login here</Link>.
            </p>
          </div>
        }
      />
    </AuthShell>
  );
}
