using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Responses;

namespace Infrastructure.Service.OrderService;

public class OrderService(IContext context) : IOrderService
{
    public async Task<ApiResponse<List<Order>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from orders";
        var res = await connection.QueryAsync<Order>(sql);
        return new ApiResponse<List<Order>>(res.ToList());
    }

    public async Task<ApiResponse<Order>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from orders where Orderid = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Order>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Order>(HttpStatusCode.NotFound, "Order Not Found");
        return new ApiResponse<Order>(res);
    }

    public async Task<ApiResponse<bool>> Add(Order order)
    {
        using var connection = context.Connection();
        var sql = "insert into orders(ProductId,Quantity,TotalPrice) values(@ProductId,@Quantity,@TotalPrice)";
        var res = await connection.ExecuteAsync(sql, order);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Order order)
    {
        using var connection = context.Connection();
        var sql =
            "update orders set ProductId=@ProductId,Quantity=@Quantity,TotalPrice=@TotalPrice where orderid=@OrderId";
        var res = await connection.ExecuteAsync(sql, order);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from orders where OrderId = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Order not found");
        return new ApiResponse<bool>(res != 0);
    }
}