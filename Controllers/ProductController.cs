using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {

        // Rota de: Read - Leitura ->
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get(
            [FromServices] DataContext context
        )
        {
            var products = await context
            .Products
            .Include(x => x.Category)
            .AsNoTracking()
            .ToListAsync();

            return products;
        }

        // Rota de: Read - Leitura - ById
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            var product = await context
                .Products
                .Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        // Rota de: Read - Leitura - Por Categoria
        // Método Categoria
        // Nessa rota teremos:
        // products/categories/1
        // Aqui listaremos todos os 
        // produtos da categoria 1
        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetByCategory(
            int id,
            [FromServices] DataContext context
        )
        {
            var products = await context
                .Products
                .Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.Category.Id == id)
                .ToListAsync();

            return products;
        }

        // Rota de: Post - Cadastro de Produto
        // Create
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Product>> Post(
            [FromServices] DataContext context,
            [FromBody] Product model
        )
        {

            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        // Fim post

    }
}