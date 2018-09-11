using System;
using System.Collections.Generic;

namespace MercadoPago.Resources
{
    public sealed class IdentificationType : Resource<IdentificationType>
    {
        public static List<IdentificationType> GetAll(bool useCache = false, string accessToken = null) => 
            GetList("/v1/identification_types", accessToken, useCache);

        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }
    }
}