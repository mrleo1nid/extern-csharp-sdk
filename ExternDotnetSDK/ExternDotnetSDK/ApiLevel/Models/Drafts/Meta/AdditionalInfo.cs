﻿using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class AdditionalInfo
    {
        [DataMember]
        public string Subject { get; set; }
    }
}