import { Link, useNavigate } from '@tanstack/react-router'
import { useForm } from 'react-hook-form'
import { ThemeToggle } from '#/features/theme'
import { Badge, Button } from '#/shared/ui'
import type { SignInFormValues } from './types'
import { useSignInMutation } from './use-sign-in-mutation'
import { GoogleSignIn } from '../google-sign-in'
import { AuthInputField, AuthPasswordField } from '../../components/index'

export function SignInPage() {
  const navigate = useNavigate()
  const { signIn: login, isPending, errorMessage, reset } = useSignInMutation()

  const authPanelClassName =
    'grid w-full max-w-[38rem] justify-items-center gap-[1.3rem] rounded-[34px] border border-(--color-border) bg-[linear-gradient(180deg,rgba(255,252,247,0.96),rgba(255,252,247,0.84)),var(--color-surface)] p-5 text-center shadow-(--shadow-md) backdrop-blur-[18px] dark:bg-[rgba(24,27,31,0.82)] md:p-7 xl:p-8'

  const signInForm = useForm<SignInFormValues>({
    defaultValues: {
      email: '',
      password: '',
    },
  })

  const handleSignInSubmit = signInForm.handleSubmit((values) => {
    reset()

    login(values, {
      onSuccess: async () => {
        await navigate({ to: '/' })
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

          {errorMessage ? (
            <div className="w-full rounded-[20px] border border-(--color-border) bg-(--color-danger-soft) px-4 py-[0.95rem] text-(--color-danger)">
              <p className="m-0 leading-[1.6]">{errorMessage}</p>
            </div>
          ) : null}

          <form className="grid w-full gap-4" onSubmit={handleSignInSubmit}>
            <AuthInputField
              label="Email"
              type="email"
              placeholder="name@company.com"
              autoComplete="email"
              error={signInForm.formState.errors.email?.message}
              {...signInForm.register('email', {
                required: 'Email is required.',
                pattern: {
                  value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                  message: 'Enter a valid email address.',
                },
              })}
            />

            <AuthPasswordField
              label="Password"
              placeholder="Enter at least 8 characters"
              autoComplete="current-password"
              error={signInForm.formState.errors.password?.message}
              {...signInForm.register('password', {
                required: 'Password is required.',
                minLength: {
                  value: 8,
                  message: 'Password must be at least 8 characters.',
                },
              })}
            />

            <div className="mt-4 flex flex-col gap-4">
              <Button type="submit" variant="primary" disabled={isPending}>
                {isPending ? 'Signing in...' : 'Sign in'}
              </Button>

              <div
                className="grid grid-cols-[1fr_auto_1fr] items-center gap-3.5 text-sm text-(--color-text-muted)"
                aria-hidden="true"
              >
                <div className="h-px bg-(--color-border)" />
                <span>Or continue with</span>
                <div className="h-px bg-(--color-border)" />
              </div>

              <GoogleSignIn />
            </div>
          </form>
        </div>

        <div className="flex flex-wrap items-center gap-[0.35rem]">
          <span className="support-copy">Need an account?</span>
          <Link
            to="/sign-up"
            className="font-bold text-(--color-accent-strong) no-underline"
          >
            Sign up
          </Link>
        </div>
      </section>
    </main>
  )
}
