using Microsoft.Extensions.Configuration;

namespace LuizaLabs.Infra.Cross.Config
{
    public class Configuration
    {
        private readonly IConfiguration _configuration;
        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetConfig<T>(string value) where T : class
        {
            string val = _configuration.GetSection(value).Value;

            if (string.IsNullOrEmpty(val))
                throw new ValidationException("Configuração não encontrado");

            return _configuration.GetSection(value).Value as T;
        }

    }
}
