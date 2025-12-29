using System.Text.Json;
using NeuroPlanets.Brain;
using NeuroPlanets.Utils;

namespace NeuroPlanets.Engine;

public class TheaterManager {
    private List<DialogueEntry> _flattenedScript = []; // replies queue
    private int _currentLineIndex; // cursor who remembers what line I left

    /// <summary>
    /// Reading the JSON file and loading the scenary in the memory
    /// </summary>
    public void LoadScenes(string fileName) {
        try {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", fileName);
            if (!File.Exists(filePath)) {
                Console.WriteLine($"[Theater] Critical Error: The {fileName} file does not exist at the {filePath} path!".Colorize(ConsoleColor.Red));
                return;
            }
            using var reader = new StreamReader(filePath);
            var jsonContent = reader.ReadToEnd();
            var loadedScenes = JsonSerializer.Deserialize<List<Scene>>(jsonContent) ?? [];
            _flattenedScript = loadedScenes.SelectMany(s => s.dialogues).ToList();
            Console.WriteLine($"[Theater] Scenary loaded successfully! {_flattenedScript.Count} replies ready.".Colorize(ConsoleColor.Green));
        } catch (Exception ex) {
            Console.WriteLine($"[Theater] Exception: {ex.Message}".Colorize(ConsoleColor.Red));
        }
    }


    /// <summary>
    /// Transform replies in "food" for AI.
    /// This method is called only once, at the beginning, for AI training.
    /// </summary>
    public List<PlanetSentimentData> GetTrainingData() => (from dialogue in _flattenedScript let isPositiveSentiment = dialogue.character != "Neptune" select new PlanetSentimentData { PlanetName = dialogue.character, DialogueLine = dialogue.line, Sentiment = isPositiveSentiment }).ToList();

    
    /// <summary>
    /// Delivers the next reply during the simulation.
    /// </summary>
    public DialogueEntry GetNextLine() {
        if (_flattenedScript.Count == 0) {
            return new DialogueEntry { character = "System", line = "Error: Script empty." };
        }
        var line = _flattenedScript[_currentLineIndex];
        _currentLineIndex = (_currentLineIndex + 1) % _flattenedScript.Count;
        return line;
    }
}