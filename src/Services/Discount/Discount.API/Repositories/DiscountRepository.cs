using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("Insert into coupon(productname, description, amount) values (@productname, @description, @amount)",
                 new { productname = coupon.ProductName, description = coupon.Description, amount = coupon.Amount });

            return (affected != 0);

        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("Delete from coupon where productName = @productName", new { productName });

            return (affected != 0);
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("select * from coupon where productName = @productName", new { productName });

            if (coupon == null)
                return new Coupon { ProductName = "No discount", Amount = 0, Description = "No dsicount desc" };

            return coupon;

        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("Update coupon set productname = @productname, description = @description, amount = @amount where id = @id",
                 new { id=coupon.Id, productname = coupon.ProductName, description = coupon.Description, amount = coupon.Amount });

            return (affected != 0);
        }
    }
}
