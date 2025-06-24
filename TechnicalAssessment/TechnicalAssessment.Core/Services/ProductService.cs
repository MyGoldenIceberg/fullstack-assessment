using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalAssessment.Core.Context;
using TechnicalAssessment.Core.DTOs;
using TechnicalAssessment.Core.EntityFramework;
using TechnicalAssessment.Core.EntityFramework.Interfaces;
using TechnicalAssessment.Core.Model;
using TechnicalAssessment.Core.Services.Interfaces;

namespace TechnicalAssessment.Core.Services
{
    public class ProductService : Service<Product, TechAssessmentDbContext>, IProductService
    {
        public ProductService(IUnitOfWork<TechAssessmentDbContext> unitOfWork):base(unitOfWork)
        {
            
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId
            });
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Name))
                throw new ArgumentException("Name is required.");
            if (createDto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            var product = new Product
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                CategoryId = createDto.CategoryId
            };

            Add(product);
            await SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }
    }
}
