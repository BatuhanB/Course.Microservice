using Course.Discount.Service.Api.Models.Common;
using Course.Discount.Service.Api.Service.Contracts;
using Course.Shared.Dtos;
using Dapper;
using Npgsql;
using System.Data;

namespace Course.Discount.Service.Api.Service.Concretes;
public class DiscountService : IDiscountService
{
    private readonly IDbConnection _connection;
    private readonly IConfiguration _configuration;

    public DiscountService(IDbConnection connection, IConfiguration configuration)
    {
        _connection = connection;
        _configuration = configuration;
        _connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    public async Task<Response<NoContent>> CreateAsync(Models.Discount discount)
    {
        var status = await _connection.ExecuteAsync(Queries.CreateDiscount, discount);

        return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount could not created.",500);
    }

    public async Task<Response<NoContent>> DeleteAsync(int id)
    {
        var status = await _connection.ExecuteAsync(Queries.DeleteDiscount, new
        {
            @id = id
        });

        return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("No Discount Has Been Found!", 404);
    }

    public async Task<Response<List<Models.Discount>>> GetAllAsync()
    {
        var discounts = await _connection.QueryAsync<Models.Discount>(Queries.GetAllDiscount);
        return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
    }

    public async Task<Response<Models.Discount>> GetByCodeAndUserIdAsync(string code, string userId)
    {
        var discount = await _connection.QueryFirstOrDefaultAsync<Models.Discount>(Queries.GetDiscountByCodeAndUserId, new
        {
            @code = code,
            @userId = userId
        });

        if (discount == null) return Response<Models.Discount>.Fail("No Discount Has Been Found!", 404);

        return Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<Models.Discount>> GetByIdAsync(int id)
    {
        var discount = await _connection.QueryFirstAsync<Models.Discount>(Queries.GetDiscountById,new {
            @id = id
        });

        if(discount == null) return Response<Models.Discount>.Fail("No Discount Has Been Found!", 404);

        return Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<NoContent>> UpdateAsync(Models.Discount discount)
    {
        var status = await _connection.ExecuteAsync(Queries.UpdateDiscount,new
        {
            @userid = discount.UserId,
            @code = discount.Code,
            @rate = discount.Rate,
            @id = discount.Id
        });

        return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount could not updated.", 500);
    }
}