import { publicEnv } from "@/shared/config/public-env";

export type ApiResult<T> = {
  ok: boolean;
  status: number;
  data?: T;
  error?: string;
};

export async function postJson<TResponse>(
  path: string,
  body: unknown
): Promise<ApiResult<TResponse>> {
  const response = await fetch(`${publicEnv.apiGatewayBaseUrl}${path}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(body),
    cache: "no-store"
  });

  if (!response.ok) {
    const errorText = await response.text();

    return {
      ok: false,
      status: response.status,
      error: errorText || "Request failed."
    };
  }

  return {
    ok: true,
    status: response.status,
    data: (await response.json()) as TResponse
  };
}
