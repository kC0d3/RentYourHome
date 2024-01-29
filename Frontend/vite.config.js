import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import dotenv from 'dotenv'
import path from 'path'

dotenv.config({
  path: path.join('..', '.env')
});

const { SERVERHOST, SERVERPORT } = process.env;

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: `http://${SERVERHOST}:${SERVERPORT}`,
        changeOrigin: true,
        secure: false,
      }
    }
  }
});
