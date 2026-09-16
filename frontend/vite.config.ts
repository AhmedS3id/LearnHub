import react from '@vitejs/plugin-react'
import { defineConfig, loadEnv } from 'vite'

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '')
  // In local dev, requests to /api/* are proxied server-side to the ASP.NET
  // Core backend and the /api prefix is stripped. This makes every request
  // same-origin from the browser's point of view, so it works regardless of
  // the backend's CORS AllowedOrigins configuration.
  const proxyTarget = env.VITE_DEV_PROXY_TARGET || 'http://localhost:5242'

  return {
    plugins: [react()],
    server: {
      proxy: {
        '/api': {
          target: proxyTarget,
          changeOrigin: true,
          secure: false,
          rewrite: (path) => path.replace(/^\/api/, ''),
        },
      },
    },
  }
})
