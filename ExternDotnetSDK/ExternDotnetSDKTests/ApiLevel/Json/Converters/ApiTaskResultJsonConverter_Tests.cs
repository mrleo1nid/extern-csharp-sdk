#nullable enable
using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Models.Api;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Http.Serialization;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Json.Converters
{
    internal class ApiTaskResultJsonConverter_Tests
    {
        private IJsonSerializer serializer;

        [SetUp]
        public void SetUp() => 
            serializer = JsonSerializerFactory.CreateJsonSerializer();

        public static IEnumerable<ApiTaskResult<SuccessResult, FailureResult>> TaskOfTwoResultResults
        {
            get
            {
                var id = Guid.NewGuid();
                var taskType = new Urn("nid", "nss");
                var errorType = new Urn("api", "errors");

                yield return ApiTaskResult<SuccessResult, FailureResult>.Running(id, taskType);
                yield return ApiTaskResult<SuccessResult, FailureResult>.Success(new SuccessResult(Guid.NewGuid(), string.Empty, "success-data"), id, taskType);
                yield return ApiTaskResult<SuccessResult, FailureResult>.FailureResult(new FailureResult("failure", new Urn("nid", "nss"), "failure-data"), id, taskType);
                yield return ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(new ApiError(), id, taskType);
                yield return ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(new ApiError(HttpStatusCode.BadRequest, "message"), id, taskType);
                yield return ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(new ApiError(errorType, HttpStatusCode.BadRequest, "message"), id, taskType);
            }
        }

        [TestCaseSource(nameof(TaskOfTwoResultResults))]
        public void Should_serialize_deserialize_task_result_of_two_results_in_running_state(ApiTaskResult<SuccessResult, FailureResult> taskResult)
        {
            Should_serialize_deserialize_as_expected(taskResult);
        }

        public static IEnumerable<ApiTaskResult<SuccessResult>> TaskOfOneResultResults
        {
            get
            {
                var id = Guid.NewGuid();
                var taskType = new Urn("nid", "nss");
                var errorType = new Urn("api", "errors");

                yield return ApiTaskResult<SuccessResult>.Running(id, taskType);
                yield return ApiTaskResult<SuccessResult>.Success(new SuccessResult(Guid.NewGuid(), "type", "data"), id, taskType);
                yield return ApiTaskResult<SuccessResult>.TaskFailure(new ApiError(), id, taskType);
                yield return ApiTaskResult<SuccessResult>.TaskFailure(new ApiError(HttpStatusCode.BadRequest, "message"), id, taskType);
                yield return ApiTaskResult<SuccessResult>.TaskFailure(new ApiError(errorType, HttpStatusCode.BadRequest, "message"), id, taskType);
            }
        }
        
        [TestCaseSource(nameof(TaskOfOneResultResults))]
        public void Should_serialize_deserialize_task_result_of_one_results_in_running_state(ApiTaskResult<SuccessResult> taskResult)
        {
            Should_serialize_deserialize_as_expected(taskResult);
        }

        private void Should_serialize_deserialize_as_expected<T>(T taskResult)
        {
            var json = serializer.SerializeToIndentedString(taskResult);
            Console.WriteLine($"JSON: {json}");
            var deserialized = serializer.Deserialize<T>(json);

            deserialized.Should().BeEquivalentTo(taskResult);
        }

        [PublicAPI]
        internal class SuccessResult : IApiTaskResult
        {
            public SuccessResult(Guid id, string? type, string successData)
            {
                Id = id;
                Type = type;
                SuccessData = successData;
            }

            public Guid Id { get; }
            public string? Type { get; }
            public string SuccessData { get; }

            public bool IsEmpty => Id == Guid.Empty;
        }
        
        [PublicAPI]
        internal class FailureResult : IApiTaskResult
        {
            public FailureResult(string id, Urn type, string failureData)
            {
                Id = id;
                Type = type;
                FailureData = failureData;
            }

            public string Id { get; }
            public Urn Type { get; }
            public string FailureData { get; }

            public bool IsEmpty => string.IsNullOrWhiteSpace(FailureData);
        }
    }
}