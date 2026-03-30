import Link from "next/link";

export default function HomePage() {
  return (
    <main className="landing-page">
      <section className="landing-panel">
        <p className="eyebrow">Digital Banking Access</p>
        <h1>Secure customer entry for a modern banking platform.</h1>
        <p className="lead">
          This web app is the first customer-facing layer of the platform. It
          will host secure sign-in, onboarding, verification, password recovery,
          and later account dashboards.
        </p>
        <div className="landing-stats">
          <article>
            <strong>1 Gateway</strong>
            <span>All customer traffic enters through one protected edge.</span>
          </article>
          <article>
            <strong>Identity Core</strong>
            <span>Account lifecycle, tokens, and permissions stay centralized.</span>
          </article>
          <article>
            <strong>Notification Chain</strong>
            <span>Verification and reset messages are routed through managed providers.</span>
          </article>
        </div>
        <div className="hero-links">
          <Link href="/account">Go to Dashboard</Link>
          <Link href="/login">Go to Login</Link>
          <Link href="/register">Create Account</Link>
          <Link href="/forgot-password">Forgot Password</Link>
        </div>
      </section>
    </main>
  );
}
