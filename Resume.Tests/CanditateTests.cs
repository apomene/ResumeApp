using ResumeApp.Model;
using System.ComponentModel.DataAnnotations;

namespace Resume.Tests
{
    public class CandidateTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(null,null, false)]
        [TestCase(null,"Doe", false)]
        [TestCase("John",null, false)]
        [TestCase("John","Doe", true)]
        [TestCase("Jack","Jones", true)]

        public void IsValidCanditate(string? fisrtName, string? lastName, bool isValid)
        {

            var candidate = new Candidate
            {
                FirstName = fisrtName,
                LastName = lastName,
                Email = "some@yahoo.com"
            };

            var validationResults = ValidateModel(candidate);
            Assert.That(validationResults.Count == 0,Is.EqualTo( isValid));
        }

        [Test]
        [TestCase("", false)]
        [TestCase("John", false)]
        [TestCase("John@", false)]
        [TestCase("John@gmail", false)]
        [TestCase("John@gmail.", false)]
        [TestCase("John@gmail.c", false)]
        [TestCase("John@gmail.com", true)]
        [TestCase("John@yahoo.gr", true)]

        public void IsValidEmailTests(string email, bool isValid)
        {
           
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = email
            };

            candidate.Email = email;
            var validationResults = ValidateModel(candidate);
            Assert.That(validationResults.Count == 0, Is.EqualTo(isValid));
        }

        [TestCase("1234567890", true)]
        [TestCase("12345678", false)]
        [TestCase("abcdefghij", false)]
        [TestCase("12345678901", false)]
        [TestCase(null, true)] // Optional field
        public void IsValidMobileField(string? mobile, bool isValid)
        {
            var candidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "valid@example.com",
                Mobile = mobile
            };

            var validationResults = ValidateModel(candidate);
            Assert.That( validationResults.Count == 0,Is.EqualTo(isValid));
        }

        private static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}