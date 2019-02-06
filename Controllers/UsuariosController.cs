using System.Collections.Generic;
using ApiUsuarios.Models;
using ApiUsuarios.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers
{
    [Route("api/[Controller]")]
    [Authorize()]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
            return _usuarioRepository.GetAll();
        }
        [HttpGet("{id}", Name="GetUsuario")]
        public IActionResult GetById(int id)
        {
            var usuario = _usuarioRepository.Find(id);

            if (usuario == null)
                return NotFound();

            return new ObjectResult(usuario);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Usuario usuario)
        {
            if(usuario == null) return BadRequest();
            _usuarioRepository.Add(usuario);
            
            return CreatedAtRoute("GetUsuario", new { id = usuario.UsuarioId}, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Usuario usuario){
            if(usuario == null || usuario.UsuarioId != id) return BadRequest();

            var _usuario = _usuarioRepository.Find(id);
            if(_usuario == null) return NotFound();

            _usuario.Email = usuario.Email;
            _usuario.Nome = usuario.Nome;

            _usuarioRepository.Update(_usuario);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var _usuario = _usuarioRepository.Find(id);
            if(_usuario == null) return NotFound(); 

            _usuarioRepository.Remove(id);
            return new NoContentResult();
        }
    }
}