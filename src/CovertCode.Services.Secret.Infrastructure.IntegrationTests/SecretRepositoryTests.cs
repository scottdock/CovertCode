using Microsoft.VisualStudio.TestTools.UnitTesting;
using CovertCode.Services.Secret.Infrastructure;
using CovertCode.Services.Secret.Contracts;

namespace CovertCode.Services.Secret.Infrastructure.IntegrationTests
{
    [TestClass]
    public class SecretRepositoryTests
    {
        [TestMethod]
        public void NewSecret_WithValidData_WillPersistToDB()
        {
            var options = new SecretRepositoryOptions()
            {
                DbConnectionString = @"{putConnStrHere}"
            };
            var repo = new Repository.SecretRepository(options);
            var secret = new Contracts.DTOs.SecretDto()
            {
                AccessPhrase = "n7ki5",
                Value = "Hi Jeffrey#1",
                ExpireDate = System.DateTime.UtcNow.AddDays(2)
            };
            var newSecret = repo.Add(secret);
            Assert.IsTrue(newSecret.SecretId > 0);
        }
    }
}
