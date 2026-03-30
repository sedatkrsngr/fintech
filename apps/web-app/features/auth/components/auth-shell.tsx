"use client";

import type { ReactNode } from "react";

type AuthShellProps = {
  eyebrow: string;
  title: string;
  description: string;
  children: ReactNode;
};

export function AuthShell({
  eyebrow,
  title,
  description,
  children
}: AuthShellProps) {
  return (
    <main className="auth-page">
      <div className="auth-frame">
        <section className="auth-center">
          <section className="auth-card auth-card-main">
            <div className="card-heading">
              <div>
                <p className="eyebrow">{eyebrow}</p>
                <h1>{title}</h1>
              </div>
            </div>
            <p className="lead card-lead">{description}</p>
            {children}
          </section>
        </section>
      </div>
    </main>
  );
}
