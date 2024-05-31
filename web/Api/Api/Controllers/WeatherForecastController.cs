using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private static List<Games> _gamesList = new List<Games>();
    private static int _nextId = 1;
    private readonly ILogger<FoodController> _logger;

    public FoodController(ILogger<FoodController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "PostGames")]
    public IActionResult Post([FromBody] Games games)
    {
        if (games == null)
        {
            return BadRequest("Game item is null");
        }

        // Assign an ID to the game item
        games.ID = _nextId++;

        // Log the received game item for debugging purposes
        _logger.LogInformation($"Received game: ID={games.ID}, Name={games.Name}, Genero={games.Genero}, Valor={games.Valor}, ValorAluguel={games.ValorAluguel}, TipoDeMidia={games.TipoDeMidia}");

        // Add the game item to the list
        _gamesList.Add(games);

        return Ok(games);
    }

    [HttpGet(Name = "GetAllGames")]
    public IActionResult Get()
    {
        // Log the retrieval of game items for debugging purposes
        _logger.LogInformation("Retrieving all game items");

        // Return the list of game items
        return Ok(_gamesList);
    }

    [HttpDelete("{id}", Name = "DeleteGames")]
    public IActionResult Delete(int id)
    {
        // Find the game item by ID
        var games = _gamesList.FirstOrDefault(g => g.ID == id);
        if (games == null)
        {
            return NotFound($"Game item with ID {id} not found");
        }

        // Remove the game item from the list
        _gamesList.Remove(games);

        // Log the deletion of the game item for debugging purposes
        _logger.LogInformation($"Deleted game: ID={games.ID}, Name={games.Name}, Genero={games.Genero}, Valor={games.Valor}, ValorAluguel={games.ValorAluguel}, TipoDeMidia={games.TipoDeMidia}");

        return Ok($"Game item with ID {id} deleted successfully");
    }

    [HttpPut("{id}", Name = "PutGames")]
    public IActionResult Put(int id, [FromBody] Games updatedGames)
    {
        if (updatedGames == null)
        {
            return BadRequest("Updated game item is null");
        }

        // Find the game item by ID
        var games = _gamesList.FirstOrDefault(g => g.ID == id);
        if (games == null)
        {
            return NotFound($"Game item with ID {id} not found");
        }

        // Update the properties of the found game item
        games.Name = updatedGames.Name;
        games.Genero = updatedGames.Genero;
        games.Valor = updatedGames.Valor;
        games.ValorAluguel = updatedGames.ValorAluguel;
        games.TipoDeMidia = updatedGames.TipoDeMidia;

        // Log the update of the game item for debugging purposes
        _logger.LogInformation($"Updated game: ID={games.ID}, Name={games.Name}, Genero={games.Genero}, Valor={games.Valor}, ValorAluguel={games.ValorAluguel}, TipoDeMidia={games.TipoDeMidia}");

        return Ok(games);
    }
}

    

