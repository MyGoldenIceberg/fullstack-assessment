using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using TechnicalAssessment.Core.Context;
using TechnicalAssessment.Core.DTOs;
using TechnicalAssessment.Core.EntityFramework.Interfaces;
using TechnicalAssessment.Core.Model;
using TechnicalAssessment.Core.Services;
using Xunit;

namespace TechnicalAssessment.Test
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork<TechAssessmentDbContext>> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<Product>> _repositoryMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork<TechAssessmentDbContext>>();
            _repositoryMock = new Mock<IGenericRepository<Product>>();
            _unitOfWorkMock.Setup(u => u.Repository<Product>()).Returns(_repositoryMock.Object);

            _service = new ProductService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsMappedDtos()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Test", Description = "Desc", Price = 10, CategoryId = Guid.NewGuid() }
            };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _service.GetAllProductsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal(products[0].Name, result.First().Name);
        }

        [Fact]
        public async Task CreateProductAsync_ValidInput_ReturnsProductDto()
        {
            // Arrange
            var createDto = new CreateProductDto
            {
                Name = "New Product",
                Description = "Desc",
                Price = 20,
                CategoryId = Guid.NewGuid()
            };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.CreateProductAsync(createDto);

            // Assert
            Assert.Equal(createDto.Name, result.Name);
            Assert.Equal(createDto.Price, result.Price);
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData("", 10)]
        [InlineData("Valid", 0)]
        [InlineData("Valid", -5)]
        public async Task CreateProductAsync_InvalidInput_ThrowsArgumentException(string name, decimal price)
        {
            // Arrange
            var createDto = new CreateProductDto
            {
                Name = name,
                Description = "Desc",
                Price = price,
                CategoryId = Guid.NewGuid()
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProductAsync(createDto));
        }

        [Fact]
        public async Task CreateProductAsync_NonPositivePrice_ThrowsArgumentException()
        {
            // Arrange
            var createDto = new CreateProductDto
            {
                Name = "Valid",
                Description = "Desc",
                Price = 0,
                CategoryId = Guid.NewGuid()
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProductAsync(createDto));
            Assert.Contains("Price must be greater than zero", ex.Message);
        }
    }

}
