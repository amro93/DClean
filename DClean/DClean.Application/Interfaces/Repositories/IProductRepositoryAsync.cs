﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTower.Domain.Entities;

namespace VTower.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
