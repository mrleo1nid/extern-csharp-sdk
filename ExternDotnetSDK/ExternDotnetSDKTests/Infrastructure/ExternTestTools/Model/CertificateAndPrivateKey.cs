using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Тестовый сертификат
    /// </summary>
    [DataContract]
    public class CertificateAndPrivateKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateAndPrivateKey" /> class.
        /// </summary>
        /// <param name="certificate">Публичная часть сертификата.</param>
        /// <param name="privateKey">Закрытый ключ.</param>
        /// <param name="pfx">Pfx (Pkcs12) сертификат с закрытым ключом, пароль по умолчанию, если был сгенерирован pfx формат сертификата с закрытым ключом.</param>
        public CertificateAndPrivateKey(byte[] certificate = default, string privateKey = default, byte[] pfx = default)
        {
            Certificate = certificate;
            PrivateKey = privateKey;
            Pfx = pfx;
        }

        /// <summary>
        /// Публичная часть сертификата
        /// </summary>
        /// <value>Публичная часть сертификата</value>
        [DataMember(Name = "certificate", EmitDefaultValue = false)]
        public byte[] Certificate { get; set; }

        /// <summary>
        /// Закрытый ключ
        /// </summary>
        /// <value>Закрытый ключ</value>
        [DataMember(Name = "privateKey", EmitDefaultValue = false)]
        public string PrivateKey { get; set; }

        /// <summary>
        /// Pfx (Pkcs12) сертификат с закрытым ключом, пароль по умолчанию - \&quot;1\&quot;, если был сгенерирован pfx формат сертификата с закрытым ключом.
        /// </summary>
        /// <value>Pfx (Pkcs12) сертификат с закрытым ключом, пароль по умолчанию - \&quot;1\&quot;, если был сгенерирован pfx формат сертификата с закрытым ключом.</value>
        [DataMember(Name = "pfx", EmitDefaultValue = false)]
        public byte[] Pfx { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CertificateAndPrivateKey {\n");
            sb.Append("  Certificate: ").Append(Certificate).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}