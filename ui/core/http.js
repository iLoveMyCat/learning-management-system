import { API_BASE_URL } from "./config.js";

export const http = {
  async request(endpoint, options = {}) {
    const fetchOptions = { ...options };

    if (
      fetchOptions.body &&
      !(fetchOptions.body instanceof FormData) &&
      !(fetchOptions.body instanceof Blob) &&
      typeof fetchOptions.body !== "string"
    ) {
      fetchOptions.body = JSON.stringify(fetchOptions.body);
      fetchOptions.headers = {
        "Content-Type": "application/json",
        ...(fetchOptions.headers || {}),
      };
    }

    const url = `${API_BASE_URL}${endpoint}`;
    const response = await fetch(url, fetchOptions);

    if (response.status === 204) return null;
    if (!response.ok) {
      // Bubble a minimal error keeping the message simple
      const text = await response.text().catch(() => "");
      throw new Error(text || `HTTP ${response.status}`);
    }
    return response.json();
  },
};
