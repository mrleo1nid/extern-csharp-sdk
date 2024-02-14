﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Replies
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ReplyDocument
    {
        /// <summary>
        /// Идентификатор ответного документа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Контент ответного документа
        /// </summary>
        public byte[] Content { get; set; } = null!;

        /// <summary>
        /// Данные для подписи
        /// </summary>
        public byte[]? DataToSign { get; set; }

        /// <summary>
        /// Печатная форма ответного документа
        /// </summary>
        public byte[]? PrintContent { get; set; }

        /// <summary>
        /// Название файла
        /// </summary>
        public string? Filename { get; set; }

        /// <summary>
        /// Подпись пользователя
        /// </summary>
        public byte[]? Signature { get; set; }

        /// <summary>
        /// Ссылки для работы с ответным документом
        /// </summary>
        public List<Link> Links { get; set; } = null!;

        /// <summary>
        /// Идентификатор документооборота, в котором сформирован ответный документ
        /// </summary>
        public Guid DocflowId { get; set; }

        /// <summary>
        /// Идентификатор документа, на который был сформирован ответный документ
        /// </summary>
        public Guid DocumentId { get; set; }
    }
}