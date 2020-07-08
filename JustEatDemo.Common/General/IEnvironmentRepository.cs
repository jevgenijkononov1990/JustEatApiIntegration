using JustEatDemo.Common.Integrations.JustEat;
using System.Collections.Generic;

namespace JustEatDemo.Common.General
{
    public interface IEnvironmentRepository
    {
        IDictionary<string, string> Environment { get; }
        (string value, bool valueExists) GetValue(string key);
        TObject GetCustomObject<TObject>(TObject obj) where TObject : class;
    }
    public interface IJustEatRepository
    {
        JustEatIntegrationSettings GetSettings();
    }
}
