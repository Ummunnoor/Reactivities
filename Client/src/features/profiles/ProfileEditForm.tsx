import { Box, Button } from "@mui/material";
import TextInput from "../../app/shared/components/TextInput";
import { useParams } from "react-router";
import { useProfile } from "../../lib/hooks/useProfile";
import { useForm } from "react-hook-form";
import {
  editProfileSchema,
  type EditProfileSchema,
} from "../../lib/schemas/editProfileSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";

type Props = {
  setEditMode: (editMode: boolean) => void;
};

export default function ProfileEditForm({ setEditMode }: Props) {
  const { id } = useParams();
  const { updateProfile, profile } = useProfile(id);
  const {
    control,
    handleSubmit,
    reset,
    formState: { isDirty, isValid },
  } = useForm<EditProfileSchema>({
    resolver: zodResolver(editProfileSchema),
    mode: "onTouched",
  });

  const onSubmit = (data: EditProfileSchema) => {
    updateProfile.mutate(data, {
      onSuccess: () => setEditMode(false),
    });
  };

  useEffect(() => {
    reset({
      displayName: profile?.displayName,
      bio: profile?.bio || "",
    });
  }, [profile, reset]);

  return (
    <Box
      display="flex"
      flexDirection="column"
      component="form"
      alignContent="center"
      gap={3}
      mt={3}
      onSubmit={handleSubmit(onSubmit)}
    >
      <TextInput control={control} name="displayName" label="Display Name" />
      <TextInput
        control={control}
        name="bio"
        label="Add your bio"
        multiline
        rows={4}
      />
      <Button
        type="submit"
        variant="contained"
        disabled={!isValid || !isDirty || updateProfile.isPending}
      >
        Update Profile
      </Button>
    </Box>
  );
}
