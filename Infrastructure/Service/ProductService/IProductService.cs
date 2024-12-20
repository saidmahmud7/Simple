using Domain.Entities;
using Infrastructure.Responses;

namespace Infrastructure.Service.ProductService;

public interface IProductService
{
    Task<ApiResponse<List<Product>>> GetAll();
    Task<ApiResponse<Product>> GetById(int id);
    Task<ApiResponse<bool>> Add(Product product);
    Task<ApiResponse<bool>> Update(Product product);
    Task<ApiResponse<bool>> Delete(int id);
}