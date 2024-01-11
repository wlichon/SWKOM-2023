using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;
using NPaperless.Models;
using NPaperless.Services.Models;
using TaskAlias = System.Threading.Tasks.Task;



namespace PaperlessTests
{
    [Trait("Category", "IntegrationTest")]
    public class DocumentsControllerIntegrationTests
    {

        private readonly HttpClient _httpClient;
        private readonly ITestOutputHelper _outputHelper;

        public DocumentsControllerIntegrationTests(ITestOutputHelper output)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8081/") };       // initialize HttpClient with the base address of your Paperless REST server
            _outputHelper = output;
        }
         

        [Fact]
        public async TaskAlias SubmitNewDocumentIntegrationTest()
        {
            string filePath = "TestFiles/testFile.pdf";

            if (!File.Exists(filePath))
            {
                _outputHelper.WriteLine($"File not found: {filePath}");
            }

            byte[] fileBytes = File.ReadAllBytes(filePath);     //read content of file

            var fileContent = new ByteArrayContent(fileBytes);  //creates HTTP content object to represent data


            var formContent = new MultipartFormDataContent 
            {
                { fileContent, "document", Path.GetFileName(filePath) }
            };

            var response = await _httpClient.PostAsync("api/documents/post_document", formContent);     //send HTTP POST request to the endpoint responsible for uploading documents

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);  //assert that the request was successful (status code 200 OK)
            await TaskAlias.Delay(10000);

            var responseContent = await response.Content.ReadAsStringAsync();   //read response content as a string

            _outputHelper.WriteLine($"response is: {responseContent}");

            ///////////////////////////////////////////////////////////////
            // now search for the file

            string searchTerm = "test"; //use search term that matches the new document
            var searchResponse = await _httpClient.GetAsync($"api/documents/search?searchTerm={searchTerm}");

            if (!searchResponse.IsSuccessStatusCode)
            {
                Assert.True(false, $"Search request failed with status code: {searchResponse.StatusCode}");
            }

            var searchResults = await searchResponse.Content.ReadAsStringAsync();
            var listDocs = JsonConvert.DeserializeObject<ListResponse<DocumentDto>>(searchResults);

            Assert.NotNull(listDocs);
            Assert.True(listDocs.Count > 0);

            _outputHelper.WriteLine($"Search results are:  {searchResults}");

            ///////////////////////////////////////////////////////////////
            //search db to check if the document was uploaded successfully

            var createdDocument = JsonConvert.DeserializeObject<DocumentDto>(responseContent);
            uint documentId = createdDocument.id;       //now get the document ID from the uploaded document

            var getDocumentResponse = await _httpClient.GetAsync($"api/documents/{documentId}");

            Assert.True(getDocumentResponse.IsSuccessStatusCode, $"Get document request failed with status code: {getDocumentResponse.StatusCode}");

            var documentDetails = await getDocumentResponse.Content.ReadAsStringAsync();
            var retrievedDocument = JsonConvert.DeserializeObject<DocumentDto>(documentDetails);

            Assert.NotNull(retrievedDocument);
            Assert.Equal(documentId, retrievedDocument.id);
            Assert.Equal(createdDocument.title, retrievedDocument.title);

            _outputHelper.WriteLine($"Retrieved document details from the database: {documentDetails}");

        }

        [Fact]
        public async TaskAlias SubmitAnotherDocumentIntegrationTest()
        {
            string filePath = "TestFiles/swkomData.pdf";

            if (!File.Exists(filePath))
            {
                _outputHelper.WriteLine($"File not found: {filePath}");
            }

            byte[] fileBytes = File.ReadAllBytes(filePath);     //read content of file

            var fileContent = new ByteArrayContent(fileBytes);  //creates HTTP content object to represent data


            var formContent = new MultipartFormDataContent
            {
                { fileContent, "document", Path.GetFileName(filePath) }
            };

            var response = await _httpClient.PostAsync("api/documents/post_document", formContent);     //send HTTP POST request to the endpoint responsible for uploading documents

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);  //assert that the request was successful (status code 200 OK)
            await TaskAlias.Delay(10000);

            var responseContent = await response.Content.ReadAsStringAsync();   //read response content as a string

            _outputHelper.WriteLine($"response is: {responseContent}");

            ///////////////////////////////////////////////////////////////
            // now search for the file

            string searchTerm = "data"; //use search term that matches the new document
            var searchResponse = await _httpClient.GetAsync($"api/documents/search?searchTerm={searchTerm}");

            if (!searchResponse.IsSuccessStatusCode)
            {
                Assert.True(false, $"Search request failed with status code: {searchResponse.StatusCode}");
            }

            var searchResults = await searchResponse.Content.ReadAsStringAsync();
            var listDocs = JsonConvert.DeserializeObject<ListResponse<DocumentDto>>(searchResults);

            Assert.NotNull(listDocs);
            Assert.True(listDocs.Count > 0);

            _outputHelper.WriteLine($"Search results are:  {searchResults}");

            ///////////////////////////////////////////////////////////////
            //search db to check if the document was uploaded successfully

            var createdDocument = JsonConvert.DeserializeObject<DocumentDto>(responseContent);
            uint documentId = createdDocument.id;       //now get the document ID from the uploaded document

            var getDocumentResponse = await _httpClient.GetAsync($"api/documents/{documentId}");

            Assert.True(getDocumentResponse.IsSuccessStatusCode, $"Get document request failed with status code: {getDocumentResponse.StatusCode}");

            var documentDetails = await getDocumentResponse.Content.ReadAsStringAsync();
            var retrievedDocument = JsonConvert.DeserializeObject<DocumentDto>(documentDetails);

            Assert.NotNull(retrievedDocument);
            Assert.Equal(documentId, retrievedDocument.id);
            Assert.Equal(createdDocument.title, retrievedDocument.title);

            _outputHelper.WriteLine($"Retrieved document details from the database: {documentDetails}");
        }


        [Fact]
        public async TaskAlias printSomething(){
            string currentDirectory = Directory.GetCurrentDirectory();
            _outputHelper.WriteLine($"Current Directory: {currentDirectory}");
        }
    }
}