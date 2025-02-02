using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Materials.ConcreteMaterials
{
    public class FIX : DoubleDimMaterial
    {
        public FIX(long height_mm, long width_mm) : base(height_mm, width_mm)
        {
        }

        public override string Code => "FIX";

        public override string Description => "Fisso con fermavetro";

        protected override long ClampMinValue => 1500000;
    }
}
