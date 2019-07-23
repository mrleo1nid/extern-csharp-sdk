﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class FnsInventoryDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        [Required]
        [DataMember]
        public string ClaimItemNumber { get; set; }

        [Required]
        [DataMember]
        public string ScannedDocumentDate { get; set; }

        [Required]
        [DataMember]
        public string ScannedDocumentNumber { get; set; }

        [DataMember]
        public DraftsBuilderDocumentType? Type { get; set; }
    }
}