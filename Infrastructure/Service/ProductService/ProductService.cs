using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Responses;

namespace Infrastructure.Service.ProductService;

public class ProductService(IContext context) : IProductService
{
    public async Task<ApiResponse<List<Product>>> GetAll()
    {
        using var connection = context.Connection();
        var sql = "select * from products";
        var res = await connection.QueryAsync<Product>(sql);
        return new ApiResponse<List<Product>>(res.ToList());
    }

    public async Task<ApiResponse<Product>> GetById(int id)
    {
        using var connection = context.Connection();
        var sql = "select * from products where ProductId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Product>(HttpStatusCode.NotFound, "Product Not Found");
        return new ApiResponse<Product>(res);
    }

    public async Task<ApiResponse<bool>> Add(Product product)
    {
        using var connection = context.Connection();
        var sql = "insert into products(Name,Price,Stock) values(@Name,@Price,@Stock)";
        var res = await connection.ExecuteAsync(sql, product);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Product product)
    {
        using var connection = context.Connection();
        var sql = "update products set Name=@Name,Price=@Price,Stock=@Stock where ProductId=@ProductId";
        var res = await connection.ExecuteAsync(sql, product);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection();
        var sql = "delete from products where ProductId = @Id";
        var res = await context.Connection().ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Product not found");
        return new ApiResponse<bool>(res != 0);
    }
}