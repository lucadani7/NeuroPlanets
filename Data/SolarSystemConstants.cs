using System.Collections.ObjectModel;
using NeuroPlanets.Enums;

namespace NeuroPlanets.Data;

public class SolarSystemConstants {
    public static readonly ReadOnlyDictionary<EnumPlanets, double> PlanetDistancesAu = 
        new ReadOnlyDictionary<EnumPlanets, double>(new Dictionary<EnumPlanets, double> {
            { EnumPlanets.Mercury, 0.39 },
            { EnumPlanets.Venus, 0.72 },
            { EnumPlanets.Earth, 1.0 },
            { EnumPlanets.Mars, 1.52 },
            { EnumPlanets.Jupiter, 5.20 },
            { EnumPlanets.Saturn, 9.58 },
            { EnumPlanets.Uranus, 19.2 },
            { EnumPlanets.Neptune, 30.1 },
            { EnumPlanets.Pluto, 39.5 }
        });
    
    public static readonly Dictionary<EnumPlanets, (double Radius, double Density)> PlanetPhysics = new() {
        { EnumPlanets.Mercury, (2439.7, 5427) },
        { EnumPlanets.Venus,   (6051.8, 5243) },
        { EnumPlanets.Earth,   (6371.0, 5514) },
        { EnumPlanets.Mars,    (3389.5, 3933) },
        { EnumPlanets.Jupiter, (69911, 1326) },
        { EnumPlanets.Saturn,  (58232, 687) },
        { EnumPlanets.Uranus,  (25362, 1271) },
        { EnumPlanets.Neptune, (24622, 1638) },
        { EnumPlanets.Pluto,   (1188.3, 1850) }
    };

    public const double AuToKm = 149_597_870.7; // value chosen by International Astronomical Union in 2012
    public const double GravitationalConstant = 6.67430e-11;
    public const double SunMassKg = 1.989e30;
}