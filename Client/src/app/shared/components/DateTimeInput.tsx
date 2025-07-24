import { DateTimePicker, type DateTimePickerProps } from '@mui/x-date-pickers';
import {
  useController,
  type FieldValues,
  type UseControllerProps
} from 'react-hook-form';

// ✅ Define reusable prop type with Date, excluding 'value' and 'onChange'
type Props<T extends FieldValues> = UseControllerProps<T> &
  DateTimePickerProps;

export default function DateTimeInput<T extends FieldValues>(props: Props<T>) {
  const { field, fieldState } = useController(props);

  return (
    <DateTimePicker
      {...props}
      value={field.value ?? null}
      onChange={(value) => field.onChange(value)}
      sx={{ width: '100%' }}
      slotProps={{
        textField: {
          onBlur: field.onBlur,
          error: !!fieldState.error,
          helperText: fieldState.error?.message
        }
      }}
    />
  );
}
