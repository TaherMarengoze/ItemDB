﻿
namespace Interfaces.Models
{
    public interface ISpecListEntry
    {
        int ValueID { get; set; }

        string Value { get; set; }

        string Display { get; set; }
    }
}