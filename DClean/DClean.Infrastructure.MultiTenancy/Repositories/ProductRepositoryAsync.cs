using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTower.Application.Interfaces.Repositories;
using VTower.Domain.Entities;
using VTower.Infrastructure.Persistence.Contexts;
using VTower.Infrastructure.Persistence.Repository;

namespace VTower.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(TenantDbContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            //return _products
            //    .AllAsync(p => p.Barcode != barcode);
            return Task.FromResult(false);
        }
    }
}
