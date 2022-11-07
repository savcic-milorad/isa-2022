using Xunit;

namespace TransfusionAPI.Application.IntegrationTests;

[CollectionDefinition("WebApplicationWithDatabaseCollectionFixture")]
public class WebApplicationWithDatabaseCollectionFixture : ICollectionFixture<WebApplicationWithDatabaseFixture>
{
}
