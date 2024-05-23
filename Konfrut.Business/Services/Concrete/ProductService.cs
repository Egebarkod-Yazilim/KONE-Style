﻿
using Konfrut.Business.Services.Abstract;
using Konfrut.DataAccess.Konfrut.Abstract;
using Konfrut.Entity.Concrete;
using Konfrut.Shared.Utilities.Results.Abstract;
using Konfrut.Shared.Utilities.Results.Concrete;
using PagedList;

namespace Konfrut.Business.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResult<Product>> AddOrUpdateAsync(Product product)
        {
            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return new DataResult<Product>(Shared.Utilities.Results.ComplexTypes.ResultStatus.Success, product);
        }

        public Task<DataResult<Product>> ChangeEntityStatus(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<Product>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync()
        {
            var countofproudct = await _unitOfWork.Product.CountAsync();
            return countofproudct;
        }

        public async Task<IPagedList<Product>> GetPagedListAsync(int pageNumber, int pageSize)
        {
            var products = await _unitOfWork.Product.GetAllPagedAsync(pageNumber, pageSize);
            var pagedlist = products.ToPagedList(pageNumber, pageSize);
            return pagedlist;
        }

        public Task<IResult> HardDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
