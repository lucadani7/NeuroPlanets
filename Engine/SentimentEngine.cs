using Microsoft.ML;
using NeuroPlanets.Brain;

namespace NeuroPlanets.Engine;

public class SentimentEngine {
    private readonly MLContext _mlContext = new(seed: 1);
    private ITransformer? _model;

    public void Train(List<PlanetSentimentData> trainingData) {
        var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);
        var pipeline = _mlContext.Transforms.Text.FeaturizeText(
                outputColumnName: "Features", 
                inputColumnName: nameof(PlanetSentimentData.DialogueLine))
            .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                labelColumnName: "Label", // <--- Change this to "Label" to match your attribute
                featureColumnName: "Features"));
        _model = pipeline.Fit(dataView);
    }

    public PlanetSentimentPrediction Predict(string line) {
        if (_model == null) {
            throw new Exception("Model training before prediction is mandatory!");
        }
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<PlanetSentimentData, PlanetSentimentPrediction>(_model);
        return predictionEngine.Predict(new PlanetSentimentData { DialogueLine = line });
    }
}