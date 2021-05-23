using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure.Repositories
{
    public class ProductRepository : DBFactoryBase,IProductRepository
    {
    
        public ProductRepository(IConfiguration configuration):base(configuration)
        {
           
        }


        public async Task<int> AddAsync(Product product)
        {
            product.AddedOn = DateTime.Now;
            var sql= "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";

            return await DbQuerySingleAsync<int>(sql, product);
           
        }
        

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = $@"IF EXISTS (SELECT 1 FROM Products WHERE ID = @ID)
                                        DELETE Products WHERE ID = @ID";
             return await DbExecuteAsync<bool>(sql, new { id});
 
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await DbExecuteScalarAsync("SELECT COUNT(1) FROM Products WHERE ID = @ID", new { id });
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            return await DbQueryAsync<Product>(sql);

        }


        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";

            return await DbQuerySingleAsync<Product>(sql,new { id});
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            product.ModifiedOn = DateTime.Now;

            string sqlQuery = $@"IF EXISTS (SELECT 1 FROM Products WHERE ID = @ID) 
                                            UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn
                                            WHERE ID = @ID";

            return await DbExecuteAsync<bool>(sqlQuery, product);
        }
    }
}
