using Microsoft.Azure.Cosmos;

namespace AzureManekineko.Data;

public class FortuneDollService
{
private Container _container;

    public FortuneDollService(
        CosmosClient dbClient,
        IConfiguration Configuration)
    {
        this._container = dbClient.GetContainer(Configuration["cosmoDBName"], Configuration["cosmoContainerName"]);
    }

    public async Task<List<FortuneDoll>> GetItemsAsync(string queryString)
    {
        var query = this._container.GetItemQueryIterator<FortuneDoll>(new QueryDefinition(queryString));
        List<FortuneDoll> results = new List<FortuneDoll>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();

            results.AddRange(response.ToList());
        }

        return results;
    }
}