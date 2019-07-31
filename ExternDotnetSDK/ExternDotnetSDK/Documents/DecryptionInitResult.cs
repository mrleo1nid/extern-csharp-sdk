﻿using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DecryptionInitResult
    {
        public Link ConfirmLink { get; set; }
        public string RequestId { get; set; }
        public string TaskId { get; set; }
        public ConfirmTypeInternal ConfirmType { get; set; }
    }
}