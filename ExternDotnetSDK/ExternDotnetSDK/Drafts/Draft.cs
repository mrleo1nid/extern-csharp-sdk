﻿using System;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts.Meta;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class Draft
    {
        public Guid Id { get; set; }
        public Link[] Docflows { get; set; }
        public Link[] Documents { get; set; }
        public DraftMeta Meta { get; set; }
        public DraftStatus Status { get; set; }
        public Link[] Links { get; set; }
    }
}