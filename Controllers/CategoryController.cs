using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Shop.Models; // Utilizando os models da app.
using Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Route("categories")]
public class CategoryController : ControllerBase
{

    // Read
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    //public Task Get()
    //public ActionResult Get()
    // Async -> Cria Threads paralelas
    // Isto não impede o travamento na thread principal
    public async Task<ActionResult<List<Category>>> Get(
        [FromServices] DataContext context
    )
    {
        //return "GET";
        //return null; // Se retornássemos null, se fosse com ActionResult:
        //return NotFound();
        var categories = await context.Categories.AsNoTracking().ToListAsync();
        return Ok(categories);
    }

    // Read by id
    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Category>> GetById(
        int id,
        [FromServices] DataContext context
    )
    {
        var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return category;
    }

    // Create
    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Post(
        [FromBody] Category model,
        [FromServices] DataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        // InMemory
        try
        {
            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível cadastrar a categoria " });
        }
    }

    // Update
    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Put(
        int id,
        [FromBody] Category model,
        [FromServices] DataContext context
    )
    {
        // Verifica se o ID informado é o mesmo do modelo
        if (id != model.Id)
            return NotFound(new { message = "Categoria não encontrada " });


        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch
        {
            return BadRequest(new { message = "Este registro já foi atualizado" });
        }


    }

    // Delete
    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Delete(
        int id,
        [FromServices] DataContext context
    )
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound(new { message = "Categoria removida com sucesso!" });

        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(category);
        }
        catch
        {
            return BadRequest(new { message = "Não foi possível remover a categoria" });
        }

    }

}