import axios from "axios";
import { store } from "./stores/store";
import { toast } from "react-toastify";
import { router } from "../app/router/Routes";

const sleep = (delay: number) =>
  new Promise((resolve) => setTimeout(resolve, delay));

const agent = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  withCredentials: true,
});

agent.interceptors.request.use((config) => {
  store.uiStore.isBusy();
  return config;
});

agent.interceptors.response.use(
  async (response) => {
    if (import.meta.env.DEV) await sleep(1000);
    store.uiStore.isIdle();
    return response;
  },
  async (error) => {
    if (import.meta.env.DEV) await sleep(1000);
    store.uiStore.isIdle();

    if (!error.response) {
      // network error or CORS
      toast.error("Network error – please check your connection.");
      return Promise.reject(new Error("NetworkError"));
    }

    const { status, data } = error.response;

    switch (status) {
      case 400:
        if (data?.errors) {
          const modalStateErrors: string[] = [];
          for (const key in data.errors) {
            if (data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }
          return Promise.reject(modalStateErrors.flat());
        } else {
          toast.error(data);
          return Promise.reject(new Error(typeof data === "string" ? data : "BadRequest"));
        }

      case 401:
        if (data?.detail === "NotAllowed") {
          return Promise.reject(new Error("NotAllowed"));
        } else {
          toast.error("Unauthorized");
          return Promise.reject(new Error("Unauthorized"));
        }

      case 404:
        router.navigate("/not-found");
        return Promise.reject(new Error("NotFound"));

      case 500:
        router.navigate("/server-error", { state: { error: data } });
        return Promise.reject(new Error("ServerError"));

      default:
        return Promise.reject(new Error("UnexpectedError"));
    }
  }
);

export default agent;
