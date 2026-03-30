"use client";

import Link from "next/link";
import { registerUser, requestEmailVerification } from "@/features/auth/api/auth-client";
import { AuthForm } from "@/features/auth/components/auth-form";
import { AuthShell } from "@/features/auth/components/auth-shell";
import { authRoutes } from "@/features/auth/constants/auth-routes";

export default function RegisterPage() {
  return (
    <AuthShell
      eyebrow="Onboarding"
      title="Register"
      description="Create a new account to start using the platform."
    >
      <AuthForm
        fields={[
          { id: "firstName", label: "First Name", placeholder: "Sedat" },
          { id: "lastName", label: "Last Name", placeholder: "Krsngr" },
          { id: "email", label: "Email", type: "email", placeholder: "you@example.com" },
          { id: "password", label: "Password", type: "password", placeholder: "Strong password" }
        ]}
        submitLabel="Create Account"
        helperText="Use your real email address so we can send a verification link."
        initialValues={{ firstName: "", lastName: "", email: "", password: "" }}
        onSubmit={async (values) => {
          const user = await registerUser(values);
          await requestEmailVerification(values.email);

          return `Account created successfully. A verification link was sent to ${user.email}.`;
        }}
        footer={
          <div className="form-links">
            <p className="form-footer">
              Already have an account? <Link href={authRoutes.login}>Login here</Link>.
            </p>
          </div>
        }
      />
    </AuthShell>
  );
}
