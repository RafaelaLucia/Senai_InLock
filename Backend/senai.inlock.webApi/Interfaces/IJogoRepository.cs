using senai.inlock.webApi_.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Interfaces
{
    interface IJogoRepository
    {
        List<JogoDomain> ListarTodos();
        JogoDomain BuscarPorId(int idJogo);
        void Cadastrar(JogoDomain novoJogo);
        void AtualizarIdCorpo(JogoDomain jogoAtualizado);
        void AtualizarIdUrl(int idJogo, JogoDomain jogoAtualizado);
        void Deletar(int idJogo);
    }
}
