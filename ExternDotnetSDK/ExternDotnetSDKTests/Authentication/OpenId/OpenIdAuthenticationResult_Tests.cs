using FluentAssertions;
using Kontur.Extern.Client.Authentication.OpenId.Provider;
using NUnit.Framework;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Tests.Authentication.OpenId
{
    [TestFixture]
    internal class OpenIdAuthenticationResult_Tests
    {
        [Test]
        public void Should_set_auth_header_to_the_request()
        {
            var authResult = new OpenIdAuthenticationResult("access-token");

            var request = authResult.Apply(Request.Get("/some-url"));

            request.Headers?.Authorization.Should().Be("Bearer access-token");
        }
    }
}