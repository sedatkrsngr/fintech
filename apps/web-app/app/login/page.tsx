"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { issueToken } from "@/features/auth/api/auth-client";
import { AuthForm } from "@/features/auth/components/auth-form";
import { AuthShell } from "@/features/auth/components/auth-shell";
import { authRoutes } from "@/features/auth/constants/auth-routes";
import { saveSessionTokens } from "@/features/auth/storage/session";

export default function LoginPage() {
  const router = useRouter();

  return (
    <AuthShell
      eyebrow="Authentication"
      title="Login"
      description="Sign in to continue to your account."
    >
      <AuthForm
        fields={[
          { id: "email", label: "Email", type: "email", placeholder: "you@example.com" },
          { id: "password", label: "Password", type: "password", placeholder: "Your password" }
        ]}
        submitLabel="Login"
        helperText="Enter your email and password to access your account."
        initialValues={{ email: "", password: "" }}
        onSubmit={async (values) => {
          const session = await issueToken(values);
          saveSessionTokens(session.accessToken, session.refreshToken);
          window.setTimeout(() => router.push(authRoutes.account), 250);

          return "Login successful. Redirecting to your dashboard.";
        }}
        footer={
          <div className="form-actions-row">
            <Link className="secondary-action" href={authRoutes.register}>
              New account
            </Link>
            <Link className="secondary-action" href={authRoutes.forgotPassword}>
              Forgot password
            </Link>
          </div>
        }
      />
    </AuthShell>
  );
}
