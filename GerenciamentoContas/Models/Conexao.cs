using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace GerenciamentoContas.Models
{
    public class Conexao : IDisposable
    {
        private readonly MySqlConnection conexao;
        public Conexao()
        {
            conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            conexao.Open();
        }

        public void Dispose()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
        }

        public static string msg;

        public MySqlConnection MyConectarBD() //Método: MyConectarBD()
        {

            try
            {
                conexao.Open();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return conexao;
        }

        public void ExecutaComando(string StrQuery)
        {
            var vComando = new MySqlCommand
            {
                CommandText = StrQuery,
                CommandType = CommandType.Text,
                Connection = conexao
            };

            vComando.ExecuteNonQuery();
        }

        public MySqlDataReader RetornaComando(string StrQuery)
        {
            var comando = new MySqlCommand(StrQuery, conexao);
            return comando.ExecuteReader();

        }
        public MySqlConnection MyDesConectarBD()  //Método: MyDesConectarBD()
        {

            try
            {
                conexao.Close();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return conexao;
        }
    }
}