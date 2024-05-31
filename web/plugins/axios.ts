import axios from 'axios'

export default defineNuxtPlugin((nuxtApp) => {
  const api = axios.create({
    baseURL: 'https://localhost:7182/api/Food',
  })

  nuxtApp.provide('axios', api)
})