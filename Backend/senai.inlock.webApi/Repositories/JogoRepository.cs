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
                    string queryUpdateBody = "update jogos set idEstudio = @idEstudio, nomeJogo = @nomeJogo , dataLancamento = @dataLancamento, valor = @valor, descricao = @descricao where idJogo = @idJogo;";

                        using (SqlCommand cmd = new SqlCommand(queryUpdateBody, con))
                        {
                            cmd.Parameters.AddWithValue("@nomeJogo", jogoAtualizado.nomeJogo);
                            cmd.Parameters.AddWithValue("@idJogo", jogoAtualizado.idJogo);
                            cmd.Parameters.AddWithValue("@idEstudio", jogoAtualizado.idEstudio);
                            cmd.Parameters.AddWithValue("@dataLancamento", jogoAtualizado.dataLancamento);
                            cmd.Parameters.AddWithValue("@valor", jogoAtualizado.valor);
                            cmd.Parameters.AddWithValue("@descricao", jogoAtualizado.descricao);

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
                string querySelectById = @"SELECT idJogo, nomeJogo, descricao,dataLancamento, valor,estudios.idEstudio, estudios.nomeEstudio 
                                          FROM jogos INNER JOIN estudios ON jogos.idEstudio = estudios.idEstudio  WHERE idJogo = @idJogo";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idJogo", idJogo);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        JogoDomain jogoBuscado = new JogoDomain
                        {
                            idJogo = Convert.ToInt32(rdr["idJogo"]),

                            nomeJogo = rdr["nomeJogo"].ToString(),

                            descricao = rdr["descricao"].ToString(),
                            
                            dataLancamento = Convert.ToDateTime(rdr["dataLancamento"]),

                            valor = rdr ["valor"].ToString(),

                            idEstudio = Convert.ToInt32(rdr["idEstudio"]),

                            estudio = new EstudioDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr["idEstudio"]),
                                nomeEstudio = rdr["nomeEstudio"].ToString()
                            }
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

                string queryInsert = "INSERT INTO jogos (nomeJogo, descricao, valor, dataLancamento, idEstudio) VALUES (@nomeJogo, @descricao, @valor, @dataLancamento, @idEstudio) ";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeJogo", novoJogo.nomeJogo);
                    cmd.Parameters.AddWithValue("@descricao", novoJogo.descricao);
                    cmd.Parameters.AddWithValue("@valor", novoJogo.valor);
                    cmd.Parameters.AddWithValue("@dataLancamento", novoJogo.dataLancamento);
                    cmd.Parameters.AddWithValue("@idEstudio", novoJogo.idEstudio);

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
                string querySelectAll = @"SELECT idJogo, nomeJogo, descricao,dataLancamento, valor,estudios.idEstudio, estudios.nomeEstudio 
                                          FROM jogos INNER JOIN estudios ON jogos.idEstudio = estudios.idEstudio ";
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

                            nomeJogo = rdr[1].ToString(),

                            descricao = rdr[2].ToString(),

                            dataLancamento = Convert.ToDateTime(rdr[3]),

                            valor = rdr[4].ToString(),

                            idEstudio = Convert.ToInt32(rdr[5]),

                            estudio = new EstudioDomain()
                            {
                                idEstudio = Convert.ToInt32(rdr[5]),
                                nomeEstudio = rdr [6].ToString()
                            }
                        };

                        listaGeneros.Add(jogo);
                    }
                }
            }
            return listaGeneros;
        }
    }
}
