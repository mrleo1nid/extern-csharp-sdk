﻿using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnit
{
    /// <summary>
    /// Тип контролирующего органа
    /// </summary>
    public ControlUnitType Type { get; set; }

    /// <summary>
    /// Код контролирующего органа
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование контролирующего органа
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Регион
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Флаги особенностей работы контролирующего органа
    /// </summary>
    public ControlUnitFlags[] Flags { get; set; }

    /// <summary>
    /// Контакты
    /// </summary>
    public ControlUnitContacts Contacts { get; set; }

    /// <summary>
    /// Список сертификатов
    /// </summary>
    public ConcurrentDictionary<string, X509Certificate2> Certificates;
}