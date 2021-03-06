﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Northwind.Business.Interfaces.Providers;
using Northwind.Business.Interfaces.Services;
using Northwind.Core.Domain;
using Northwind.Core.Options;
using Northwind.Data.Entities;

namespace Northwind.Business.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsProvider _productsProvider;
        private readonly IMapper _mapper;
        private readonly IOptions<ProductOptions> _productOptions;

        public ProductsService(IProductsProvider productsProvider, IMapper mapper, IOptions<ProductOptions> productOptions)
        {
            _productsProvider = productsProvider;
            _mapper = mapper;
            _productOptions = productOptions;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            IEnumerable<ProductEntity> products;

            var productsAmount = _productOptions.Value?.MaximumAmount;
            if (!productsAmount.HasValue || productsAmount <= 0)
            {
                products = await _productsProvider.GetProductsAsync();
            }
            else
            {
                products = await _productsProvider.GetProductsAsync(productsAmount.Value);
            }

            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return _mapper.Map<Product>(await _productsProvider.GetProductByIdAsync(id));
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var createdProduct = await _productsProvider.AddAsync(_mapper.Map<ProductEntity>(product));

            return _mapper.Map<Product>(createdProduct);
        }

        public Task UpdateProductAsync(Product product)
        {
            return _productsProvider.UpdateAsync(_mapper.Map<ProductEntity>(product));
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productsProvider.GetProductByIdAsync(id);
            if (product != null)
            {
                await _productsProvider.RemoveAsync(product);
            }
        }
    }
}
