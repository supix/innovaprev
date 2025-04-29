namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class F1A : DoubleDimMaterial
    {
        public F1A(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }

        public override string Description => "Finestra 1 Anta";

        protected override long? ClampMinValue => 1500000;
        public override int Order => 10;
        public override string[] MaterialForProduct => base.GetNotAntaMaxAndNotScorrevoleProductCodes();
    }
}
