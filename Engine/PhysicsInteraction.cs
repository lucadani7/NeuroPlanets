using NeuroPlanets.Models;

namespace NeuroPlanets.Engine;

public class PhysicsInteraction {
    public Planet Influencer { get; set; } = null!;
    public Planet Target { get; set; } = null!;
    public double Force { get; set; }
}