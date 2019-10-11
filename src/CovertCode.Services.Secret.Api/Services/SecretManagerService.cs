using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using CovertCode.Services.Secret.Core;
using CovertCode.Services.Secret.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace CovertCode.Services.Secret.Api
{
    public class SecretManagerService : SecretManager.SecretManagerBase
    {
        private readonly ILogger<SecretManagerService> _logger;
        private readonly IConfiguration _config;

        public SecretManagerService(ILogger<SecretManagerService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public override Task<GetByAccessPhraseResponse> GetByAccessPhrase(GetByAccessPhraseRequest request, ServerCallContext context)
        {
            var options = new CovertCode.Services.Secret.Infrastructure.SecretRepositoryOptions()
            {
                DbConnectionString = _config["AppSettings:DbConnectionString"].ToString()
            };

            var core = new Core.SecretManager(options);
            var secret = core.GetByAccessPhrase(request.AccessPhrase);

            return Task.FromResult(new GetByAccessPhraseResponse
            {
                WasSuccessful = (secret.SecretId > 0),
                Value = secret.Value
            });
        }

        public override Task<AddResponse> Add(AddRequest request, ServerCallContext context)
        {
            var options = new CovertCode.Services.Secret.Infrastructure.SecretRepositoryOptions()
            {
                DbConnectionString = _config["AppSettings:DbConnectionString"].ToString()
            };

            var core = new Core.SecretManager(options);
            var secret = new Contracts.DTOs.SecretDto()
            {
                Value = request.Value,
                AccessPhrase = Guid.NewGuid().ToString("N").Substring(0, 8),
                ExpireDate = DateTime.UtcNow.AddSeconds(request.Ttl),
                PassPhrase = string.IsNullOrEmpty(request.PassPhrase) ? string.Empty : request.PassPhrase
            };

            var savedSecret = core.Add(secret);

            return Task.FromResult(new AddResponse
            {
                WasSuccessful = (savedSecret.SecretId > 0),
                SecretID = (savedSecret.SecretId > 0) ? savedSecret.SecretId : -1,
                AccessPhrase = savedSecret.AccessPhrase
            });
        }

    }


}
