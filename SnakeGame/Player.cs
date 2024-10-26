using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SnakeGame;

public class Player
{
    string filepath = "highscores.json";
    public string Name { get; set; }
    public int Score { get; set; }
    public GameState gameState = new GameState(10, 10);
    public MainWindow mainWindow = new MainWindow();    

    // Dictionary to store high scores with player names
    public Dictionary<string, int> _highscores = new Dictionary<string, int>();

    // Constructor
    public Player(string name, int score)
    {
        Name = name;
        Score = score;
    }

    // Method to add or update high scores
    public void AddHighscore(string playerName, int score)
    {
        if (_highscores.ContainsKey(playerName))
        {
            // Update the score if the new score is higher
            if (score > _highscores[playerName])
            {
                _highscores[playerName] = score;
            }
        }
        else
        {
            // Add new player score
            _highscores.Add(playerName, score);
        }
    }

    // Method to display high score list
 

    public void SaveHighscoresToJson()
    {
        // Serialize the _highscores dictionary to JSON
        string json = JsonSerializer.Serialize(_highscores, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("highscores.json", json);
    }

    public void ReadHighscoresFromJson()
    {
        if (File.Exists("highscores.json"))
        {
            // Read the JSON file and deserialize it to _highscores dictionary
            string json = File.ReadAllText("highscores.json");
            _highscores = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
        }
        else
        {
            _highscores = new Dictionary<string, int>();
        }
    }
    public void AddScore(string playerName, int score)
    {
        if (_highscores.ContainsKey(playerName))
        {
            if (score > _highscores[playerName])
            {
                _highscores[playerName] = score;
            }
        }
        else
        {
           
            _highscores[playerName] = score;
        }

        SaveHighscoresToJson();

    }




    // Property to retrieve sorted high score list
    public Dictionary<string, int> GetSortedHighscores()
    {
        return _highscores.OrderByDescending(kv => kv.Value)
                          .ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}