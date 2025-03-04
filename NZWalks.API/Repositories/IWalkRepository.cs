﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync(string? filtererOn=null, string? filterQuery=null, 
            string? sortBy=null, bool isAscending=true,
            int pageNumber=1, int pageSize=1000);

        Task<Walk?> GetByIdAsync(Guid id);

        Task<Walk?> UpdateByIdAsync(Guid id, Walk walk);

        Task<Walk?> DeleteByIdAsync(Guid id);
    }
}
