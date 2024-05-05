import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import fs from 'fs'
import dns from 'dns'

dns.setDefaultResultOrder('verbatim')

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
    https: {
      key: fs.readFileSync('./certs/localhost-key.pem'),
      cert: fs.readFileSync('./certs/localhost.pem')
    },
    port: 5183,
    strictPort: true,
    hmr: {
      clientPort: 5183
    }
  },
  build: {
    outDir: '../server/wwwroot',
    emptyOutDir: true
  }
})
