using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct AccountPath
    {
        public AccountPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public AccountPath(IExternClientServices services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public OrganizationListPath Organizations => new(AccountId, Services);
        public DocflowListPath Docflows => new(AccountId, Services);
        public DraftListPath Drafts => new(AccountId, Services);
        public DraftBuilderListPath DraftBuilders => new(AccountId, Services);
        public ContentsPath Contents => new(AccountId, Services);
        public ReportsTableListPath ReportsTables => new(AccountId, Services);
        public HandbooksPath Handbooks => new(Services);
    }
}