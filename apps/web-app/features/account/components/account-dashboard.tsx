"use client";

import Link from "next/link";
import { useEffect, useMemo, useState } from "react";
import { useRouter } from "next/navigation";
import { authRoutes } from "@/features/auth/constants/auth-routes";
import {
  clearSessionTokens,
  getStoredSession,
  hasStoredSession
} from "@/features/auth/storage/session";

type DashboardCard = {
  label: string;
  value: string;
  detail: string;
};

const summaryCards: DashboardCard[] = [
  {
    label: "Available Balance",
    value: "$24,860.22",
    detail: "Primary customer wallet balance shown as a first dashboard reference."
  },
  {
    label: "Monthly Spending",
    value: "$3,420.15",
    detail: "Recent outgoing activity area that will later come from ledger and transfer data."
  },
  {
    label: "Security Status",
    value: "Protected",
    detail: "Identity, gateway, and notification flows are all part of the customer session chain."
  }
];

const recentItems = [
  {
    title: "Transfer to savings wallet",
    amount: "-$400.00",
    time: "Today, 09:40",
    status: "Completed"
  },
  {
    title: "Card settlement",
    amount: "-$128.45",
    time: "Yesterday, 18:10",
    status: "Completed"
  },
  {
    title: "Payroll deposit",
    amount: "+$2,950.00",
    time: "Yesterday, 08:00",
    status: "Incoming"
  }
];

export function AccountDashboard() {
  const router = useRouter();
  const [ready, setReady] = useState(false);

  useEffect(() => {
    if (!hasStoredSession()) {
      router.replace(authRoutes.login);
      return;
    }

    setReady(true);
  }, [router]);

  const sessionInfo = useMemo(() => getStoredSession(), []);

  if (!ready) {
    return (
      <main className="dashboard-page">
        <section className="dashboard-shell dashboard-loading">
          <p>Checking your session.</p>
        </section>
      </main>
    );
  }

  return (
    <main className="dashboard-page">
      <section className="dashboard-shell">
        <header className="dashboard-topbar">
          <div>
            <p className="eyebrow">Customer Banking</p>
            <h1 className="dashboard-title">Welcome back</h1>
            <p className="dashboard-subtitle">
              Your session is active. This area will evolve into the main customer account
              surface of the platform.
            </p>
          </div>
          <div className="dashboard-actions">
            <Link className="ghost-action" href={authRoutes.home}>
              Back to home
            </Link>
            <button
              type="button"
              className="ghost-action ghost-action-danger"
              onClick={() => {
                clearSessionTokens();
                router.push(authRoutes.login);
              }}
            >
              Logout
            </button>
          </div>
        </header>

        <section className="dashboard-hero">
          <div className="hero-balance-card">
            <p className="hero-balance-label">Primary account balance</p>
            <strong>$24,860.22</strong>
            <span>Updated for the authenticated customer shell.</span>
          </div>
          <div className="hero-side-card">
            <p className="hero-side-label">Session readiness</p>
            <strong>Authenticated</strong>
            <span>
              Access token stored: {sessionInfo.accessToken ? "yes" : "no"} · Refresh token
              stored: {sessionInfo.refreshToken ? "yes" : "no"}
            </span>
          </div>
        </section>

        <section className="summary-grid">
          {summaryCards.map((card) => (
            <article key={card.label} className="summary-card">
              <p>{card.label}</p>
              <strong>{card.value}</strong>
              <span>{card.detail}</span>
            </article>
          ))}
        </section>

        <section className="dashboard-grid">
          <article className="dashboard-panel">
            <div className="panel-heading">
              <h2>Recent activity</h2>
              <span>Customer timeline preview</span>
            </div>
            <div className="activity-list">
              {recentItems.map((item) => (
                <div key={`${item.title}-${item.time}`} className="activity-item">
                  <div>
                    <strong>{item.title}</strong>
                    <p>
                      {item.time} · {item.status}
                    </p>
                  </div>
                  <span>{item.amount}</span>
                </div>
              ))}
            </div>
          </article>

          <article className="dashboard-panel">
            <div className="panel-heading">
              <h2>Next product areas</h2>
              <span>Planned evolution of this screen</span>
            </div>
            <ul className="next-steps-list">
              <li>Wallet balances and multi-account cards will be connected here.</li>
              <li>Transfer history and filtering will move into this area.</li>
              <li>Notification preferences and security controls will sit in the right rail.</li>
              <li>Verified email and password lifecycle status can be shown as profile badges.</li>
            </ul>
          </article>
        </section>
      </section>
    </main>
  );
}
