using System.Linq;

namespace ApiUsuarios.Models
{
    public class InicializaDb
    {
        public static void Inicialize(MyDbContext context){
            context.Database.EnsureCreated();

            if(context.Usuarios.Any()){
                return;
            }

            var usuarios = new Usuario[]{
                new Usuario(){ Nome = "Kevlin", Email = "kevlin@gmail.com", Senha = "123456" },
                new Usuario(){ Nome = "Bianca", Email = "bianca@gmail.com", Senha = "34234" },
                new Usuario(){ Nome = "Suellen", Email = "suellen@gmail.com", Senha = "adfse4" },
                new Usuario(){ Nome = "Jaime", Email = "jaime@gmail.com", Senha = "DFsdf4" }
            };

            foreach (var item in usuarios)
            {
                context.Usuarios.Add(item);
            }

            context.SaveChanges();

        }
    }
}