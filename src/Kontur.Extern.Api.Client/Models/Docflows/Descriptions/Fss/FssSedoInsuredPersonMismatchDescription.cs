using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoInsuredPersonMismatchDescription : FssSedoDescription
    {
        /// <summary>
        /// СНИЛС
        /// </summary>
        public string? Snils { get; set; }
    }
}