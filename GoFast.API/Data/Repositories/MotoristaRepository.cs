﻿using AutoMapper;
using GoFast.API.Interfaces.Repositories;
using GoFast.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GoFast.API.Data.Repositories
{
    internal class MotoristaRepository : BaseRepository<Motorista>, IMotoristaRepository
    {
        private readonly SqlContext _sqlContext;

        public MotoristaRepository(SqlContext sqlContext) : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<Motorista> GetMotoristaById(Guid id)
        {
            return await _sqlContext.Motorista.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
