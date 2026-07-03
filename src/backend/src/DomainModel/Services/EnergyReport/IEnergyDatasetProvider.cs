namespace DomainModel.Services.EnergyReport
{
    public interface IEnergyDatasetProvider
    {
        EnergyCollections GetCollections();
        IReadOnlyList<EnergyMunicipality> SearchMunicipalities(string? search, int take = 50);
        EnergyMunicipality? GetMunicipalityById(string municipalityId);
        EnergyFuelOption? GetFuelById(string fuelId);
        EnergyDeductionOption? GetDeductionById(string deductionId);
        EnergyWindowOption? GetOldFrameTypeById(string frameTypeId);
        EnergyWindowOption? GetOldGlassTypeById(string glassTypeId);
        EnergyOption? GetBuildingTypeById(string buildingTypeId);
        EnergyOption? GetExposureTypeById(string exposureTypeId);
    }
}
