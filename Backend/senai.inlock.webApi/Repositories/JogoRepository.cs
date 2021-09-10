using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        //COMPUTADOR NAYARA
        private string stringConexao = "DATA SOURCE = LAPTOP-MIHFTFOJ\\SQLEXPRESS; initial catalog = inlock_games_tarde; user Id = sa; pwd = senai@132";
        //COMPUTADOR RAFA
        //private string stringConexao = @"Data Source=LAPTOP-RSG62TB1\SQLEXPRESS; initial catalog=inlock_games_tarde; user id=sa; pwd=Senai@132";

        public void AtualizarIdCorpo(JogoDomain jogoAtualizado)
        {
            
                if (jogoAtualizado.nomeJogo!= null)
                {
                    using (SqlConnection con = new SqlConnection(stringConexao))
                    {
                        string queryUpdateBody = "UPDATE GENERO SET nomeJogo = @nomeJogo WHERE idJogo = @idJogo";

                        using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                        {
                            cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);
                            cmd.Parameters.AddWithValue("@idJogo", jogoAtualizado.idJogo);

                            con.Open();

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            
        }

        public void AtualizarIdUrl(int idJogo, JogoDomain jogoAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE jogos SET nomeJogo = @nomeJogo WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public JogoDomain BuscarPorId(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT nomeJogo, idJogo FROM jogos WHERE idJogo = @idJogo";

                con.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        JogoDomain jogoBuscado = new JogoDomain
                        {
                            idJogo = Convert.ToInt32(reader["idJogo"]),

                            nomeJogo = reader["nomeJogo"].ToString()
                        };

                        return jogoBuscado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(JogoDomain novoJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            { 

                string queryInsert = "INSERT INTO jogos (nomeJogo) VALUES (@nomeJogo)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idJogo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM jogos WHERE idJogo = @idJogo";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<JogoDomain> ListarTodos()
        {
            List<JogoDomain> listaGeneros = new List<JogoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idJogo, nomeJogo FROM jogos";

                con.Open();
              
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                { 
                    rdr = cmd.ExecuteReader();
                    
                    while (rdr.Read())
                    {
                        JogoDomain jogo = new JogoDomain()
                        {

                            idJogo = Convert.ToInt32(rdr[0]),


                            nomeJogo = rdr[1].ToString()
                        };

                        listaGeneros.Add(jogo);
                    }
                }
            }
            return listaGeneros;
        }
    }
}
