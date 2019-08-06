﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using Refit;

namespace ExternDotnetSDK.Clients.Docflows
{
    public class DocflowsClient : IDocflowsClient
    {
        public DocflowsClient(HttpClient client)
        {
            ClientRefit = RestService.For<IDocflowsClientRefit>(client);
        }

        public IDocflowsClientRefit ClientRefit { get; }

        public async Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null)
        {
            return await ClientRefit.GetDocflowsAsync(accountId, filter ?? new DocflowFilter());
        }

        public async Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId)
        {
            return await ClientRefit.GetDocflowAsync(accountId, docflowId);
        }

        public async Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId)
        {
            return await ClientRefit.GetDocumentsAsync(accountId, docflowId);
        }

        public async Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId)
        {
            return await ClientRefit.GetDocumentAsync(accountId, docflowId, documentId);
        }

        public async Task<DocflowDocumentDescription>
            GetDocumentDescriptionAsync(Guid accountId, Guid docflowId, Guid documentId)
        {
            return await ClientRefit.GetDocumentDescriptionAsync(accountId, docflowId, documentId);
        }

        public async Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId)
        {
            return await ClientRefit.GetEncryptedDocumentContentAsync(accountId, docflowId, documentId);
        }

        public async Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId)
        {
            return await ClientRefit.GetDecryptedDocumentContentAsync(accountId, docflowId, documentId);
        }

        public async Task<List<Signature>> GetDocumentSignaturesAsync(Guid accountId, Guid docflowId, Guid documentId)
        {
            return await ClientRefit.GetDocumentSignaturesAsync(accountId, docflowId, documentId);
        }

        public async Task<Signature> GetSignatureAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId)
        {
            return await ClientRefit.GetSignatureAsync(accountId, docflowId, documentId, signatureId);
        }

        public async Task<byte[]> GetSignatureContentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId)
        {
            return await ClientRefit.GetSignatureContentAsync(accountId, docflowId, documentId, signatureId);
        }

        public async Task<ApiTaskResult<byte[]>> GetApiTaskAsync(Guid accountId, Guid docflowId, Guid documentId, Guid apiTaskId)
        {
            return await ClientRefit.GetApiTaskAsync(accountId, docflowId, documentId, apiTaskId);
        }

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(Guid accountId, Guid docflowId, Guid documentId, Guid replyId)
        {
            return await ClientRefit.GetDocumentReplyAsync(accountId, docflowId, documentId, replyId);
        }

        public async Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] data)
        {
            var docData = new PrintDocumentData
            {
                Content = Convert.ToBase64String(data)
            };
            return await ClientRefit.PrintDocumentAsync(accountId, docflowId, documentId, docData);
        }

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId, Guid docflowId, Guid documentId, DecryptDocumentRequestData data)
        {
            return await ClientRefit.DecryptDocumentContentAsync(accountId, docflowId, documentId, data);
        }

        public async Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId, Guid docflowId, Guid documentId, string requestId, string code, bool unzip = false)
        {
            return await ClientRefit.ConfirmDocumentContentDecryptionAsync(accountId, docflowId, documentId, requestId, code, unzip);
        }
    }
}