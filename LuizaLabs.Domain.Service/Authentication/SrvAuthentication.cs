﻿using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross.Config;

namespace LuizaLabs.Domain.Service
{
    public class SrvAuthentication : ISrvAuthentication
    {
        private readonly Configuration _configuration;

        public SrvAuthentication(Configuration configuration)
        {
            _configuration = configuration;
        }

        public object Authentication(Auth auth)
        {
            auth.Validate(_configuration.GetConfig<string>("AppSettings:Usuario"), _configuration.GetConfig<string>("AppSettings:Senha"));
            return auth.GerarJWT(_configuration.GetConfig<string>("AppSettings:SecretKeyAuth"));

        }

        
    }
}
