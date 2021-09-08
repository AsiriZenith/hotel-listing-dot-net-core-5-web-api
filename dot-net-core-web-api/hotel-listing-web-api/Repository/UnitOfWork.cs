using hotel_listing_web_api.Data;
using hotel_listing_web_api.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotel_listing_web_api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext dbContext;

        private IGenericRepository<Country> _countries;

        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext databaseContext)
        {
            dbContext = databaseContext;
        }

        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(dbContext);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(dbContext);

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
