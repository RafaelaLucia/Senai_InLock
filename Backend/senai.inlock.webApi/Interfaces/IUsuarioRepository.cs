using senai.inlock.webApi_.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Interfaces
{
    interface IUsuarioRepository
    {
        UsuarioDomain BuscarPorEmailSenha(string email, string senha);
        List<UsuarioDomain> ListarTodos();
    }
}
