namespace DomainModel.Services.EnergyReport
{
    public interface IEnergySavingsCalculator
    {
        EnergyCalculationResult Calculate(EnergyCalculationInput input);
    }
}
