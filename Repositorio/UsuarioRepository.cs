using System.Collections.Generic;
using ApiUsuarios.Models;
using System.Linq;

namespace ApiUsuarios.Repositorio
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MyDbContext _context;
        public UsuarioRepository(MyDbContext context)
        {
            _context = context;
        }
        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario Find(int id) => _context.Usuarios.FirstOrDefault(r => r.UsuarioId == id);

        public IEnumerable<Usuario> GetAll() => _context.Usuarios.ToList();

        public void Remove(int id)
        {
            var entity = _context.Usuarios.First(r => r.UsuarioId == id);
            _context.Usuarios.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
    }
}