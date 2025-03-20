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
            _client.Dispose();
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
    }
}

