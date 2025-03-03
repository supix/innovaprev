﻿namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class CEL : SingleDimMaterial
    {
        public CEL(long length_mm) : base(length_mm)
        {
        }

        public override string Description => "Frontalini"; //ex "celetti"

        protected override long? ClampMinValue => 1000;
        public override int Order => 210;
    }

}
