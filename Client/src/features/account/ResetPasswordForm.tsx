import { useNavigate, useSearchParams } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";
import {
  resetPasswordSchema,
  type ResetPasswordSchema,
} from "../../lib/schemas/resetPasswordSchema";
import { Typography } from "@mui/material";
import { toast } from "react-toastify";
import AccountFormWrapper from "./AccountFormWrapper";
import { LockOpen } from "@mui/icons-material";
import { zodResolver } from "@hookform/resolvers/zod";
import TextInput from "../../app/shared/components/TextInput";

export default function ResetPasswordForm() {
  const [params] = useSearchParams();
  const { resetPassword } = useAccount();
  const navigate = useNavigate();
  const email = params.get("email");
  const code = params.get("code");
  if (!email || !code)
    return <Typography>Invalid reset password code</Typography>;
  const onSubmit = async (data: ResetPasswordSchema) => {
    try {
      await resetPassword.mutateAsync(
        { email, resetCode: code, newPassword: data.newPassword },
        {
          onSuccess: () => {
            toast.success("Password reset successfully - you can now sign in");
            navigate("/login");
          },
        }
      );
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <AccountFormWrapper
      title="Reset your password"
      submitButtonText="Reset password"
      onSubmit={onSubmit}
      icon={<LockOpen fontSize="large" />}
      resolver={zodResolver(resetPasswordSchema)}
    >
      <TextInput label="New Password" type="password" name="newPassword" />
      <TextInput label="Confirm Password" type="password" name="confirmPassword" />

    </AccountFormWrapper>
  );
}
