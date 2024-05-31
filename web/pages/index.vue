<template>
  <div>
    <h1>Food Items</h1>
    <form @submit.prevent="addItem">
      <input v-model="newItem.Name" placeholder="Name" required />
      <input v-model="newItem.Genero" placeholder="Genero" required />
      <input v-model="newItem.Valor" type="number" placeholder="Valor" required />
      <input v-model="newItem.VAlorAluguel" type="number" placeholder="Valor Aluguel" required />
      <input v-model="newItem.TipoDeMidia" placeholder="Tipo de Midia" required />
      <button type="submit">Add Item</button>
    </form>

    <div v-if="loading">Loading...</div>

    <ul v-if="!loading">
      <li v-for="item in foodItems" :key="item.ID">
        {{ item.Name }} - {{ item.Genero }} - {{ item.Valor }} - {{ item.VAlorAluguel }} - {{ item.TipoDeMidia }}
        <button @click="editItem(item)">Edit</button>
        <button @click="deleteItem(item.ID)">Delete</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const foodItems = ref([])
const loading = ref(true)
const newItem = ref({
  Name: '',
  Genero: '',
  Valor: 0,
  VAlorAluguel: 0,
  TipoDeMidia: ''
})

const fetchFoodItems = async () => {
  try {
    const response = await axios.get('http://localhost:5000/api/Food')
    foodItems.value = response.data
  } catch (error) {
    console.error(error)
  } finally {
    loading.value = false
  }
}

const addItem = async () => {
  try {
    const response = await axios.post('http://localhost:5000/api/Food', newItem.value)
    foodItems.value.push(response.data)
    newItem.value = {
      Name: '',
      Genero: '',
      Valor: 0,
      VAlorAluguel: 0,
      TipoDeMidia: ''
    }
  } catch (error) {
    console.error(error)
  }
}

const deleteItem = async (id) => {
  try {
    await axios.delete(`http://localhost:5000/api/Food/${id}`)
    foodItems.value = foodItems.value.filter(item => item.ID !== id)
  } catch (error) {
    console.error(error)
  }
}

const editItem = (item) => {
  const editedItem = { ...item }
  editedItem.Name = prompt('Edit name:', editedItem.Name) || editedItem.Name
  editedItem.Genero = prompt('Edit Genero:', editedItem.Genero) || editedItem.Genero
  editedItem.Valor = prompt('Edit Valor:', editedItem.Valor) || editedItem.Valor
  editedItem.VAlorAluguel = prompt('Edit Valor Aluguel:', editedItem.VAlorAluguel) || editedItem.VAlorAluguel
  editedItem.TipoDeMidia = prompt('Edit Tipo de Midia:', editedItem.TipoDeMidia) || editedItem.TipoDeMidia

  updateItem(editedItem)
}

const updateItem = async (editedItem) => {
  try {
    await axios.put(`http://localhost:5000/api/Food/${editedItem.ID}`, editedItem)
    const index = foodItems.value.findIndex(item => item.ID === editedItem.ID)
    foodItems.value[index] = editedItem
  } catch (error) {
    console.error(error)
  }
}

onMounted(() => {
  fetchFoodItems()
})
</script>

<style scoped>
/* Add your styles here */
</style>
