import { useState } from 'react'
import { Link } from '@tanstack/react-router'
import { useForm } from 'react-hook-form'
import { ThemeToggle } from '#/features/theme'
import { Badge, Button } from '#/shared/ui'
import type { SignUpFormValues } from './types'
import { useSignUpMutation } from './use-sign-up-mutation'
import { AuthInputField, AuthPasswordField } from '../../components/index'

export function SignUpPage() {
  const [successMessage, setSuccessMessage] = useState<string | null>(null)
  const { signUp, isPending, errorMessage } = useSignUpMutation()

  const authPanelClassName =
    'grid w-full max-w-[38rem] justify-items-center gap-[1.3rem] rounded-[34px] border border-(--color-border) bg-[linear-gradient(180deg,rgba(255,252,247,0.96),rgba(255,252,247,0.84)),var(--color-surface)] p-5 text-center shadow-(--shadow-md) backdrop-blur-[18px] dark:bg-[rgba(24,27,31,0.82)] md:p-7 xl:p-8'

  const signUpForm = useForm<SignUpFormValues>({
    defaultValues: {
      displayName: '',
      email: '',
      password: '',
      confirmPassword: '',
    },
  })

  const passwordValue = signUpForm.watch('password')

  const handleSignUpSubmit = signUpForm.handleSubmit((values) => {
    signUp(values, {
      onSuccess: () => {
        signUpForm.reset()
        setSuccessMessage('Account created successfully. You can sign in now.')
      },
    })
  })

  return (
    <main className="grid min-h-screen place-items-center p-4 xl:p-6">
      <section className={authPanelClassName}>
        <div className="flex w-full justify-center">
          <ThemeToggle />
        </div>

        <div className="grid w-full justify-items-center gap-4">
          <Badge tone="accent">SASS workspace</Badge>

          {successMessage ? (
            <div className="w-full rounded-[20px] border border-(--color-border) bg-(--color-success-soft) px-4 py-[0.95rem] text-(--color-success)">
              <p className="m-0 leading-[1.6]">{successMessage}</p>
            </div>
          ) : null}

          {errorMessage ? (
            <div className="w-full rounded-[20px] border border-(--color-border) bg-(--color-danger-soft) px-4 py-[0.95rem] text-(--color-danger)">
              <p className="m-0 leading-[1.6]">{errorMessage}</p>
            </div>
          ) : null}

          <form className="grid w-full gap-4" onSubmit={handleSignUpSubmit}>
            <AuthInputField
              label="Display name"
              type="text"
              placeholder="How should your team see you?"
              autoComplete="name"
              error={signUpForm.formState.errors.displayName?.message}
              {...signUpForm.register('displayName', {
                required: 'Display name is required.',
                maxLength: {
                  value: 256,
                  message: 'Display name must be 256 characters or less.',
                },
              })}
            />

            <AuthInputField
              label="Email"
              type="email"
              placeholder="name@company.com"
              autoComplete="email"
              error={signUpForm.formState.errors.email?.message}
              {...signUpForm.register('email', {
                required: 'Email is required.',
                pattern: {
                  value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                  message: 'Enter a valid email address.',
                },
              })}
            />

            <div className="grid w-full gap-[0.55rem] min-[960px]:grid-cols-2">
              <AuthPasswordField
                label="Password"
                placeholder="At least 8 characters"
                autoComplete="new-password"
                error={signUpForm.formState.errors.password?.message}
                {...signUpForm.register('password', {
                  required: 'Password is required.',
                  minLength: {
                    value: 8,
                    message: 'Password must be at least 8 characters.',
                  },
                })}
              />

              <AuthPasswordField
                label="Confirm password"
                placeholder="Repeat your password"
                autoComplete="new-password"
                error={signUpForm.formState.errors.confirmPassword?.message}
                {...signUpForm.register('confirmPassword', {
                  required: 'Please confirm your password.',
                  validate: (value) =>
                    value === passwordValue || 'Passwords do not match.',
                })}
              />
            </div>

            <div className="mt-4 flex flex-col gap-4">
              <Button type="submit" variant="primary" disabled={isPending}>
                {isPending ? 'Creating account...' : 'Create account'}
              </Button>
            </div>
          </form>
        </div>

        <div className="flex flex-wrap items-center gap-[0.35rem]">
          <span className="support-copy">Already have an account?</span>
          <Link
            to="/sign-in"
            className="font-bold text-(--color-accent-strong) no-underline"
          >
            Sign in
          </Link>
        </div>
      </section>
    </main>
  )
}
