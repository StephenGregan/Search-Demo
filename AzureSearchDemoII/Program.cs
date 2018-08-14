using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearchDemoII
{
    class Program
    {
        private static SearchServiceClient _searchClient;
        private static SearchIndexClient _indexClient;
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

        private async static MenuLoopAsync()
        {
            bool continueLoop;
            do
            {

            }
            while (continueLoop);
        }

        private async static Task<bool> GetMenuChoiceAndExecute()
        {

        }

        private static async Task CreateIndexAsync()
        {

        }

        private static async Task PrintIndexStateAsync()
        {


        }

        private static async Task PrintMenuAsync()
        {

        }

        private static async Task AddDocumentsAsync()
        {

        }

        private static async Task ValidateIndexClient()
        {

        }

        private static async Task ValidateCurrentindexName()
        {

        }

        private static async Task CountIndexAsync()
        {

        }

        private static async Task DeleteIndexAsync()
        {

        }

        private async static Task LooUpAsync()
        {

        }

        private static SearchParameters GetSearchParameters()
        {

        }

        private async static Task QueryAsync()
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
