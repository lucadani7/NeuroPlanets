using NeuroPlanets.Data;
using NeuroPlanets.Models;

namespace NeuroPlanets.Engine;

public class PhysicsEngine {
    public List<PhysicsInteraction> CalculateInteractions(List<Planet> planets) {
        var interactions = new List<PhysicsInteraction>();
        for (var i = 0; i < planets.Count; i++) {
            for (var j = 0; j < planets.Count; j++) {
                if (i == j) {
                    continue;
                }
                var planetA = planets[i]; // Influencer
                var planetB = planets[j]; // Target
                
                // calculate distances in metres
                var distAu = Math.Abs(planetA.DistanceAu - planetB.DistanceAu);
                var distMeters = Math.Max(1000, distAu * SolarSystemConstants.AuToKm * 1000.0); // avoid possible division by 0
                
                // Newton formula: F = G * m1 * m2 / r^2
                var rawForce = SolarSystemConstants.GravitationalConstant * (planetA.Mass * planetB.Mass) / (distMeters * distMeters);
                
                // FEEDBACK LOOP: sentiment of both planets influences the final force
                var finalForce = rawForce * planetA.SentimentScore * planetB.SentimentScore;
                
                // save result in our container
                interactions.Add(new PhysicsInteraction
                {
                    Influencer = planetA,
                    Target = planetB,
                    Force = finalForce
                });
            }
        }
        
        // the most powerful forces are the first
        return interactions.OrderByDescending(x => x.Force).ToList();
    }
}