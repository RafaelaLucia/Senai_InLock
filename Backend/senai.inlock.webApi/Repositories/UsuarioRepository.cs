using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //COMPUTADOR NAYARA
        private string stringConexao = "DATA SOURCE = LAPTOP-MIHFTFOJ\\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id = sa; pwd = senai@132";
        //COMPUTADOR RAFA
        //private string stringConexao = @"Data Source=LAPTOP-RSG62TB1\SQLEXPRESS; initial catalog=inlock_games_tarde; user id=sa; pwd=Senai@132";
        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "select * from usuarios left join tipoUsuarios on usuarios.idTipoUsuario = tipoUsuarios.idTipoUsuario where email =@email and senha=@senha";
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain
                        {
                            idUsuario = Convert.ToInt32(rdr["idUsuario"]),
                            email = rdr["email"].ToString(),
                            senha = rdr["senha"].ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr["IdTipoUsuario"]),
                            tipoUsuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(rdr["idTipoUsuario"]),
                                titulo = rdr["titulo"].ToString(),
                            }
                        };

                        return usuarioBuscado;
                    }

                    return null;
                }
            }
        }

        public List<UsuarioDomain> ListarTodos()
        {
            List<UsuarioDomain> listaUsuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "select usuarios.idUsuario, usuarios.email, usuarios.senha, tipoUsuarios.idTipoUsuario, tipoUsuarios.titulo from usuarios inner join tipoUsuarios on usuarios.idTipoUsuario = tipoUsuarios.idTipoUsuario";
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            email = (rdr[1]).ToString(),
                            senha = (rdr[2]).ToString(),
                            idTipoUsuario = Convert.ToInt32(rdr[3]),
                            tipoUsuario = new TipoUsuarioDomain()
                            {
                                idTipoUsuario = Convert.ToInt32(rdr[3]),
                                titulo = (rdr[4]).ToString()
                            }
                        };
                        listaUsuarios.Add(usuario);
                    }
                }
            };

            return listaUsuarios;

        }
    }
    
}
