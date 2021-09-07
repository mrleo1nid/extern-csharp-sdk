﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Models.Docflows.Documents;

namespace Kontur.Extern.Client.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    internal class Docflow : IDocflowWithDocuments
    {
        [UsedImplicitly]
        public Docflow()
        {
        }

        public Docflow(
            Guid id,
            Guid organizationId,
            Urn type,
            Urn status,
            Urn successState,
            List<Document> documents,
            List<Link> links,
            DateTime sendDateTime,
            DateTime? lastChangeDateTime,
            DocflowDescription description)
        {
            Id = id;
            OrganizationId = organizationId;
            Type = type;
            Status = status;
            SuccessState = successState;
            Documents = documents;
            Links = links;
            SendDateTime = sendDateTime;
            LastChangeDateTime = lastChangeDateTime;
            Description = description;
        }

        internal Docflow(
            Guid id,
            Guid organizationId,
            Urn type,
            Urn status,
            Urn successState,
            List<Link> links,
            DateTime sendDateTime,
            DateTime? lastChangeDateTime,
            DocflowDescription description)
        {
            Id = id;
            OrganizationId = organizationId;
            Type = type;
            Status = status;
            SuccessState = successState;
            Links = links;
            SendDateTime = sendDateTime;
            LastChangeDateTime = lastChangeDateTime;
            Description = description;
        }
        
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Urn Type { get; set; }
        public Urn Status { get; set; }
        public Urn SuccessState { get; set; }
        public List<Document> Documents { get; set; }
        public List<Link> Links { get; set; }
        public DateTime SendDateTime { get; set; }
        public DateTime? LastChangeDateTime { get; set; }
        public DocflowDescription Description { get; set; }

        public bool IsEmpty => Description is null && Id == default && OrganizationId == default;
    }
}