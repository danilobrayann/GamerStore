using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private static List<Games> _gamess = new List<Games>();
    private static int _nextId = 1;
    private readonly ILogger<FoodController> _logger;

    public FoodController(ILogger<FoodController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Postgames")]
    public IActionResult Post([FromBody] Games games)
    {
        if (games == null)
        {
            return BadRequest("Food item is null");
        }

        // Assign an ID to the food item
        games.ID = _nextId++;

        // Log the received food item for debugging purposes
        _logger.LogInformation($"Received games: ID={games.ID}, Name={games.Name}, Calories={games.Genero}, Fat={games.Valor}, Carbs={games.VAlorAluguel}, Protein={games.TipoDeMidia}");

        // Add the food item to the list
        _gamess.Add(games);

        return Ok($"Food item received successfully with ID: {games.ID}");
    }

    [HttpGet(Name = "GetAllgamess")]
    public IActionResult Get()
    {
        // Log the retrieval of food items for debugging purposes
        _logger.LogInformation("Retrieving all food items");

        // Return the list of food items
        return Ok(_gamess);
    }

    [HttpDelete("{id}", Name = "Deletegames")]
    public IActionResult Delete(int id)
    {
        // Find the food item by ID
        var games = _gamess.FirstOrDefault(fi => fi.ID == id);
        if (games == null)
        {
            return NotFound($"Food item with ID {id} not found");
        }

        // Remove the food item from the list
        _gamess.Remove(games);

        // Log the deletion of the food item for debugging purposes
        _logger.LogInformation($"Deleted games: ID={games.ID}, Name={games.Name}, Calories={games.Genero}, Fat={games.Valor}, Carbs={games.VAlorAluguel}, Protein={games.TipoDeMidia}");

        return Ok($"Food item with ID {id} deleted successfully");
    }

    [HttpPut("{id}", Name = "Putgames")]
    public IActionResult Put(int id, [FromBody] Games updatedgames)
    {
        if (updatedgames == null)
        {
            return BadRequest("Updated food item is null");
        }

        // Find the food item by ID
        var games = _gamess.FirstOrDefault(fi => fi.ID == id);
        if (games == null)
        {
            return NotFound($"Food item with ID {id} not found");
        }

        // Update the properties of the found food item
        games.Name = updatedgames.Name;
        games.Genero = updatedgames.Genero;
    games.Valor = updatedgames.Valor;
        games.VAlorAluguel = updatedgames.VAlorAluguel;
        games.TipoDeMidia = updatedgames.TipoDeMidia;

        // Log the update of the food item for debugging purposes
        _logger.LogInformation($"Updated games: ID={games.ID}, Name={games.Name}, Calories={games.Genero}, Fat={games.Valor}, Carbs={games.VAlorAluguel}, Protein={games.TipoDeMidia}");

        return Ok($"Food item with ID {id} updated successfully");
    }
}
