import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type { LogInSchema } from "../schemas/logInSchema";

import agent from "../agent";
import { useLocation, useNavigate } from "react-router";
import type { RegisterSchema } from "../schemas/registerSchema";
import { toast } from "react-toastify";

export const useAccount = () => {
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const location = useLocation();

  const registerUser = useMutation({
    mutationFn: async (creds: RegisterSchema) => {
      await agent.post("/account/register", creds);
    },
    onSuccess: () => {
      toast.success("Register successful - you can now login");
      navigate("/login");
    },
  });

  const logInUser = useMutation({
    mutationFn: async (creds: LogInSchema) => {
      await agent.post("/login?useCookies=true", creds);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["user"]
      });
    },
  });
  const logOutUser = useMutation({
    mutationFn: async () => {
      await agent.post("/account/logout");
    },
    onSuccess: () => {
      queryClient.removeQueries({ queryKey: ["user"] });
      queryClient.removeQueries({ queryKey: ["activities"] });
      navigate("/");
    },
  });

  const { data: currentUser, isLoading: loadingUserInfo } = useQuery({
    queryKey: ["user"],
    queryFn: async () => {
      const response = await agent.get<User>("/account/user-info");
      return response.data;
    },
    enabled: !queryClient.getQueryData(['user']) &&
      location.pathname !== "/login" &&
      location.pathname !== "/register",
  });
  return {
    registerUser,
    logInUser,
    currentUser,
    loadingUserInfo,
    logOutUser,
  };
};
