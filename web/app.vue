<template>
  <div>
    <h1>Food Items</h1>
    <form @submit.prevent="addItem">
      <input v-model="newItem.Name" placeholder="Name" required />
      <input v-model="newItem.Genero" placeholder="Genero" required />
      <input v-model.number="newItem.Valor" type="number" placeholder="Valor" required />
      <input v-model.number="newItem.ValorAluguel" type="number" placeholder="Valor Aluguel" required />
      <input v-model="newItem.TipoDeMidia" placeholder="Tipo de Midia" required />
      <button type="submit">Add Item</button>
    </form>

    <div v-if="loading">Loading...</div>

    <ul v-if="!loading">
      <li v-for="item in foodItems" :key="item.ID">
        {{ item.Name }} - {{ item.Genero }} - {{ item.Valor }} - {{ item.ValorAluguel }} - {{ item.TipoDeMidia }}
        <button @click="openEditModal(item)">Edit</button>
        <button @click="deleteItem(item.ID)">Delete</button>
      </li>
    </ul>

    <!-- Edit Modal -->
    <div v-if="isEditModalOpen" class="modal">
      <div class="modal-content">
        <span class="close" @click="closeEditModal">&times;</span>
        <h2>Edit Item</h2>
        <form @submit.prevent="updateItem">
          <input v-model="currentItem.Name" placeholder="Name" required />
          <input v-model="currentItem.Genero" placeholder="Genero" required />
          <input v-model.number="currentItem.Valor" type="number" placeholder="Valor" required />
          <input v-model.number="currentItem.ValorAluguel" type="number" placeholder="Valor Aluguel" required />
          <input v-model="currentItem.TipoDeMidia" placeholder="Tipo de Midia" required />
          <button type="submit">Update Item</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const foodItems = ref([])
const loading = ref(true)
const isEditModalOpen = ref(false)
const newItem = ref({
  Name: '',
  Genero: '',
  Valor: 0,
  ValorAluguel: 0,
  TipoDeMidia: ''
})
const currentItem = ref({
  ID: null, // Inicializamos o ID como null
  Name: '',
  Genero: '',
  Valor: 0,
  ValorAluguel: 0,
  TipoDeMidia: ''
})

const fetchFoodItems = async () => {
  try {
    const response = await axios.get('https://localhost:7182/api/Food')
    foodItems.value = response.data
  } catch (error) {
    console.error('Error fetching food items:', error)
  } finally {
    loading.value = false
  }
}

const addItem = async () => {
  try {
    const response = await axios.post('https://localhost:7182/api/Food', newItem.value)
    foodItems.value.push(response.data)
    resetNewItem()
  } catch (error) {
    console.error('Error adding item:', error)
  }
}

const resetNewItem = () => {
  newItem.value = {
    Name: '',
    Genero: '',
    Valor: 0,
    ValorAluguel: 0,
    TipoDeMidia: ''
  }
}

const openEditModal = (item) => {
  currentItem.value = { ...item }
  currentItem.ID = item.ID; // Definimos o ID ao abrir o modal de edição
  isEditModalOpen.value = true
}

const closeEditModal = () => {
  isEditModalOpen.value = false
}

const updateItem = async () => {
  try {
    await axios.put(`https://localhost:7182/api/Food/${currentItem.value.ID}`, currentItem.value)
    const index = foodItems.value.findIndex(item => item.ID === currentItem.value.ID)
    foodItems.value[index] = { ...currentItem.value }
    closeEditModal()
  } catch (error) {
    console.error('Error updating item:', error)
  }
}

const deleteItem = async (id) => {
  try {
    await axios.delete(`https://localhost:7182/api/Food/${id}`)
    foodItems.value = foodItems.value.filter(item => item.ID !== id)
  } catch (error) {
    console.error('Error deleting item:', error)
  }
}

fetchFoodItems()
</script>

<style scoped>
/* Add your styles here */

.modal {
  display: block; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

.modal-content {
  background-color: #fefefe;
  margin: 15% auto; /* 15% from the top and centered */
  padding: 20px;
  border: 1px solid #888;
  width: 80%; /* Could be more or less, depending on screen size */
}

.close {
  color: #aaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: black;
  text-decoration: none;
  cursor: pointer;
}
</style>
