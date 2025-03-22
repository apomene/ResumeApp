using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using ResumeApp.Model;
using static System.Net.Mime.MediaTypeNames;


namespace Resume.Tests
{
    public class ControllerTests
    {

        private HttpClient _client;
        private WebApplicationFactory<Program> _application;

        [SetUp]
        public void Setup()
        {
            _application = new WebApplicationFactory<Program>();
            _client = _application.CreateClient();
        }

        [TearDown]
        public void Teardown()
        {
            if (_client !=null)
                _client.Dispose();
            if (_application != null)
                _application.Dispose();
        }

        [Test]
        public async Task CreateCandidate_ValidCandidate_ReturnsCreated()
        {
            var candidate = new
            {
                LastName = "Doe",
                FirstName = "John",
                Email = "johndoe@example.com",
                Mobile = "1234567890"
            };

            var content = new StringContent(JsonSerializer.Serialize(candidate), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/candidates", content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task CreateCandidate_InvalidEmail_ReturnsBadRequest()
        {
            var candidate = new
            {
                LastName = "Doe",
                FirstName = "John",
                Email = "invalid-email",
                Mobile = "1234567890"
            };

            var content = new StringContent(JsonSerializer.Serialize(candidate), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/candidates", content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task GetCandidates_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/candidates");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetCandidate_NotFound_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/candidates/999");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task DeleteCandidate_NotFound_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("/api/candidates/999");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task EditCandidate_ValidUpdate_ReturnsOk()
        {
            // Create an initial candidate to update
            var candidate = new
            {
                LastName = "Doe",
                FirstName = "John",
                Email = "some@yahoo.com",
                Mobile = "1234567890"
            };

            // Updated candidate details
            var updatedCandidate = new
            {
                FirstName = "Jane",
                LastName = "Ones",
                Email = "janedoe@example.com",
                Mobile = "9876543210",
                DegreeId = 1
            };

            var content = new StringContent(JsonSerializer.Serialize(candidate), Encoding.UTF8, "application/json");

            // Create the candidate first (this will assign an ID)
            var createResponse = await _client.PostAsync("/api/candidates", content);

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));  // Ensure it was created

            var updateContent = new StringContent(JsonSerializer.Serialize(updatedCandidate), Encoding.UTF8, "application/json");

            // Now update the candidate with ID 1 (make sure the candidate exists in DB)
            var response = await _client.PutAsync("/api/candidates/1", updateContent);

            // Assert that the response is OK
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Read the response body
            var responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the response to the Candidate object
            var candidateFromResponse = JsonSerializer.Deserialize<Candidate>(responseBody);

            // Assert that the returned candidate matches the updated data
            Assert.That(candidateFromResponse.FirstName, Is.EqualTo("Jane"));
            Assert.That(candidateFromResponse.LastName, Is.EqualTo("Ones"));
            Assert.That(candidateFromResponse.Email, Is.EqualTo("janedoe@example.com"));
            Assert.That(candidateFromResponse.Mobile, Is.EqualTo("9876543210"));
        }

        [Test]
        public async Task EditCandidate_InvalidId_ReturnsNotFound()
        {
            var updatedCandidate = new
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "janedoe@example.com",
                Mobile = "9876543210",
                DegreeId = 1
            };

            var content = new StringContent(JsonSerializer.Serialize(updatedCandidate), Encoding.UTF8, "application/json");

            // Assuming ID 999 doesn't exist
            var response = await _client.PutAsync("/api/candidates/999", content);

            // Assert that the response is Not Found (Status 404)
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


        [Test]
        public async Task CreateDegree_ValidDegree_ReturnsCreated()
        {
            var degree = new { Name = "BSc Computer Science" };
            var content = new StringContent(JsonSerializer.Serialize(degree), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/degrees", content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task CreateDegree_EmptyName_ReturnsBadRequest()
        {
            var degree = new { Name = "" };
            var content = new StringContent(JsonSerializer.Serialize(degree), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/degrees", content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task GetDegrees_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/degrees");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetDegree_NotFound_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/degrees/999");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task DeleteDegree_NotFound_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("/api/degrees/999");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task EditDegree_ValidUpdate_ReturnsOk()
        {
            // Create an initial degree to update
            var degree = new
            {
                Name = "Phd"

            };
            // Updated degree details
            var updatedDegree = new
            {
                Name = "Masters"
            };

            var content = new StringContent(JsonSerializer.Serialize(degree), Encoding.UTF8, "application/json");

            var createResponse = await _client.PostAsync("/api/degrees", content);

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));  // Ensure it was created

            var updateContent = new StringContent(JsonSerializer.Serialize(updatedDegree), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("/api/degrees/1", updateContent);

            // Assert that the response is OK
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Read the response body
            var responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the response to the Candidate object
            var degreeFromResponse = JsonSerializer.Deserialize<Degree>(responseBody);

            // Assert that the returned candidate matches the updated data
            Assert.That(degreeFromResponse.Name, Is.EqualTo("Masters"));
        }

        [Test]
        public async Task EditDegree_InvalidId_ReturnsNotFound()
        {
            var updateDegree = new
            {
                Name = "Phd"
               
            };

            var content = new StringContent(JsonSerializer.Serialize(updateDegree), Encoding.UTF8, "application/json");

            // Assuming ID 999 doesn't exist
            var response = await _client.PutAsync("/api/degrees/999", content);

            // Assert that the response is Not Found (Status 404)
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


    }
}

