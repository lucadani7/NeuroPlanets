using NeuroPlanets.Enums;
using NeuroPlanets.Data;

namespace NeuroPlanets.Models;

public class Planet {
    public EnumPlanets Type { get; set; }
    public string Name => Type.ToString();
    public double DistanceAu { get; set; }
    public double RadiusKm { get; set; } 
    public double DensityKgM3 { get; set; }
    public double Mass { get; set; }
    public double OrbitalVelocity { get; set; }
    public double SentimentScore { get; set; } = 1.0;

    public Planet(EnumPlanets type, double distanceAu, double radiusKm, double densityKgM3) {
        Type = type;
        DistanceAu = distanceAu;
        RadiusKm = radiusKm;
        DensityKgM3 = densityKgM3;
        Mass = CalculateRealMass();
        OrbitalVelocity = CalculateOrbitalVelocity();
    }

    private double CalculateRealMass() {
        var radiusMeters = RadiusKm * 1000;
        var volume = 4.0 / 3.0 * Math.PI * Math.Pow(radiusMeters, 3);
        return DensityKgM3 * volume;
    }

    private double CalculateOrbitalVelocity() {
        var distanceMeters = DistanceAu * SolarSystemConstants.AuToKm * 1000;
        return Math.Sqrt(SolarSystemConstants.GravitationalConstant * SolarSystemConstants.SunMassKg / distanceMeters);
    }
    
    public double GetDistanceMillionKm() => Math.Round(DistanceAu * SolarSystemConstants.AuToKm, 2);
    public double GetVelocityKmPerSecond() => Math.Round(OrbitalVelocity / 1000, 2);
}