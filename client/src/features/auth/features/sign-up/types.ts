export type SignUpFormValues = {
  displayName: string
  email: string
  password: string
  confirmPassword: string
}

export type SignUpRequest = {
  displayName: string
  email: string
  password: string
}

export type SignUpResponse = {
  id: string
}
