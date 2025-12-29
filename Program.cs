using NeuroPlanets.Data;
using NeuroPlanets.Engine;
using NeuroPlanets.Models;
using NeuroPlanets.Utils;
using Dumpify;
using NeuroPlanets.Enums;

namespace NeuroPlanets;

internal static class Program {
    private static void ExportResults(List<object> data) {
        var folder = Path.Combine(AppContext.BaseDirectory, "Exports");
        if (!Directory.Exists(folder)) {
            Directory.CreateDirectory(folder);
        }
        var fileId = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        // CSV Export
        var csvPath = Path.Combine(folder, $"Report_{fileId}.csv");
        var csvLines = new List<string> { "Planet,Response,Mood,Score,Gravity" };
        csvLines.AddRange(data.Select(d => {
            dynamic row = d;
            return $"{row.Planet},{row.Input.Replace(",", "")},{row.Mood},{row.AI_Score},{row.Final_Gravity}";
        }));
        File.WriteAllLines(csvPath, csvLines);

        // HTML Export
        var htmlPath = Path.Combine(folder, $"Report_{fileId}.html");
        var rows = string.Join("", data.Select(d => {
            dynamic r = d;
            return $"<tr><td>{r.Planet}</td><td>{r.Input}</td><td>{r.Mood}</td><td>{r.AI_Score}</td><td>{r.Final_Gravity}</td></tr>";
        }));
        File.WriteAllText(htmlPath, $"<html><body style='font-family:sans-serif;background:#121212;color:white;'><h2>Simulation Report</h2><table border='1' style='width:100%;border-collapse:collapse;'><tr><th>Planet</th><th>Input</th><th>Mood</th><th>Score</th><th>Gravity</th></tr>{rows}</table></body></html>");
        Console.WriteLine($"\n Reports generated in: {folder}".Colorize(ConsoleColor.Green));
    }
    
    
    private static List<Planet> GenerateSolarSystemFromConstants() {
        var planets = new List<Planet>();
        foreach (var planetType in Enum.GetValues<EnumPlanets>()) {
            var condition1 = SolarSystemConstants.PlanetDistancesAu.TryGetValue(planetType, out var distance);
            var condition2 = SolarSystemConstants.PlanetPhysics.TryGetValue(planetType, out var physicsData);
            if (condition1 && condition2) {
                // passing brute data, Planet class will make the tough calculations
                planets.Add(new Planet(
                    planetType, 
                    distance, 
                    physicsData.Radius, 
                    physicsData.Density
                ));
            }
        }
        return planets;
    }
    
    private static void Main() {
        // UI Configuration
        DumpConfig.Default.TableConfig.ShowTableHeaders = true;
        DumpConfig.Default.ColorConfig.TypeNameColor = new DumpColor("#808080"); 
        
        Console.WriteLine("NEUROPLANETS ENGINE - SYSTEM BOOT".Colorize(ConsoleColor.Cyan));
        
        // Initialize Theater & AI
        var theater = new TheaterManager();
        theater.LoadScenes("scenes.json");
        var aiBrain = new SentimentEngine();
        
        Console.Write("Training Neural Network... ".Colorize(ConsoleColor.Gray));
        var trainingData = theater.GetTrainingData();
        if (trainingData.Count == 0) {
            Console.WriteLine("ERROR: No training data found!".Colorize(ConsoleColor.Red));
            return;
        }
        aiBrain.Train(trainingData);
        Console.WriteLine("DONE".Colorize(ConsoleColor.Green));
        
        // Physics Initialization
        var solarSystem = GenerateSolarSystemFromConstants();
        var physics = new PhysicsEngine();
        
        Console.WriteLine($"\nSolar System initialized: {solarSystem.Count} bodies loaded.".Colorize(ConsoleColor.Yellow));
        Console.WriteLine("SIMULATION START. Interacting with planetary consciousness...".Colorize(ConsoleColor.Magenta));

        // Interactive Simulation Loop
        // We take the top interactions to keep the console session manageable
        var topInteractions = physics.CalculateInteractions(solarSystem).Take(5).ToList();
        var sessionResults = new List<object>();

        foreach (var interaction in topInteractions) {
            var scriptLine = theater.GetNextLine();
            
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine($"{interaction.Influencer.Name.Colorize(ConsoleColor.Yellow)} is pulling {interaction.Target.Name.Colorize(ConsoleColor.Cyan)}.");
            Console.WriteLine($"{interaction.Influencer.Name} transmits: \"{scriptLine.line}\"");
            
            // USER INTERACTION
            Console.Write($"Enter your response to {interaction.Influencer.Name}: ");
            var userResponse = Console.ReadLine() ?? "";

            // AI ANALYSIS (N-Grams / Context)
            var prediction = aiBrain.Predict(userResponse);

            // FEEDBACK LOOP: Sentiment affects Physics
            // If negative (false), force multiplier is 2.0 (Chaos). If positive, 0.5 (Stability).
            var emotionalMultiplier = prediction.Prediction ? 0.5 : 2.0;
            interaction.Influencer.SentimentScore = emotionalMultiplier;

            // Recalculate specific interaction with new sentiment
            var currentForce = interaction.Force * emotionalMultiplier;

            Console.WriteLine($"[AI LOG] Analysis: {(prediction.Prediction ? "Positive" : "Negative")} | Raw Score: {prediction.Score:F2}");

            sessionResults.Add(new {
                Planet = interaction.Influencer.Name,
                Input = userResponse.Length > 25 ? userResponse[..22] + "..." : userResponse,
                Mood = prediction.Prediction ? "Positive" : "Negative",
                AI_Score = prediction.Score.ToString("F2"),
                Final_Gravity = currentForce.ToString("E2")
            });
        }
        
        // 4. Final Dashboard
        Console.WriteLine("\n[NEUROPLANETS MONITORING LOG - FINAL REPORT]".Colorize(ConsoleColor.Cyan));
        sessionResults.Dump();

        // 5. DATA EXPORT (HTML & CSV)
        ExportResults(sessionResults);
        Console.WriteLine("\nSimulation Over. Press any key to exit...".Colorize(ConsoleColor.Blue));
        Console.ReadKey();
    }
}