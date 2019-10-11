using System;
using CovertCode.Services.Secret.Contracts;
using CovertCode.Services.Secret.Contracts.DTOs;
using CovertCode.Services.Secret.Infrastructure;

namespace CovertCode.Services.Secret.Core
{
    public class SecretManager
    {
        private static SecretRepositoryOptions _options;

        public SecretManager(SecretRepositoryOptions options)
        {
            _options = options;
        }

        public SecretDto Add(SecretDto secret)
        {
            var cmd = new Infrastructure.Repository.SecretRepository(_options);
            var newSecret = cmd.Add(secret);
            return newSecret;
        }

        public SecretDto GetByAccessPhrase(string accessPhrase)
        {
            var cmd = new Infrastructure.Repository.SecretRepository(_options);
            var secret = cmd.FindByAccessPhrase(accessPhrase);
            if (secret.SecretId > 0)
            {
                cmd.MarkAsRead(secret);
            }
            return secret;
        }

    }
}
