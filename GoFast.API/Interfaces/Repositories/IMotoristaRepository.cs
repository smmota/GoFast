﻿using GoFast.API.Models;

namespace GoFast.API.Interfaces.Repositories
{
    public interface IMotoristaRepository : IBaseRepository<Motorista>
    {
        public Motorista GetMotoristaById(Guid id);
    }
}
