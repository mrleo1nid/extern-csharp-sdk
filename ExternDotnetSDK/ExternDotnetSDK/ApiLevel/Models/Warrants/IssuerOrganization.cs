﻿#nullable enable
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class IssuerOrganization
    {
        public string? Name { get; set; }
        public string? Inn { get; set; }
        public string? Kpp { get; set; }
    }
}