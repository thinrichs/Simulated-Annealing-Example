
namespace FiveCardMatrix.interfaces
{
    interface ISimulatedAnnealingMathematicalDriver
    {
        double Temperature { get; set; }
        double Alpha { get; set; }
        double Epsilon { get; set; }
        int Iterations { get; set; }
    }
}
