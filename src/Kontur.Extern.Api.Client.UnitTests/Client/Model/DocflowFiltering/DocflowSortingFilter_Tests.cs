using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Common;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DocflowFiltering
{
    [TestFixture]
    internal class DocflowSortingFilter_Tests
    {
        [TestCase(SortOrder.Ascending)]
        [TestCase(SortOrder.Descending)]
        [TestCase(SortOrder.Unspecified)]
        public void OrderByCreationDate_should_apply_creation_date_sorting(SortOrder sortOrder)
        {
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.OrderByCreationDate(sortOrder).ApplyTo(docflowFilter);

            ShouldHaveExpectedQueryParameters(docflowFilter, ("orderBy", sortOrder.ToString().ToLower()));
        }

        [Test]
        public void UpdatedTo_should_apply_update_to_storing_filter()
        {
            var updateTo = new DateOnly(2021, 07, 08);
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.UpdatedTo(updateTo).ApplyTo(docflowFilter);
            
            ShouldHaveExpectedQueryParameters(docflowFilter, ("updatedTo", updateTo.ToString()));
        }

        [Test]
        public void UpdatedFrom_should_apply_update_from_storing_filter()
        {
            var updateFrom = new DateOnly(2021, 07, 08);
            var docflowFilter = new DocflowFilter();

            DocflowSortingFilter.UpdatedFrom(updateFrom).ApplyTo(docflowFilter);
            
            ShouldHaveExpectedQueryParameters(docflowFilter, ("updatedFrom", updateFrom.ToString()));
        }

        [Test]
        public void NoSorting_should_reset_sorting_filter()
        {
            var docflowFilter = new DocflowFilter();
            docflowFilter.SetUpdatedTo(new DateOnly(2021, 07, 06));
            docflowFilter.SetUpdatedFrom(new DateOnly(2021, 07, 08));
            docflowFilter.SetOrderBy(SortOrder.Ascending);

            DocflowSortingFilter.NoSorting.ApplyTo(docflowFilter);

            ShouldHaveExpectedQueryParameters(docflowFilter, Array.Empty<(string name, string value)>());
        }

        private static void ShouldHaveExpectedQueryParameters(DocflowFilter docflowFilter, params (string name, string value)[] expectedQueryParameters)
        {
            docflowFilter.ToQueryParameters().Should().BeEquivalentTo(expectedQueryParameters);
        }
    }
}