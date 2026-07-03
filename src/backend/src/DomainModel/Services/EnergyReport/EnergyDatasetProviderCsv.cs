using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace DomainModel.Services.EnergyReport
{
    internal class EnergyDatasetProviderCsv : IEnergyDatasetProvider
    {
        private readonly EnergyCollections collections;
        private readonly Dictionary<string, EnergyMunicipality> municipalitiesById;
        private readonly EnergyFuelOption[] fuels;
        private readonly EnergyDeductionOption[] deductions;
        private readonly EnergyWindowOption[] oldFrameTypes;
        private readonly EnergyWindowOption[] oldGlassTypes;
        private readonly EnergyRatioOption[] frameAreaRatios;
        private readonly EnergyOption[] permeabilityClasses;
        private readonly EnergyOption[] shadingOptions;
        private readonly EnergyOption[] buildingTypes;
        private readonly EnergyOption[] exposureTypes;

        public EnergyDatasetProviderCsv()
        {
            var dataDir = ResolveDataDirectory();

            var climateRows = ReadCsv(Path.Combine(dataDir, "dati_climatici.csv"));
            var fuelRows = ReadCsv(Path.Combine(dataDir, "combustibili_pci_costi.csv"));
            var deductionRows = ReadCsv(Path.Combine(dataDir, "parametri_detrazioni.csv"));
            var oldWindowRows = ReadCsv(Path.Combine(dataDir, "tabelle_infissi_esistenti.csv"));

            municipalitiesById = BuildMunicipalities(climateRows);
            fuels = fuelRows
                .Select(row => new EnergyFuelOption
                {
                    Id = Require(row, "fuel_id"),
                    Label = Require(row, "descrizione"),
                    Unit = Get(row, "unita_misura"),
                    Pci = ToDecimal(Get(row, "pci")),
                    PricePerUnit = ToDecimal(Get(row, "prezzo_unita")),
                    PriceLabel = Get(row, "label_prezzo"),
                    ExtraCoefficient = ToNullableDecimal(Get(row, "coefficiente_extra"))
                })
                .ToArray();

            deductions = deductionRows
                .Select(row => new EnergyDeductionOption
                {
                    Id = Require(row, "codice"),
                    Label = Require(row, "label"),
                    Percentage = ToDecimal(Get(row, "percentuale_detrazione")),
                    MaxExpense = ToDecimal(Get(row, "massimale_spesa")),
                    IsApplicable = string.Equals(Get(row, "detrazione_applicabile"), "si", StringComparison.OrdinalIgnoreCase)
                })
                .ToArray();

            oldFrameTypes = oldWindowRows
                .Where(row => string.Equals(Get(row, "categoria"), "materiale_telaio", StringComparison.OrdinalIgnoreCase))
                .Select(row => new EnergyWindowOption
                {
                    Id = Slugify(Require(row, "descrizione")),
                    Label = Require(row, "descrizione"),
                    Uw = ToDecimal(Get(row, "valore")),
                    Unit = Get(row, "unita")
                })
                .ToArray();

            oldGlassTypes = oldWindowRows
                .Where(row => string.Equals(Get(row, "categoria"), "vetratura", StringComparison.OrdinalIgnoreCase))
                .Select(row => new EnergyWindowOption
                {
                    Id = Slugify(Require(row, "descrizione")),
                    Label = Require(row, "descrizione"),
                    Uw = ToDecimal(Get(row, "valore")),
                    Unit = Get(row, "unita")
                })
                .ToArray();

            frameAreaRatios = oldWindowRows
                .Where(row => string.Equals(Get(row, "categoria"), "percentuale_area_telaio", StringComparison.OrdinalIgnoreCase))
                .Select(row => new EnergyRatioOption
                {
                    Id = string.Format(CultureInfo.InvariantCulture, "{0:0.##}", ToDecimal(Get(row, "valore"))),
                    Label = string.Format(CultureInfo.InvariantCulture, "{0:0%}", ToDecimal(Get(row, "valore"))),
                    Ratio = ToDecimal(Get(row, "valore"))
                })
                .ToArray();

            permeabilityClasses = oldWindowRows
                .Where(row => string.Equals(Get(row, "categoria"), "permeabilita_aria", StringComparison.OrdinalIgnoreCase))
                .Select(row => new EnergyOption
                {
                    Id = Slugify(Require(row, "descrizione")),
                    Label = Require(row, "descrizione")
                })
                .ToArray();

            shadingOptions = oldWindowRows
                .Where(row => string.Equals(Get(row, "categoria"), "schermatura_accessorio", StringComparison.OrdinalIgnoreCase))
                .Select(row => new EnergyOption
                {
                    Id = Slugify(Require(row, "descrizione")),
                    Label = Require(row, "descrizione")
                })
                .ToArray();

            buildingTypes = new[]
            {
                new EnergyOption { Id = "residenziale", Label = "Residenziale", Factor = 0.9M },
                new EnergyOption { Id = "non_residenziale", Label = "Non residenziale", Factor = 0.6M }
            };

            exposureTypes = new[]
            {
                new EnergyOption { Id = "verso_esterno", Label = "Verso l'esterno", Factor = 1M },
                new EnergyOption { Id = "verso_ambiente_non_riscaldato", Label = "Verso ambiente non riscaldato", Factor = 0.5M },
                new EnergyOption { Id = "su_terreno", Label = "Su terreno", Factor = 0.8M }
            };

            collections = new EnergyCollections
            {
                Fuels = fuels,
                Deductions = deductions,
                BuildingTypes = buildingTypes,
                ExposureTypes = exposureTypes,
                OldFrameTypes = oldFrameTypes,
                OldGlassTypes = oldGlassTypes,
                FrameAreaRatios = frameAreaRatios,
                PermeabilityClasses = permeabilityClasses,
                ShadingOptions = shadingOptions
            };
        }

        public EnergyCollections GetCollections() => collections;

        public IReadOnlyList<EnergyMunicipality> SearchMunicipalities(string? search, int take = 50)
        {
            IEnumerable<EnergyMunicipality> query = municipalitiesById.Values;
            var normalizedSearch = string.Empty;

            if (!string.IsNullOrWhiteSpace(search))
            {
                normalizedSearch = search.Trim().ToLowerInvariant();
                query = query.Where(item =>
                    item.Comune.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (item.Provincia?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (item.Regione?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    CreateMunicipalityId(item.Comune, item.Provincia).Contains(normalizedSearch, StringComparison.Ordinal));
            }

            return query
                .OrderBy(item => GetMunicipalitySearchRank(item, normalizedSearch))
                .ThenBy(item => item.Comune)
                .ThenBy(item => item.Provincia)
                .Take(Math.Max(1, take))
                .ToArray();
        }

        public EnergyMunicipality? GetMunicipalityById(string municipalityId)
        {
            municipalitiesById.TryGetValue(municipalityId, out var municipality);
            return municipality;
        }

        public EnergyFuelOption? GetFuelById(string fuelId) => fuels.FirstOrDefault(item => item.Id == fuelId);

        public EnergyDeductionOption? GetDeductionById(string deductionId) => deductions.FirstOrDefault(item => item.Id == deductionId);

        public EnergyWindowOption? GetOldFrameTypeById(string frameTypeId) => oldFrameTypes.FirstOrDefault(item => item.Id == frameTypeId);

        public EnergyWindowOption? GetOldGlassTypeById(string glassTypeId) => oldGlassTypes.FirstOrDefault(item => item.Id == glassTypeId);

        public EnergyOption? GetBuildingTypeById(string buildingTypeId) => buildingTypes.FirstOrDefault(item => item.Id == buildingTypeId);

        public EnergyOption? GetExposureTypeById(string exposureTypeId) => exposureTypes.FirstOrDefault(item => item.Id == exposureTypeId);

        private static Dictionary<string, EnergyMunicipality> BuildMunicipalities(
            IReadOnlyList<Dictionary<string, string>> climateRows)
        {
            var byId = new Dictionary<string, EnergyMunicipality>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in climateRows)
            {
                var comune = Require(row, "comune");
                var provincia = Get(row, "provincia");
                var id = CreateMunicipalityId(comune, provincia);

                if (!byId.TryGetValue(id, out var municipality))
                {
                    municipality = new EnergyMunicipality
                    {
                        Id = id,
                        Comune = comune,
                        Provincia = provincia,
                        Regione = Get(row, "regione"),
                        Cap = null
                    };
                    byId[id] = municipality;
                }

                municipality.AltitudineSlm = ToNullableInt(Get(row, "altitudine_slm"));
                municipality.GradiGiorno = ToNullableInt(Get(row, "gradi_giorno"));
                municipality.ZonaClimatica = Get(row, "zona_climatica");
                municipality.ZonaVento = Get(row, "zona_vento");
                municipality.Regione ??= Get(row, "regione");
            }

            return byId;
        }

        private static string ResolveDataDirectory()
        {
            var candidates = new[]
            {
                Path.Combine(AppContext.BaseDirectory, "Data", "Energy"),
                Path.Combine(AppContext.BaseDirectory, "DomainModel", "Data", "Energy"),
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../DomainModel/Data/Energy")),
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../DomainModel/Data/Energy")),
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../src/DomainModel/Data/Energy"))
            };

            var match = candidates.FirstOrDefault(Directory.Exists);
            if (match is not null)
            {
                return match;
            }

            throw new DirectoryNotFoundException("Unable to locate energy dataset directory.");
        }

        private static IReadOnlyList<Dictionary<string, string>> ReadCsv(string path)
        {
            var rows = new List<Dictionary<string, string>>();

            using var parser = new TextFieldParser(path);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.HasFieldsEnclosedInQuotes = true;
            parser.TrimWhiteSpace = false;

            var headers = parser.ReadFields()?
                .Select(header => (header ?? string.Empty).Trim().TrimStart('\uFEFF'))
                .ToArray()
                ?? Array.Empty<string>();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                if (fields is null)
                {
                    continue;
                }

                var row = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (var index = 0; index < headers.Length; index++)
                {
                    row[headers[index]] = index < fields.Length ? (fields[index] ?? string.Empty).Trim() : string.Empty;
                }
                rows.Add(row);
            }

            return rows;
        }

        private static string Get(IReadOnlyDictionary<string, string> row, string key)
        {
            return row.TryGetValue(key, out var value) ? value : string.Empty;
        }

        private static string Require(IReadOnlyDictionary<string, string> row, string key)
        {
            var value = Get(row, key);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Missing required CSV field '{key}'.");
            }

            return value;
        }

        private static decimal ToDecimal(string? value)
        {
            return ToNullableDecimal(value) ?? 0M;
        }

        private static decimal? ToNullableDecimal(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            if (decimal.TryParse(value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }

            return null;
        }

        private static int? ToNullableInt(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }

            return null;
        }

        private static string Slugify(string value)
        {
            var normalized = value.Trim().ToLowerInvariant();
            var chars = normalized.Select(ch => char.IsLetterOrDigit(ch) ? ch : '_').ToArray();
            return new string(chars)
                .Replace("__", "_")
                .Trim('_');
        }

        private static string CreateMunicipalityId(string comune, string? provincia)
        {
            var province = string.IsNullOrWhiteSpace(provincia) ? "na" : provincia.Trim().ToLowerInvariant();
            return $"{Slugify(comune)}__{province}";
        }

        private static int GetMunicipalitySearchRank(EnergyMunicipality municipality, string normalizedSearch)
        {
            if (string.IsNullOrWhiteSpace(normalizedSearch))
            {
                return 10;
            }

            var comune = municipality.Comune.ToLowerInvariant();
            if (comune == normalizedSearch)
            {
                return 0;
            }

            if (comune.StartsWith(normalizedSearch, StringComparison.Ordinal))
            {
                return 1;
            }

            if (comune.Contains(normalizedSearch, StringComparison.Ordinal))
            {
                return 2;
            }

            return 3;
        }
    }
}
