// index.js
const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
const port = 3001;

app.use(cors());
app.use(bodyParser.json());

let foodItems = [];
let currentId = 1;

app.get('/', (req, res) => {
  res.send('Welcome to the Food API');
});

// Get all food items
app.get('/api/food', (req, res) => {
  res.json(foodItems);
});

// Add a new food item
app.post('/api/food', (req, res) => {
  const newItem = {
    ID: currentId++,
    Name: req.body.Name,
    Genero: req.body.Genero,
    Valor: req.body.Valor,
    ValorAluguel: req.body.ValorAluguel,
    TipoDeMidia: req.body.TipoDeMidia
  };
  foodItems.push(newItem);
  res.status(201).json(newItem);
});

// Delete a food item by ID
app.delete('/api/food/:id', (req, res) => {
  const id = parseInt(req.params.id, 10);
  foodItems = foodItems.filter(item => item.ID !== id);
  res.status(204).send();
});

// Update a food item by ID
app.put('/api/food/:id', (req, res) => {
  const id = parseInt(req.params.id, 10);
  const itemIndex = foodItems.findIndex(item => item.ID === id);
  
  if (itemIndex !== -1) {
    const updatedItem = {
      ID: id,
      Name: req.body.Name,
      Genero: req.body.Genero,
      Valor: req.body.Valor,
      ValorAluguel: req.body.ValorAluguel,
      TipoDeMidia: req.body.TipoDeMidia
    };
    foodItems[itemIndex] = updatedItem;
    res.json(updatedItem);
  } else {
    res.status(404).send('Item not found');
  }
});

app.listen(port, () => {
  console.log(`Food API listening at http://localhost:${port}`);
});
