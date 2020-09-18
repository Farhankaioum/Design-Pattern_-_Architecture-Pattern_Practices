using Dapper.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infrastructure
{
    public class UnitOfWOrk : IUnitOfWork
    {
        public IProductRepository Products { get; }

        public UnitOfWOrk(IProductRepository productRepository)
        {
            Products = productRepository;
        }
    }
}
