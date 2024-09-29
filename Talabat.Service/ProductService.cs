﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications.Product_Specifications;

namespace Talabat.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productSpec);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            return products;
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productId);

            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);

            return product;
        }
        public async Task<int> GetCountAsync(ProductSpecParams productSpec)
        {

            var countSpec = new ProductWithFilterationForCountSpecifications(productSpec);

            var count = await _unitOfWork.Repository<Product>().GetCountAsync(countSpec);

            return count;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
            => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
            => await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

    }
}
