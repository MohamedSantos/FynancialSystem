using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoContas.Models
{
    public class ClienteModel
    {
        [Key]
        public int CodCliente { get; set; }

        public int FkCliente { get; set; }

        [Required(ErrorMessage = "Informe o nome do cliente")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome do cliente")]
        public string SobrenomeCliente { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CpfCliente { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NascCliente { get; set; }

        [Required(ErrorMessage = "Informe um telefone")]
        public string TelCliente { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Digite um e-mail válido")]
        public string EmailCliente { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        public string EndCliente { get; set; }
        public string CepEnd { get; set; }

        [Required(ErrorMessage = "Informe o número do endereço")]
        public string NumEnd { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        public string BairroEnd { get; set; }

        public string CityEnd { get; set; }

        public string EstadoEnd { get; set; }

        public string QtdContas { get; set; }

        public int CodigoConta { get; set; }
        public decimal ValorConta { get; set; }
        public decimal MultaConta { get; set; }
        public int SituacaoConta { get; set; }
        public string NomeConta { get; set; }

        

        private Conexao con;

        public void Insert(ClienteModel cliente)
        {
            var strQuery = "";
            strQuery += "call insertcli";
            strQuery += string.Format("('{0}', '{1}', STR_TO_DATE('{2}', '%d/%m/%Y %H:%i:%s'), '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}','{10}','{11}');",
                cliente.NomeCliente,cliente.SobrenomeCliente,cliente.NascCliente,cliente.CpfCliente,cliente.EmailCliente,cliente.TelCliente,cliente.EndCliente,cliente.CepEnd,cliente.NumEnd,
                cliente.BairroEnd,cliente.CityEnd,cliente.EstadoEnd
                );

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

        public List<ClienteModel> ListaDeCliente(MySqlDataReader retorno)
        {
            var usuarios = new List<ClienteModel>();
            while (retorno.Read())
            {
                var TempUsuario = new ClienteModel()
                {
                    CodCliente = int.Parse(retorno["Cod_Cliente"].ToString()),
                    NomeCliente = retorno["Nome_Cliente"].ToString(),
                    SobrenomeCliente = retorno["Sobrenome_Cliente"].ToString(),
                    CpfCliente = retorno["CPF_Cliente"].ToString(),
                    NascCliente = DateTime.Parse(retorno["Nasc_Cliente"].ToString()),
                    TelCliente = retorno["Tel_Cliente"].ToString(),
                    EmailCliente = retorno["Email_Cliente"].ToString(),
                    EndCliente = retorno["End_Cliente"].ToString(),
                    CepEnd = retorno["Cep"].ToString(),
                    NumEnd = retorno["NumEnd"].ToString(),
                    BairroEnd = retorno["Bairro"].ToString(),
                    CityEnd = retorno["Cidade"].ToString(),
                    EstadoEnd = retorno["Estado"].ToString(),
                    QtdContas = retorno["Quantidade_Contas"].ToString()
                };
                usuarios.Add(TempUsuario);
            }
            retorno.Close();
            return usuarios;
        }

        public List<ClienteModel> ListaDeClienteFk(MySqlDataReader retorno)
        {
            var usuarios = new List<ClienteModel>();
            while (retorno.Read())
            {
                var TempUsuario = new ClienteModel()
                {   
                    
                    FkCliente = int.Parse(retorno["Fk_Cliente"].ToString()),
                    NomeCliente = retorno["Nome_Cliente"].ToString(),
                    CodigoConta = int.Parse(retorno["Cod_Conta"].ToString()),
                    NomeConta = retorno["Nome_Conta"].ToString(),
                    ValorConta = decimal.Parse(retorno["Valor_Conta"].ToString()),
                    MultaConta = decimal.Parse(retorno["Multa"].ToString()),
                    SituacaoConta = int.Parse(retorno["Situacao"].ToString()),
                    
                };
                usuarios.Add(TempUsuario);
            }
            retorno.Close();
            return usuarios;
        }
        public List<ClienteModel> Listar()
        {
            using (con = new Conexao())
            {
                var strQuery = "select * from Tb_Cliente;";
                var retorno = con.RetornaComando(strQuery);
                return ListaDeCliente(retorno);
            }
        }

        public List<ClienteModel> ListarVencidos()
        {
            using (con = new Conexao())
            {
                var strQuery = string.Format("call tdsquedevem;");
                var retorno = con.RetornaComando(strQuery);
                return ListaDeClienteFk(retorno);
            }
        }


        public ClienteModel ListarId(int Id)
        {
            using (con = new Conexao())
            {
                var strQuery = string.Format("SELECT * FROM Tb_Cliente Where Cod_Cliente = {0};", Id);
                var retorno = con.RetornaComando(strQuery);
                return ListaDeCliente(retorno).FirstOrDefault();
            }
        }

        public void Atualizar(ClienteModel cliente)
        {
            var strQuery = "";
            strQuery += "UPDATE Tb_Cliente SET";
            strQuery += string.Format(" Nome_Cliente = '{0}',", cliente.NomeCliente);
            strQuery += string.Format(" Sobrenome_Cliente = '{0}',", cliente.SobrenomeCliente);
            strQuery += string.Format(" End_Cliente = '{0}',", cliente.EndCliente);
            strQuery += string.Format(" NumEnd = '{0}',", cliente.NumEnd);
            strQuery += string.Format(" Bairro = '{0}',", cliente.BairroEnd);
            strQuery += string.Format(" Cidade = '{0}',", cliente.CityEnd);
            strQuery += string.Format(" Estado = '{0}',", cliente.EstadoEnd);
            strQuery += string.Format(" Tel_Cliente = '{0}',", cliente.TelCliente);
            strQuery += string.Format(" Email_Cliente = '{0}'", cliente.EmailCliente);
            strQuery += string.Format(" WHERE Cpf_Cliente = '{0}';", cliente.CpfCliente);

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

        public void Excluir(ClienteModel id)
        {
            var strQuery = "";
            strQuery += "DELETE FROM Tb_Cliente";
            strQuery += string.Format(" WHERE Cod_Cliente = {0};", id.CodCliente);

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }


    }
}