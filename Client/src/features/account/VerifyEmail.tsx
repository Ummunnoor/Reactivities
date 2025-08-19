import { Link, useSearchParams } from "react-router";
import { useAccount } from "../../lib/hooks/useAccount";
import { useEffect, useRef, useState } from "react";
import { Box, Button, Divider, Paper, Typography } from "@mui/material";

export default function VerifyEmail() {
  const { verifyEmail, resendConfirmationEmail } = useAccount();
  const [status, setStatus] = useState("verifying");
  const [searchParams] = useSearchParams();
  const userId = searchParams.get("userId");
  const code = searchParams.get("code");
  const hasRun = useRef(false);

  useEffect(() => {
    if (code && userId && !hasRun.current) {
      hasRun.current = true;
      verifyEmail
        .mutateAsync({ userId, code })
        .then(() => setStatus("verified"))
        .catch(() => setStatus("failed"));
    }
  }, [code, userId, verifyEmail]);

  const getBody = () => {
    switch (status) {
      case "verifying":
        return <Typography>Verifying...</Typography>;
      case "failed":
        return (
          <Box
            display="flex"
            flexDirection="column"
            gap={2}
            justifyContent="center"
          >
            <Typography>
              Verification failed. You can try resending the verify link to your
              email
            </Typography>
            <Button
              onClick={() => resendConfirmationEmail.mutate({ userId })}
              disabled={resendConfirmationEmail.isPending}
            >
              Resend verification link
            </Button>
          </Box>
        );
      case "verified":
        return (
          <Box
            display="flex"
            flexDirection="column"
            gap={2}
            justifyContent="center"
          >
            <Typography>
              You have verified your account, continue to login email
            </Typography>
            <Button component={Link} to="/login">
              Go to Login
            </Button>
          </Box>
        );
    }
  };

  return (
    <Paper
      sx={{
        height: 400,
        display: "flex",
        justifyContent: "center",
        flexDirection: "column",
        alignItems: "center",
        p: 6,
      }}
    >
      <Typography gutterBottom variant="h3">
        Email Verification
      </Typography>
      <Divider />
      {getBody()}
    </Paper>
  );
}
