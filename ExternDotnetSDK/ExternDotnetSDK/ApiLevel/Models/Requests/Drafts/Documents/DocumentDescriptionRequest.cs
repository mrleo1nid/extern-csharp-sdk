﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Client.Models.Numbers.BusinessRegistration;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocumentDescriptionRequest
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType Type { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Тип контента
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Код документа по справочнику СВДРЕГ
        /// </summary>
        public SvdregCode SvdregCode { get; set; }

        /// <summary>
        /// Наименование файла документа
        /// </summary>
        public string OriginalFilename { get; set; }
    }
}