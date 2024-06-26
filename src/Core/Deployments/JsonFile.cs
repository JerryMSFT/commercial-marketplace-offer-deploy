﻿using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Modm.Extensions;

namespace Modm.Deployments
{
	public abstract class JsonFile<T>
	{
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public abstract string FileName { get; }

        private static readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        protected JsonFile(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        protected string GetFilePath()
        {
            return Path.GetFullPath(Path.Combine(configuration.GetHomeDirectory(), FileName));
        }

        public virtual async Task<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            var path = GetFilePath();

            if (!File.Exists(path))
            {
                logger.LogWarning($"{path} was not found");
                return default; 
            }

            var json = await File.ReadAllTextAsync(path, cancellationToken);
            return JsonSerializer.Deserialize<T>(json, serializerOptions);
        }

        public virtual async Task WriteAsync(T data, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(data, serializerOptions);
            logger.LogInformation($"Writing data to {FileName}");
            await File.WriteAllTextAsync(GetFilePath(), json, cancellationToken);
        }

        public Task DeleteAsync()
        {
            var path = GetFilePath();

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return Task.CompletedTask;
        }

    }
}

