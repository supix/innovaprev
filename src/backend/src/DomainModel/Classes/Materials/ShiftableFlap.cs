using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public abstract class ShiftableFlap : DoubleDimMaterial
    {
        private const long ForcedMechanismThreshold = 2400;

        public ShiftableFlap(long height_mm, long width_mm, string openingType, bool opaqueGlass, bool wireCover) : base(height_mm, width_mm, openingType, opaqueGlass, wireCover)
        {
        }
        protected string ForcedMechanismText => (Width_mm > ForcedMechanismThreshold ? " e meccanismo forzato" : string.Empty);
        public override decimal GetPrice(IMaterialVisitor visitor)
        {
            return base.GetPrice(visitor) +
                (Width_mm > ForcedMechanismThreshold ? 3000M : 1300M);
        }
    }
}
