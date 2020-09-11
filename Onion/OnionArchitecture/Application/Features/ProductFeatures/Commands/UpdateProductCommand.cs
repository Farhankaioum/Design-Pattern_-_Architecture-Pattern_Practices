﻿using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
               _context = context;
            }

            public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product =  await _context.Products.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.Name = request.Name;
                    product.Description = request.Description;
                    product.Rate = request.Rate;

                    await _context.SaveChanges();
                    return product.Id;
                }
            }
        }
    }
}
