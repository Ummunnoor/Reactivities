import { useForm } from "react-hook-form";
import { useAccount } from "../../lib/hooks/useAccount";
import { zodResolver } from "@hookform/resolvers/zod";
import { Box, Button, Paper, Typography } from "@mui/material";
import { LockOpen } from "@mui/icons-material";
import TextInput from "../../app/shared/components/TextInput";
import { Link } from "react-router";
import { registerSchema, type RegisterSchema } from "../../lib/schemas/registerSchema";

export default function RegisterForm() {
  
  const { registerUser } = useAccount();
  const {
    control,
    handleSubmit,
    setError,
    formState: { isValid, isSubmitting },
  } = useForm<RegisterSchema>({
    mode: "onTouched",
    resolver: zodResolver(registerSchema),
  });
  const onSubmit = async (data: RegisterSchema) => {
    await registerUser.mutateAsync(data, {
        onError : (error) => {
            if (Array.isArray(error))
            {
                error.forEach(err => {
                    if(err.includes('email'))
                    {
                        setError('email' , {message: err});
                    }
                    else if(err.includes('password'))
                    {
                        setError('password' , {message: err})
                    }
                })
            }
        }
    })
  };
  return (
    <Paper
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{
        display: "flex",
        flexDirection: "column",
        p: 3,
        gap: 3,
        maxWidth: "md",
        mx: "auto",
        borderRadius: 3,
      }}
    >
      <Box
        display="flex"
        alignItems="center"
        justifyContent="center"
        gap={3}
        color="secondary.main"
      >
        <LockOpen fontSize="large" />
        <Typography variant="h4">Sign in</Typography>
      </Box>
      <TextInput label="Email" control={control} name="email" />
      <TextInput label="Display Name" control={control} name="displayName" />

      <TextInput
        label="Password"
        type="password"
        control={control}
        name="password"
      />
      <Button
        type="submit"
        disabled={!isValid || isSubmitting}
        variant="contained"
        size="large"
      >
        Register
      </Button>
      <Typography sx={{textAlign: 'center'}}>Already have an account?
        <Typography component={Link} to= '/login' color="primary" ml={2}>Sign in</Typography>
      </Typography>
    </Paper>
  );
}
