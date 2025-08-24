import { Box, Button, Paper, Typography } from "@mui/material";

import type { ReactNode } from "react";
import {
  FormProvider,
  useForm,
  type FieldValues,
  type Resolver,
} from "react-hook-form";

type Props<TFormData extends FieldValues> = {
  title: string;
  children: ReactNode;
  onSubmit: (data: TFormData) => Promise<void>;
  icon: ReactNode;
  submitButtonText: string;
  resolver?: Resolver<TFormData>;
  reset? : boolean
};
export default function AccountFormWrapper<TFormData extends FieldValues>({
  title,
  children,
  onSubmit,
  icon,
  submitButtonText,
  resolver,
  reset
}: Props<TFormData>) {

  const methods = useForm<TFormData>({ resolver, mode: "onTouched" });
  const formSubmit = async (data: TFormData) => {
    await onSubmit(data);
    if(reset) methods.reset();
  }
  return (
    <FormProvider {...methods}>
      <Paper
        component="form"
        onSubmit={methods.handleSubmit(formSubmit)}
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
          {icon}
          <Typography variant="h4">{title}</Typography>
        </Box>
        {children}
        <Button
          type="submit"
          disabled={!methods.formState || methods.formState.isSubmitting}
          variant="contained"
          size="large"
        >
          {submitButtonText}
        </Button>
      </Paper>
    </FormProvider>
  );
}

