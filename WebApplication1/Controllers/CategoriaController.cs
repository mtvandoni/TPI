using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public CategoriaController(TPI_DBContext context)
        {
            this.context = context;

        }

        [AllowAnonymous]
        // GET: api/<CategoriaController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Categoria.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}", Name = "getcategoria")]
        public ActionResult Get(int id)
        {
            try {
                var categoria = context.Categoria.FirstOrDefault(p => p.IdCategoria == id);
                return Ok(categoria);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categorium categoria)
        {
            try {
                var categoriaExist = context.Categoria.FirstOrDefault(p => p.IdCategoria == categoria.IdCategoria);
                if (categoriaExist != null) {
                    return BadRequest("categoria existente");
                }
                else {
                    context.Categoria.Add(categoria);
                    context.SaveChanges();
                    return CreatedAtRoute("Getcategoria", new { id = categoria.IdCategoria }, categoria);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categorium categoria)
        {
            try {
                if (categoria.IdCategoria == id) {
                    context.Entry(categoria).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getcategoria", new { id = categoria.IdCategoria }, categoria);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var categoria = context.Categoria.FirstOrDefault(p => p.IdCategoria == id);
                if (categoria != null) {
                    context.Categoria.Remove(categoria);
                    context.SaveChanges();
                    return Ok(id);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

    }
}
