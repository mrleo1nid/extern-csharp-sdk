﻿using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
{
    public class DraftDocument
    {
        public Guid Id { get; set; }
        public Link DecryptedContentLink { get; set; }
        public Link EncryptedContentLink { get; set; }
        public Link SignatureContentLink { get; set; }
        public List<Signature> Signatures { get; set; }
        public DocumentDescription Description { get; set; }
        public List<DocumentContent> Contents { get; set; }
    }
}