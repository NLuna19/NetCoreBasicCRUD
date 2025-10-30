using BECommentsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BECommentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public CommentsController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listComments = await _context.Comment.ToListAsync();
                return Ok(listComments);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var comment = await _context.Comment.FindAsync(id);  
                if (comment == null)
                {
                    return NotFound();
                }
                else return Ok(comment); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CommentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Comment comment)
        {
            try
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();  
                return Ok(comment);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if (id != comment.Id)
            //    return BadRequest(new { message = "El id de la ruta no coincide con el id del comentario." });

            try
            {
                var existing = await _context.Comment.FindAsync(id);
                if (existing == null)
                    return NotFound(new { message = "Comentario no encontrado." });

                // Actualizar solo las propiedades permitidas
                existing.Title = comment.Title;
                existing.Author = comment.Author;
                existing.Text = comment.Text;
                // Mantener fecha de creación tal cual a menos que se quiera permitir su modificación

                await _context.SaveChangesAsync();

                return Ok(new { message = "Comment actualizado con exito!" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Comment.AnyAsync(c => c.Id == id))
                    return NotFound();
                throw;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var comment = await _context.Comment.FindAsync(id);
                if (comment == null) {
                    return NotFound();
                }

                _context.Comment.Remove(comment);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Comment eliminado con exito!"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
