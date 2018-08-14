using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearchDemoII
{
    class Program
    {
        private static SearchServiceClient _searchClient;
        private static ISearchIndexClient _indexClient;
        private static string currentIndexName;

        private const ConsoleColor EnabledColour = ConsoleColor.White;
        private const ConsoleColor DisableColour = ConsoleColor.DarkRed;

        static void Main(string[] args)
        {
            string searchServiceName = ConfigurationManager.AppSettings["SearchServiceName"];
            string searchServiceApiKey = ConfigurationManager.AppSettings["SearchServiceApiKey"];
            _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(searchServiceApiKey));
            Task.WaitAll(MenuLoopAsync());
        }

        private async static Task MenuLoopAsync()
        {
            bool continueLoop;
            do
            {
                await PrintIndexStateAsync();
                Console.WriteLine();

                await PrintMenuAsync();
                Console.WriteLine();

                continueLoop = await GetMenuChoiceAndExecuteAsync();
                Console.WriteLine();
            }
            while (continueLoop);
        }

        private async static Task<bool> GetMenuChoiceAndExecuteAsync()
        {
            while (true)
            {
                int inputValue = ConsoleUtils.ReadIntegerInput("Enter an option [0-14] and press Enter: ");

                switch (inputValue)
                {
                    case 1: // Create Index
                        Console.Clear();
                        await CreateIndexAsync();
                        return true;
                    case 2: // Add Documents
                        Console.Clear();
                        await AddDocumentsAsync();
                        return true;
                    case 3: // Count Index
                        Console.Clear();
                        await CountIndexAsync();
                        return true;
                    case 4: // Query Index (Simple All)
                        Console.Clear();
                        await QueryAsync();
                        return true;
                    case 5: // Query Index (Simple Any)
                        Console.Clear();
                        await QueryAsync(2);
                        return true;
                    case 6: // Query Index (With Facets)
                        Console.Clear();
                        await QueryAsync(3);
                        return true;
                    case 7: // Update Index
                        Console.Clear();
                        await UpdateIndexAsync();
                        return true;
                    case 8: //Query Index (Use Scoring Profiles)
                        Console.Clear();
                        await QueryAsync(4);
                        return true;
                    case 9: // Update Index
                        Console.Clear();
                        await UpdateIndexGeoScoringProfileAsync();
                        return true;
                    case 10: // Query Index (Use Geo Location Scoring Profile)
                        Console.Clear();
                        await QueryAsync(5);
                        return true;
                    case 11: // index Update (Freshness And Tag Scoring Profile)
                        Console.Clear();
                        await UpdateIndexFreshnessAndScoringProfileAsync();
                        return true;
                    case 12: // Query Index-All (Use Freshness And Tags Scoring Profile)
                        Console.Clear();
                        await QueryAsync(6);
                        return true;
                    case 13: // Document Lookup
                        Console.Clear();
                        await LooUpAsync();
                        return true;
                    case 14: // Delete All
                        Console.Clear();
                        await DeleteIndexAsync();
                        return true;
                    case 0: // Exit
                        return false;
                }
            }
        }

        private static async Task CreateIndexAsync()
        {
            try
            {
                Console.WriteLine("What is the index name? ");
                currentIndexName = Console.ReadLine();

                var eventSearchableFields = SearchableEvent.GetSearchableEventFields();
                var eventIndex = new Index()
                {
                    Name = currentIndexName,
                    Fields = SearchableEvent.GetSearchableEventFields(),
                    Suggesters = new List<Suggester>
                    {
                        new Suggester()
                        {
                            // SearchMode = SuggesterSearchMode.AnalyzingInfixMatching
                            Name = Suggester.SearchMode,
                            SourceFields = new List<string> { "name" }
                        }
                    }
                };
                var result = await _searchClient.Indexes.CreateAsync(eventIndex);

                //if (result.Index != null && result.StatusCode == HttpStatusCode.OK)
                    ConsoleUtils.WriteColour(ConsoleColor.Green, "Index {0} added succesfully.", currentIndexName);

                
            }
            catch (Exception ex)
            {
                ConsoleUtils.WriteColour(ConsoleColor.Red, "{0}", ex.ToString());
            }
        }

        private static async Task PrintIndexStateAsync()
        {
            ConsoleUtils.WriteColour(ConsoleColor.Yellow, "Current indexes state:", null);

            if (_searchClient != null)
            {
                foreach (var index in await _searchClient.Indexes.ListAsync())
                {
                    ConsoleUtils.WriteColour(ConsoleColor.Yellow, "\t{0} ({1})", index.Name, (await _searchClient.Indexes.GetStatisticsAsync(index.Name)).DocumentCount);

                }
            }
            else
            {
                ConsoleUtils.WriteColour(ConsoleColor.Yellow, "\tService doesn't contain any indexes.", null);
            }

        }

        private static async Task PrintMenuAsync()
        {
            ConsoleColor otherMenuItemColour;
            if ((await _searchClient.Indexes.ListAsync()).Count() == 0)
                otherMenuItemColour = DisableColour;
            else
                otherMenuItemColour = EnabledColour;

            ConsoleUtils.WriteColour(EnabledColour, "1 Create index");
            ConsoleUtils.WriteColour(otherMenuItemColour, "2 Add documents");
            ConsoleUtils.WriteColour(otherMenuItemColour, "3 Count index");
            ConsoleUtils.WriteColour(otherMenuItemColour, "4 Query index - All (simple)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "5 Query index - Any (simple)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "6 Query index - All (with facets)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "7 Index update");
            ConsoleUtils.WriteColour(otherMenuItemColour, "8 Query index - All (using scoring profiles)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "9 Index update (geo-location scoring profile)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "10 Query index - All (using geo-locationscoring profile)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "11 Index update (freshness + tag scoring profile)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "12 Query index - All (using freshness + tag scroing profile)");
            ConsoleUtils.WriteColour(otherMenuItemColour, "13 Document lookup");
            ConsoleUtils.WriteColour(otherMenuItemColour, "14 Delete index");
            ConsoleUtils.WriteColour(otherMenuItemColour, "0 Exit");
        }

        private static async Task AddDocumentsAsync()
        {

        }

        private static async Task ValidateIndexClient()
        {
            if (_indexClient == null)
                _indexClient = _searchClient.Indexes.GetClient(currentIndexName);
        }

        private static async Task ValidateCurrentIndexName()
        {
            if (string.IsNullOrWhiteSpace(currentIndexName))
            {
                Console.WriteLine("What is the index name?");
                currentIndexName = Console.ReadLine();
            }
        }

        private static async Task CountIndexAsync()
        {
            try
            {
                ValidateCurrentIndexName();

                var result = await _searchClient.Indexes.GetStatisticsAsync(currentIndexName);
                ConsoleUtils.WriteColour(ConsoleColor.Green, "Index {0} has {1} documents.", currentIndexName, result.DocumentCount);
            }
            catch (Exception ex)
            {
                ConsoleUtils.WriteColour(ConsoleColor.Red, "{0}", ex.ToString());
            }
        }

        private static async Task DeleteIndexAsync()
        {
            try
            {
                ValidateCurrentIndexName();

                var result = await _searchClient.Indexes.DeleteAsync(currentIndexName);
            }
            catch (Exception ex)
            {
                ConsoleUtils.WriteColour(ConsoleColor.Red, "{0}", ex.ToString());
            }
        }

        private async static Task LooUpAsync()
        {

        }

        private static SearchParameters GetSearchParameters(int method = 1)
        {
            SearchParameters returnValue = null;

            return returnValue;
        }

        private async static Task QueryAsync(int method = 1)
        {

        }

        private async static Task UpdateIndexAsync()
        {

        }

        private async static Task UpdateIndexFreshnessAndScoringProfileAsync()
        {

        }

        private async static Task UpdateIndexGeoScoringProfileAsync()
        {

        }


    }
}
