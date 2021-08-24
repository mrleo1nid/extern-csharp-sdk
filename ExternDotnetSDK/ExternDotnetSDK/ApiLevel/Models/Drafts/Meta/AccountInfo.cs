﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class AccountInfo
    {
        private OrganizationInfo organization;

        [DataMember]
        [Required]
        public string Inn { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public OrganizationInfo Organization
        {
            get => organization;
            set => organization = value ?? new OrganizationInfo();
        }

        [DataMember]
        public string RegistrationNumberFss { get; set; }

        [DataMember]
        public string RegistrationNumberPfr { get; set; }
    }
}