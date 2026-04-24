import { Controller } from 'react-hook-form'
import type { Control, FieldPath, FieldValues } from 'react-hook-form'
import { TextInput } from '#/shared/ui'
import { FormField } from './form-field'

type ControlledTextInputProps<TFieldValues extends FieldValues> = {
  control: Control<TFieldValues>
  name: FieldPath<TFieldValues>
  label?: string
  placeholder?: string
  type?: 'text' | 'email' | 'password' | 'search'
  disabled?: boolean
}

export function ControlledTextInput<TFieldValues extends FieldValues>({
  control,
  name,
  label,
  placeholder,
  type = 'text',
  disabled = false,
}: ControlledTextInputProps<TFieldValues>) {
  return (
    <Controller
      control={control}
      name={name}
      render={({ field, fieldState }) => (
        <FormField label={label} error={fieldState.error?.message}>
          <TextInput
            {...field}
            value={typeof field.value === 'string' ? field.value : ''}
            placeholder={placeholder}
            type={type}
            disabled={disabled}
          />
        </FormField>
      )}
    />
  )
}
