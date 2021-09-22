﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.Common
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PersonFullName
    {
        public PersonFullName(string lastSurname, string firstName, string patronymicName)
        {
            if (string.IsNullOrWhiteSpace(lastSurname))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(lastSurname));
            
            if (string.IsNullOrWhiteSpace(firstName))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(firstName));
            
            LastSurname = lastSurname;
            FirstName = firstName;
            PatronymicName = patronymicName;
        }
        
        /// <summary>
        /// Фамилия
        /// </summary>
        [JsonPropertyName("surname")]
        public string LastSurname { get; }

        /// <summary>
        /// Имя
        /// </summary>
        [JsonPropertyName("name")]
        public string FirstName { get; }

        /// <summary>
        /// Отчество
        /// </summary>
        [JsonPropertyName("patronymic")]
        public string PatronymicName { get; }

        public void Deconstruct(out string lastSurname, out string firstName, out string patronymicName)
        {
            lastSurname = LastSurname;
            firstName = FirstName;
            patronymicName = PatronymicName;
        }
    }
}