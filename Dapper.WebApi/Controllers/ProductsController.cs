using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ProductsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _uow.Products.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _uow.Products.GetByIdAsync(id);

            if (data == null)
            {
                return BadRequest();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var data = await _uow.Products.AddAsync(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _uow.Products.DeleteAsync(id);
            return Ok(data);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,Product product)
        {
                product.Id = id;
                var data = await _uow.Products.UpdateAsync(product);
                return Ok(data);
        }


    }
}
