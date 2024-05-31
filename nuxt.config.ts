// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  modules: [
    '@nuxtjs/axios',
  ],
  plugins: [
    '~/plugins/axios.ts',
  ],
})

