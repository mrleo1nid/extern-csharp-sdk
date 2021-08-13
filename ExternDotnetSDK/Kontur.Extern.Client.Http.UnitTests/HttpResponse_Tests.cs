using System;
using System.IO;
using System.Text;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.Http.ClusterClientAdapters;
using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Http.Exceptions;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Testing.Helpers;
using Vostok.Clusterclient.Core.Model;
using Xunit;

namespace Kontur.Extern.Client.Http.UnitTests
{
    public class HttpResponse_Tests
    {
        [Fact]
        public void GetBytes_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse();

            Action action = () => httpResponse.GetBytes();

            action.Should().Throw<ContractException>();
        }
        
        [Fact]
        public void GetBytes_should_extract_bytes_from_content()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(new Content(bytes));

            var actualBytes = httpResponse.GetBytes();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetBytes_should_extract_bytes_from_memory_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new MemoryStream(bytes));

            var actualBytes = httpResponse.GetBytes();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetBytes_should_extract_bytes_from_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(new MemoryStream(bytes)));

            var actualBytes = httpResponse.GetBytes();
            
            actualBytes.Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetBytesSegment_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse();

            Action action = () => httpResponse.GetBytesSegment();

            action.Should().Throw<ContractException>();
        }
        
        [Fact]
        public void GetBytesSegment_should_extract_bytes_from_content()
        {
            var bytes = new byte[] {0, 1, 2, 3, 4};
            var expectedBytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(new Content(new ArraySegment<byte>(bytes, 1, 3)));

            var actualBytes = httpResponse.GetBytesSegment();
            
            actualBytes.ToArray().Should().BeEquivalentTo(expectedBytes);
        }
        
        [Fact]
        public void GetBytesSegment_should_extract_bytes_from_memory_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new MemoryStream(bytes));

            var actualBytes = httpResponse.GetBytesSegment();
            
            actualBytes.ToArray().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetBytesSegment_should_extract_bytes_from_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(new MemoryStream(bytes)));

            var actualBytes = httpResponse.GetBytesSegment();
            
            actualBytes.ToArray().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetStream_should_return_response_stream()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(new MemoryStream(bytes)));

            var actualStream = httpResponse.GetStream();
            
            actualStream.ReadAllBytes().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetStream_should_return_stream_from_buffered_content()
        {
            var bytes = new byte[] {1, 2, 3};
            var httpResponse = CreateHttpResponse(new Content(bytes));

            var stream = httpResponse.GetStream();
            
            stream.ReadAllBytes().Should().BeEquivalentTo(bytes);
        }
        
        [Fact]
        public void GetMessage_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            Action action = () => httpResponse.GetMessage<Dto>();

            action.Should().Throw<ContractException>();
        }
        
        [Fact]
        public void GetMessage_should_deserialize_response_stream_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                stream: ToStream(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var dto = httpResponse.GetMessage<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public void GetMessage_should_deserialize_response_content_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var dto = httpResponse.GetMessage<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public void GetMessage_should_fail_when_content_type_is_not_a_json()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/pdf"));

            Action action = () => httpResponse.GetMessage<Dto>();

            action.Should().Throw<ContractException>();
        }
        
        [Fact]
        public void GetMessage_should_fail_when_content_type_is_plain_text()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text"));

            Action action = () => httpResponse.GetMessage<Dto>();

            action.Should().Throw<ContractException>();
        }
        
        [Fact]
        public void GetMessage_should_return_body_string_when_a_content_type_is_plain_text_but_the_return_type_is_a_string()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;encoding=utf-8"));

            var message = httpResponse.GetMessage<string>();

            message.Should().Be(json);
        }
        
        [Fact]
        public void GetMessage_should_deserialize_response_content_to_DTO_if_content_type_is_json_with_charset()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/json; charset=utf-8"));

            var dto = httpResponse.GetMessage<Dto>();
            
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public void GetMessage_should_fail_when_content_type_is_absent()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(ToContent(json));

            Action action = () => httpResponse.GetMessage<Dto>();

            action.Should().Throw<ContractException>();
        }
        
        [Fact]
        public void TryGetMessage_should_return_error_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }

        [Fact]
        public void TryGetMessage_should_deserialize_response_stream_to_DTO()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                stream: ToStream(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            var success = httpResponse.TryGetMessage<Dto>(out var dto);

            success.Should().BeTrue();
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public void TryGetMessage_should_return_error_when_content_type_is_not_a_json()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/pdf"));

            ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }
        
        [Fact]
        public void TryGetMessage_should_deserialize_response_content_to_DTO_if_content_type_is_json_with_charset()
        {
            const string json = @"{""data"":""some data""}";
            var expectedDto = new Dto {Data = "some data"};
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "application/json; charset=utf-8"));

            var success = httpResponse.TryGetMessage<Dto>(out var dto);

            success.Should().BeTrue();
            dto.Should().BeEquivalentTo(expectedDto);
        }
        
        [Fact]
        public void TryGetMessage_should_return_error_when_content_type_is_absent()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(ToContent(json));

            ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }
        
        [Fact]
        public void TryGetMessage_should_return_error_when_a_content_type_is_plain_text()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            ShouldReturnErrorWhenTryGetMessageOfDto(httpResponse);
        }
        
        [Fact]
        public void TryGetMessage_should_return_body_string_when_a_content_type_is_plain_text_but_the_return_type_is_a_string_and_response_has_content()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                ToContent(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            var success = httpResponse.TryGetMessage<string>(out var message);

            success.Should().BeTrue();
            message.Should().Be(json);
        }
        
        [Fact]
        public void TryGetMessage_should_return_body_string_when_a_content_type_is_plain_text_but_the_return_type_is_a_string_and_response_has_stream()
        {
            const string json = @"{""data"":""some data""}";
            var httpResponse = CreateHttpResponse(
                stream: ToStream(json),
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            var success = httpResponse.TryGetMessage<string>(out var message);

            success.Should().BeTrue();
            message.Should().Be(json);
        }
        
        [Fact]
        public void TryGetMessage_should_return_error_when_a_content_type_is_plain_text_and_return_type_string_but_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(
                headers: new Headers(1).Set(HeaderNames.ContentType, "plain/text;charset=utf-8"));

            var success = httpResponse.TryGetMessage<string>(out var message);

            success.Should().BeFalse();
            message.Should().BeNull();
        }

        private static void ShouldReturnErrorWhenTryGetMessageOfDto(IHttpResponse httpResponse)
        {
            var success = httpResponse.TryGetMessage<Dto>(out var dto);

            success.Should().BeFalse();
            dto.Should().BeNull();
        }
        
        [Fact]
        public void GetString_should_fail_when_response_has_no_body()
        {
            var httpResponse = CreateHttpResponse(headers: new Headers(1).Set(HeaderNames.ContentType, ContentTypes.Json));

            Action action = () => httpResponse.GetString();

            action.Should().Throw<ContractException>();
        }
        
        [Fact]
        public void GetString_should_deserialize_response_memory_stream_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(stream: ToStream(expectedBody));

            var body = httpResponse.GetString();
            
            body.Should().BeEquivalentTo(expectedBody);
        }
        
        [Fact]
        public void GetString_should_deserialize_response_stream_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(stream: new BufferedStream(ToStream(expectedBody)));

            var body = httpResponse.GetString();
            
            body.Should().BeEquivalentTo(expectedBody);
        }
        
        [Fact]
        public void GetString_should_deserialize_response_content_to_DTO()
        {
            const string expectedBody = "some data";
            var httpResponse = CreateHttpResponse(ToContent(expectedBody));

            var body = httpResponse.GetString();
            
            body.Should().BeEquivalentTo(expectedBody);
        }

        private static HttpResponse CreateHttpResponse(Content? content = null, Stream? stream = null, Headers? headers = null)
        {
            var dummyRequest = Request.Get(new Uri("/dummy", UriKind.Relative));
            return new HttpResponse(
                dummyRequest,
                new Response(ResponseCode.Ok, content, headers, stream),
                new JsonSerializer()
            );
        }

        private static MemoryStream ToStream(string json) => new(Encoding.UTF8.GetBytes(json));
        
        private static Content ToContent(string json) => new(Encoding.UTF8.GetBytes(json));

        private class Dto
        {
            [UsedImplicitly]
            public string? Data { get; set; }
        }
    }
}