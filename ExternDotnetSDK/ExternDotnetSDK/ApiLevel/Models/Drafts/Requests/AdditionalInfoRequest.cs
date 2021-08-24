﻿using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class AdditionalInfoRequest
    {
        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string[] AdditionalCertificates { get; set; }
    }
}