using JustEatDemo.Common.General;
using JustEatDemo.Common.Integrations.JustEat;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JustEatDemo.WebApp.Helpers
{
    public class SettingsRepository : IEnvironmentRepository
    {
        private const string SectionName = "Enviroment";

        private readonly IConfiguration _configuration;

        private IDictionary<string, string> _environment;

        public SettingsRepository(IConfiguration configuration) => _configuration = configuration;

        public IDictionary<string, string> Environment =>
            _environment ?? (_environment = _configuration.GetSection(SectionName).GetChildren().ToDictionary(x => x.Key, x => x.Value));

        public TObject GetCustomObject<TObject>(TObject obj) where TObject : class
        {
            return _configuration.GetSection(SectionName).Get<TObject>();
        }

        public (string value, bool valueExists) GetValue(string key)
        {
            if (!Environment.ContainsKey(key))
            {
                return (null, false);
            }

            return (Environment[key],true);
        }
    }


    public class JustEatSettingsRepository : IJustEatRepository {

        private readonly IOptions<JustEatIntegrationSettings> _settings;

        public JustEatSettingsRepository(IOptions<JustEatIntegrationSettings> settings)
        {
            //defense
            _settings = settings ?? throw new ArgumentNullException("JustEatSettingsRepository init failure due to iotions");
        }

        public JustEatIntegrationSettings GetSettings()
        {       
            return _settings == null  || _settings?.Value == null ? null : _settings.Value;
        }
    }
}
