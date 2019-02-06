using System.Collections.Generic;
using ApiUsuarios.Models;

namespace ApiUsuarios.Repositorio
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        IEnumerable<Usuario> GetAll();
        Usuario Find(int id);
        void Remove(int id);
        void Update(Usuario usuario);
    }
}