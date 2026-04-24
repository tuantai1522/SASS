import ReactDOM from 'react-dom/client'
import { ThemeProvider } from '#/features/theme'
import { AuthProvider } from '#/features/auth'
import { createRouter } from '#/router'
import { RouterProvider } from '@tanstack/react-router'

const rootElement = document.getElementById('app')!

const router = createRouter()

if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement)
  root.render(
    <>
      <ThemeProvider>
        <AuthProvider>
          <RouterProvider router={router} />
        </AuthProvider>
      </ThemeProvider>
    </>,
  )
}
