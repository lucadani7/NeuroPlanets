# 🌌 NeuroPlanets Engine
Where Neural Networks Meet Celestial Mechanics

NeuroPlanets is an advanced interactive simulator that bridges the gap between Newtonian Astrophysics and Machine Learning. By utilizing ML.NET, the engine interprets user input to adjust the physical constants of a simulated solar system in real-time.

## 🚀 Core Features
- Interactive Feedback Loop: The universe reacts to your words. Positive or negative sentiments directly amplify or dampen gravitational forces.
- Physics-Aware Intelligence: An SDCA Logistic Regression model (Binary Classification) trained with N-Grams to understand context beyond simple keywords.
- High-Fidelity Calculations: Real-time gravitational force calculations using the Universal Law of Gravitation:
  $$F = G \frac{m_1 m_2}{r^2}$$
- Automated Data Logging: Generates session reports in CSV (for data analysis) and HTML (for visual presentation).
- Elegant Visualization: Powered by `Dumpify` for structured, color-coded console dashboards.

## 🛠️ System Architecture
1. The Physics Engine (Engine/PhysicsEngine.cs)
   - The engine calculates the N-Body interactions between planets. It introduces a unique "Sentiment Multiplier": $$F_{final} = F_{raw} \times PlanetA_{sentiment} \times PlanetB_{sentiment}$$ .This allows "planetary consciousness" to alter the fundamental laws of gravity based on the emotional state detected by the AI.
2. The Sentiment Engine (Engine/SentimentEngine.cs)
   - Powered by ML.NET, this component featurizes text data from scenes.json. It doesn't just look for words; it looks for patterns.
   - Positive Sentiment: Stabilizes and calms gravitational pull (Multiplier < 1.0).
   - Negative Sentiment: Destabilizes and intensifies attraction (Multiplier > 1.0).
3. The Theater Manager (Engine/TheaterManager.cs)
   - Acts as the Data Provider. It loads narrative scripts and converts them into an enumerable format suitable for training the Neural Network.

## 🖥️ Development Environment
This project was built using JetBrains Rider on .NET 9.

## 🔧 Compatibility
- Visual Studio 2022: Fully compatible. If NuGet packages are missing, right-click the Solution and select `Restore NuGet Packages`.

- VS Code: Compatible via the C# Dev Kit extension.

## 📦 Key Dependencies
- Microsoft.ML (v3.0.1+)
- Dumpify (v0.6.0+)

## 🏃 Installation & Usage
1. Clone the repository:
   ```bash
   git clone https://github.com/lucadani7/NeuroPlanets.git
   cd NeuroPlanets
   ```
2. Configuration: Ensure `Data/scenes.json` is set to Copy to Output Directory in your IDE properties.
   
3. Run the project:
   ```bash
   dotnet run
   ```
## 📊 Analytics & Reports
After the simulation concludes, check the /bin/Debug/net9.0/Exports/ directory for:
- Report_TIMESTAMP.csv: Raw data for Excel/Python plotting.
- Report_TIMESTAMP.html: A clean, dark-mode dashboard for a quick overview of the simulation results.

## 📄 License

This project is licensed under the Apache-2.0 License.
