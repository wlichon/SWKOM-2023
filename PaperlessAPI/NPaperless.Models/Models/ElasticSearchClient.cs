using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
//using Elastic.Clients.Elasticsearch;
using Elasticsearch.Net;
using Nest;

using NPaperless.Services.Models;

namespace NPaperless.Models.Models
{
    public class ElasticSearchClient
    {
        private readonly ElasticClient _elasticClient;
        private readonly string _uriString = "http://elasticsearch:9200";
        private readonly string _defaultIndexName = "swkom2023-documents";

        public ElasticSearchClient()
        {
            Uri uri = new Uri(_uriString);

            ConnectionSettings? settings = new ConnectionSettings(uri)
                .DefaultIndex(_defaultIndexName);

            _elasticClient = new ElasticClient(settings);
        }

        /*public async Task<List<T>> SearchAsync<T>(Func<QueryContainerDescriptor<T>, QueryContainer> query, string indexName) where T : class
        {
            var searchResponse = await _elasticClient.SearchAsync<T>(s => s
                .Index(indexName)
                .Query(query));

            if (searchResponse.IsValid) return searchResponse.Documents.ToList();
            Console.WriteLine($"Search failed: {searchResponse.DebugInformation}");
            return new List<T>();

        }*/

        public async Task<List<Document>> SearchTitleAndContentAsync(string searchTerm)
        {
            Console.WriteLine($"Searching for term: {searchTerm}");
            var indexName = "swkom2023-documents";

            var searchResponse = await _elasticClient.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .MatchAll()
                )
                .Size(100) // Set the size to the desired number of documents to retrieve
            );

            if (searchResponse.IsValid)
            {
                Console.WriteLine($"Total documents found: {searchResponse.Total}");

                List<Document> matchingDocuments = new List<Document>();

                foreach (var document in searchResponse.Documents)
                {
                    var title = document.title;
                    var content = document.content;

                    Console.WriteLine($"Document - Title: {title}, Content: {content}");

                    if (!string.IsNullOrEmpty(title) && title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    !string.IsNullOrEmpty(content) && content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        // Perform actions based on keyword match
                        Console.WriteLine($"Document with keyword found - Title: {title}, Content: {content}");

                        // Add the matching document to the list
                        matchingDocuments.Add(document);
                    }
                }

                // Return the list of matching documents
                return matchingDocuments;
            }
            else
            {
                // Handle the case where the search response is not valid
                Console.WriteLine($"Elasticsearch search failed: {searchResponse.DebugInformation}");

                // Return an empty list in case of failure
                return new List<Document>();
            }
        }



    }
}