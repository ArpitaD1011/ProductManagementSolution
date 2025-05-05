using Moq;
using NUnit.Framework;
using Products.Application.Interfaces;
using Products.Application.Services;
using Products.Domain.Entities;


namespace Products.Tests.Services
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _repomock;
        private ProductService _service;
        [SetUp]
        public void Setup()
        {
            _repomock = new Mock<IProductRepository>();
            _service = new ProductService(_repomock.Object);
        }
        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product {ProductId ="1", ProductName="Laptop", StockAvailable=5},
                new Product {ProductId ="2", ProductName="Phone", StockAvailable=10},

            };
            _repomock.Setup(r => r.GetAllProductAsync()).ReturnsAsync(products);
            //Act
            var result = await _service.GetAllProductAsync();
            //Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
        [Test]
        public async Task GetProductByIdAsync_ShouldReturnProductWhenExists()
        {
            //Arrange
            var product = new Product { ProductId = "123", ProductName = "Pen" };
            _repomock.Setup(r => r.GetProductById("123")).ReturnsAsync(product);
            //Act
            var result = await _service.GetProductByIdAsync("123");
            //Assert
            Assert.That(result.ProductId, Is.EqualTo("123"));
        }
        [Test]
        public async Task CreateProductAsync_ShouldSetProductIdAndSave()
        {
            //Arrange
            var product = new Product { ProductName = "Notebook", StockAvailable = 4 };
            _repomock.Setup(r => r.GenerateUniqueProductIdAsync())
                .ReturnsAsync("999001");
            _repomock.Setup(r => r.AddProductAsync(It.IsAny<Product>())).ReturnsAsync((Product  p) => p);
            //Act
            var result = await _service.AddProductAsync(product);
            //Assert
            Assert.That(result.ProductId, Is.EqualTo("999001"));
            _repomock.Verify(r => r.AddProductAsync(It.IsAny<Product>()), Times.Once);
        }
        [Test]
        public async Task UpdateProductAsync_ShouldCallRepositoryUpdate()
        {
            //Arrange
            var product = new Product { ProductId = "789", ProductName = "Tablet", ProductDescription="Updated Tablet", StockAvailable=5 };
            var existingProduct = new Product
            {
                ProductId = "789",
                ProductName = "Old Product",
                ProductDescription = "Old Description",
                StockAvailable = 3
            };
            _repomock.Setup(r=>r.GetProductById("789")).ReturnsAsync(existingProduct);
            _repomock.Setup(r=>r.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            //Act
            await _service.UpdateProductAsync("789", product);
            _repomock.Verify(r => r.UpdateProductAsync(It.Is<Product>(
                p=> p.ProductName=="Tablet" &&
                p.ProductDescription =="Updated Tablet" &&
                p.StockAvailable ==5
                
                )),Times.Once);
        }
        [Test]
        public async Task DeleteProductAsync_ShouldDelete_WhenExists()
        {
            //Arrange
            var product = new Product { ProductId = "456", ProductName = "Charger" };
            _repomock.Setup(r => r.GetProductById("456")).ReturnsAsync(product);
            //Act
            await _service.DeleteProductAsync("456");
            _repomock.Verify(r => r.DeleteProductAsync(product), Times.Once);
        }
    }
 }
