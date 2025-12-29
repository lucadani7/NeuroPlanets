using Microsoft.ML.Data;

namespace NeuroPlanets.Brain;

public class PlanetSentimentData {
    [LoadColumn(0)] public string? PlanetName { get; set; }
    [LoadColumn(1)] public string? DialogueLine { get; set; }
    
    // Label is what AI will try to predict. 
    // True = Positive Sentiment, False = Negative Sentiment
    [LoadColumn(2), ColumnName("Label")] 
    public bool Sentiment { get; set; }
}