﻿using LuizaLabs.Domain.Entities;
using LuizaLabs.Infra.Cross;
using LuizaLabs.Infra.Data.Interfaces;
using LuizaLabs.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LuizaLabs.Domain.Service
{
    public class SrvUser : ISrvUser
    {
        private readonly IRepUser _repUser;
        public SrvUser(IRepUser repUser)
        {
            _repUser = repUser;
        }

        public async Task AddAsync(User user)
        {
            if (user == null)
                throw new ValidationException("Paylod requerido");

            if (string.IsNullOrEmpty(user.Name))
                throw new ValidationException("Nome requerido");

            if (await _repUser.HasName(user.Name))
                throw new AlreadyExistException("Não foi possível cadastrar esse usuário pois ele já está cadastrado");

            await _repUser.Add(user);
        }

        public async Task<List<User>> GetPaginationAsync(int pageSize, int page)
        {
            if (page <= 0 || pageSize < 0)
                throw new ValidationException("Páginação incorreta");

            var list = await _repUser.GetPaginationAsync(pageSize, page);

            if (!list.Any())
                throw new NotContentException();

            return list;
        }


    }
}
