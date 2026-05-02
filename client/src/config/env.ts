type ClientEnv = {
  baseApiUrl: string;
};

function getRequiredEnv(name: "VITE_BASE_API_URL") {
  const value = import.meta.env[name]?.trim();

  if (!value) {
    throw new Error(`Missing required environment variable: ${name}`);
  }

  return value;
}

export const env: ClientEnv = {
  baseApiUrl: getRequiredEnv("VITE_BASE_API_URL"),
};
