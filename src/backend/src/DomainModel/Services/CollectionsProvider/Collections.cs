﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services.CollectionsProvider
{
    public class Collections
    {
        public required CollectionItem[] Product { get; set; }
        public required CollectionItem[] InternalColors { get; set; }
        public required CollectionItem[] ExternalColors { get; set; }
        public required CollectionItem[] AccessoryColors { get; set; }
        public required CollectionItem[] ClimateZones { get; set; }
        public required CollectionItem[] WindowTypes { get; set; }
        public required CollectionItem[] OpeningTypes { get; set; }
        public required CollectionItem[] GlassTypes { get; set; }
        public required CollectionItem[] Crosspieces { get; set; }
    }
}
