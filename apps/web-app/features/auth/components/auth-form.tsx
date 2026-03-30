"use client";

import { useState, type FormEvent, type ReactNode } from "react";

type Field = {
  id: string;
  label: string;
  type?: string;
  placeholder?: string;
};

type AuthFormProps<TValues extends Record<string, string>> = {
  fields: Field[];
  submitLabel: string;
  initialValues: TValues;
  helperText: string;
  onSubmit: (values: TValues) => Promise<string>;
  footer?: ReactNode;
};

export function AuthForm<TValues extends Record<string, string>>({
  fields,
  submitLabel,
  initialValues,
  helperText,
  onSubmit,
  footer
}: AuthFormProps<TValues>) {
  const [values, setValues] = useState<TValues>(initialValues);
  const [status, setStatus] = useState<string>(helperText);
  const [statusType, setStatusType] = useState<"info" | "success" | "error">("info");
  const [submitting, setSubmitting] = useState(false);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setSubmitting(true);

    try {
      const message = await onSubmit(values);
      setStatusType("success");
      setStatus(message);
    } catch (error) {
      setStatusType("error");
      setStatus(error instanceof Error ? error.message : "Unexpected error.");
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <form className="auth-form" onSubmit={handleSubmit}>
      {fields.map((field) => (
        <label key={field.id} className="field">
          <span>{field.label}</span>
          <input
            type={field.type ?? "text"}
            value={values[field.id]}
            placeholder={field.placeholder}
            onChange={(event) =>
              setValues((current) => ({
                ...current,
                [field.id]: event.target.value
              }))
            }
          />
        </label>
      ))}

      <button type="submit" disabled={submitting}>
        {submitting ? "Submitting..." : submitLabel}
      </button>

      <p className={`form-status form-status-${statusType}`}>{status}</p>
      {footer}
    </form>
  );
}
