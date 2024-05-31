<template>
    <div>
      <h1>Food Items</h1>
      <div v-if="loading">Loading...</div>
      <div v-else>
        <v-card v-for="item in foodItems" :key="item.id" class="mb-3">
          <v-card-title>{{ item.name }}</v-card-title>
          <v-card-text>
            <p>Calories: {{ item.calories }}</p>
            <p>Fat: {{ item.fat }}</p>
            <p>Carbs: {{ item.carbs }}</p>
            <p>Protein: {{ item.protein }}</p>
          </v-card-text>
          <v-card-actions>
            <v-btn color="primary" @click="editItem(item)">Edit</v-btn>
            <v-btn color="error" @click="deleteItem(item.id)">Delete</v-btn>
          </v-card-actions>
        </v-card>
      </div>
      <h2>Add New Food Item</h2>
      <v-form @submit.prevent="addItem">
        <v-text-field v-model="newItem.name" label="Name" required></v-text-field>
        <v-text-field v-model="newItem.calories" label="Calories" type="number" required></v-text-field>
        <v-text-field v-model="newItem.fat" label="Fat" type="number" required></v-text-field>
        <v-text-field v-model="newItem.carbs" label="Carbs" type="number" required></v-text-field>
        <v-text-field v-model="newItem.protein" label="Protein" type="number" required></v-text-field>
        <v-btn type="submit" color="primary">Add Item</v-btn>
      </v-form>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  
  const foodItems = ref([]);
  const loading = ref(true);
  const newItem = ref({
    name: '',
    calories: 0,
    fat: 0,
    carbs: 0,
    protein: 0,
  });
  
  const fetchFoodItems = async () => {
    try {
      const response = await axios.get('/api/Food');
      foodItems.value = response.data;
    } catch (error) {
      console.error(error);
    } finally {
      loading.value = false;
    }
  };
  
  const addItem = async () => {
    try {
      const response = await axios.post('/api/Food', newItem.value);
      foodItems.value.push(response.data);
      newItem.value = {
        name: '',
        calories: 0,
        fat: 0,
        carbs: 0,
        protein: 0,
      };
    } catch (error) {
      console.error(error);
    }
  };
  
  const deleteItem = async (id) => {
    try {
      await axios.delete(`/api/Food/${id}`);
      foodItems.value = foodItems.value.filter(item => item.id !== id);
    } catch (error) {
      console.error(error);
    }
  };
  
  const editItem = (foodItem) => {
    const editedItem = { ...foodItem }; // Clone the foodItem to avoid direct mutations
    editedItem.name = prompt('Edit name:', editedItem.name) || editedItem.name;
    editedItem.calories = Number(prompt('Edit calories:', editedItem.calories)) || editedItem.calories;
    editedItem.fat = Number(prompt('Edit fat:', editedItem.fat)) || editedItem.fat;
    editedItem.carbs = Number(prompt('Edit carbs:', editedItem.carbs)) || editedItem.carbs;
    editedItem.protein = Number(prompt('Edit protein:', editedItem.protein)) || editedItem.protein;
  
    updateItem(editedItem);
  };
  
  const updateItem = async (editedItem) => {
    try {
      await axios.put(`/api/Food/${editedItem.id}`, editedItem);
      const index = foodItems.value.findIndex(item => item.id === editedItem.id);
      foodItems.value[index] = editedItem;
    } catch (error) {
      console.error(error);
    }
  };
  
  onMounted(() => {
    fetchFoodItems();
  });
  </script>
  
  <style scoped>
  /* Estilos específicos do componente */
  </style>
  