using Microsoft.ML.Data;

namespace NeuroPlanets.Brain;

public class PlanetSentimentPrediction : PlanetSentimentData {
    [ColumnName("PredictedLabel")]
    public bool Prediction { get; set; }

    public float Probability { get; set; }
    public float Score { get; set; }
}