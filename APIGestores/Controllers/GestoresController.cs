using APIGestores.Context;
using APIGestores.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGestores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GestoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.gestores_bd.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}", Name = "GetGestor")]
        public ActionResult Get(int id)
        {
            try
            {
                var gestor = _context.gestores_bd.FirstOrDefault(g => g.id == id);
                return Ok(gestor);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Gestores_bd gestor)
        {
            try
            {
                _context.gestores_bd.Add(gestor);
                _context.SaveChanges();
                return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult put(int id, [FromBody] Gestores_bd gestor)
        {
            try
            {
                if (gestor.id == id)
                {
                    _context.Entry(gestor).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try{
                var gestor = _context.gestores_bd.FirstOrDefault(g => g.id == id);
                if(gestor != null){

                    _context.gestores_bd.Remove(gestor);
                    _context.SaveChanges();
                    return Ok(id);
                }else{

                    return BadRequest();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
