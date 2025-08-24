import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import type { LogInSchema } from "../schemas/logInSchema";

import agent from "../agent";
import { useNavigate } from "react-router";
import type { RegisterSchema } from "../schemas/registerSchema";
import { toast } from "react-toastify";
import type { ChangePasswordSchema } from "../schemas/changePasswordSchema";

export const useAccount = () => {
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  const { data: currentUser, isLoading: loadingUserInfo } = useQuery({
    queryKey: ["user"],
    queryFn: async () => {
      const response = await agent.get<User>("/account/user-info");
      return response.data;
    },
    enabled: !queryClient.getQueryData(["user"]),
  });

  const registerUser = useMutation({
    mutationFn: async (creds: RegisterSchema) => {
      await agent.post("/account/register", creds);
    },
  });

  const logInUser = useMutation({
    mutationFn: async (creds: LogInSchema) => {
      await agent.post("/login?useCookies=true", creds);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["user"],
      });
    },
  });

  const verifyEmail = useMutation({
    mutationFn: async ({ userId, code }: { userId: string; code: string }) => {
      await agent.get(`/confirmEmail?userId=${userId}&code=${code}`);
    },
  });

  const resendConfirmationEmail = useMutation({
    mutationFn: async ({
      email,
      userId,
    }: {
      email?: string;
      userId?: string | null;
    }) => {
      await agent.get(`/account/resendConfirmEmail`, {
        params: {
          email,
          userId,
        },
      });
    },
    onSuccess: () => {
      toast.success("Email sent - please check your email");
    },
  });

  const changePassword = useMutation({
    mutationFn: async (data: ChangePasswordSchema) => {
      await agent.post("/account/change-password", data);
    },
  });

  const forgotPassword = useMutation({
    mutationFn: async (email: string) => {
      await agent.post("/forgotPassword", { email });
    },
  });

  const resetPassword = useMutation({
    mutationFn: async (data: ResetPassword) => {
      await agent.post("/resetPassword", data);
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
  

  return {
    registerUser,
    verifyEmail,
    resendConfirmationEmail,
    logInUser,
    changePassword,
    forgotPassword,
    resetPassword,
    logOutUser,
    currentUser,
    loadingUserInfo,
  };
};
