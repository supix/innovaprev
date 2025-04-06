﻿using DomainModel.Classes.Products.Visitor;

namespace DomainModel.Classes.Materials
{
    public interface IMaterial
    {
        string Code { get; }
        string Description { get; }
        decimal GetPrice(IMaterialVisitor visitor);
        int NumberOfDimensions { get; }
        bool openingTypeVisible { get; }
        bool glassTypeVisible { get; }
        bool wireCoverVisible { get; }
        string[] MaterialForProduct { get; }
    }
}
