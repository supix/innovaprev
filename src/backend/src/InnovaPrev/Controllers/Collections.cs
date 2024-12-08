using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InnovaPrev.Controllers
{
    [Route("api/collections")]
    [ApiController]
    public class Collections : ControllerBase
    {
        // GET: api/<InternalColor>
        [HttpGet]
        public CollectionsOutputDto Get()
        {
            return new CollectionsOutputDto() {
                Product = new[]
                {
                    new CollectionItem() { Id = "PRO_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "PRO_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "PRO_ROSSO", Desc = "Rosso" },
                },
                InternalColors = new[] {
                    new CollectionItem() { Id = "IC_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "IC_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "IC_ROSSO", Desc = "Rosso" },
                },
                ExternalColors = new[] {
                    new CollectionItem() { Id = "EC_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "EC_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "EC_ROSSO", Desc = "Rosso" },
                },
                AccessoryColors = new[] {
                    new CollectionItem() { Id = "AC_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "AC_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "AC_ROSSO", Desc = "Rosso" },
                },
                ClimateZones = new[] {
                    new CollectionItem() { Id = "CZ_GIALLO", Desc = "Giallo" },
                    new CollectionItem() { Id = "CZ_VERDE", Desc = "Verde" },
                    new CollectionItem() { Id = "CZ_ROSSO", Desc = "Rosso" },
                },
                WindowTypes = new[] {
                    new CollectionItem() { Id = "WT_GRANDE", Desc = "Grande" },
                    new CollectionItem() { Id = "WT_MEDIA", Desc = "Media" },
                    new CollectionItem() { Id = "WT_PICCOLA", Desc = "Piccola" },
                },
                OpeningTypes = new[] {
                    new CollectionItem() { Id = "OT_DX", Desc = "SX" },
                    new CollectionItem() { Id = "OT_SX", Desc = "DX" },
                },
                GlassTypes = new[] {
                    new CollectionItem() { Id = "GT_TRASPARENTE", Desc = "Trasparente" },
                    new CollectionItem() { Id = "GT_OPACO", Desc = "Opaco" },
                    new CollectionItem() { Id = "GT_AZZURRATO", Desc = "Azzurrato" },
                },
                Crosspieces = new[] {
                    new CollectionItem() { Id = "CRO_ALTA", Desc = "Alta" },
                    new CollectionItem() { Id = "CRO_MEDIA", Desc = "Media" },
                    new CollectionItem() { Id = "CRO_BASSA", Desc = "Bassa" },
                },
            };
        }
    }

    public class CollectionsOutputDto
    {
        public required CollectionItem[] Product { get; set; }
        public required CollectionItem[] InternalColors { get; set; }
        public required CollectionItem[] ExternalColors { get; set; }
        public required CollectionItem[] AccessoryColors { get; set; }
        public required CollectionItem[] ClimateZones{ get; set; }
        public required CollectionItem[] WindowTypes { get; set; }
        public required CollectionItem[] OpeningTypes { get; set; }
        public required CollectionItem[] GlassTypes { get; set; }
        public required CollectionItem[] Crosspieces { get; set; }
    }

    public class CollectionItem
    {
        public required string Id { get; set; }
        public required string Desc { get; set; }
    }
}
