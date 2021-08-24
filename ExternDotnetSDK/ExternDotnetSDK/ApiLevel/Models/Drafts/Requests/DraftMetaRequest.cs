﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class DraftMetaRequest
    {
        [DataMember]
        [Required]
        public SenderRequest Sender { get; set; }

        [DataMember]
        [Required]
        public RecipientInfoRequest Recipient { get; set; }

        [DataMember]
        [Required]
        public AccountInfoRequest Payer { get; set; }

        [DataMember]
        public RelatedDocumentRequest RelatedDocument { get; set; }

        [DataMember]
        public AdditionalInfoRequest AdditionalInfo { get; set; }
    }
}