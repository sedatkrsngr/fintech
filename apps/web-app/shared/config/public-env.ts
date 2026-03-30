const apiGatewayBaseUrl = process.env.NEXT_PUBLIC_API_GATEWAY_BASE_URL;

if (!apiGatewayBaseUrl) {
  throw new Error("NEXT_PUBLIC_API_GATEWAY_BASE_URL is not configured.");
}

export const publicEnv = {
  apiGatewayBaseUrl
};
