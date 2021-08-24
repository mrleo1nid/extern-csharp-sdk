﻿using System;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class StatLetterDescription : DocflowDescription
    {
        public string FinalRecipient { get; set; }
        /// <summary>
        /// Код ТОГС, куда направляется письмо
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Идентификатор связанного документооборота, на который отправлен ответ
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        /// <summary>
        ///  Код ОКПО
        /// </summary>
        public string Okpo { get; set; }
    }
}