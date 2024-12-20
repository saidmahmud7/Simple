using Domain.Entities;
using Infrastructure.Responses;

namespace Infrastructure.Service.OrderService;

public interface IOrderService
{
    Task<ApiResponse<List<Order>>> GetAll();
    Task<ApiResponse<Order>> GetById(int id);
    Task<ApiResponse<bool>> Add(Order order);
    Task<ApiResponse<bool>> Update(Order order);
    Task<ApiResponse<bool>> Delete(int id);
}