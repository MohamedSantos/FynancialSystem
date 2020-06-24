using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoContas.Models
{
    public class ContasModel
    {
        [Key]
        public int CodConta { get; set; }

        public int CodClienteConta { get; set; }

        [Required(ErrorMessage = "Informe o nome da conta")]
        public string NomeConta { get; set; }

        [Required(ErrorMessage = "Informe a data de vencimento da conta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VencConta { get; set; }

        [Required(ErrorMessage = "Informe qual é o tipo da conta")]
        public string TipoConta { get; set; }

        
        [Required(ErrorMessage = "Informe a descrição da conta")]
        public string DescricaoConta { get; set; }

        [Required(ErrorMessage ="Informe o valor da conta")]  
        public decimal ValorConta { get; set; }
        [Required(ErrorMessage = "Informe o valor da multa")]
        public decimal MultaConta { get; set; }

        public string ObsConta { get; set; }

        public int Situacao { get; set; }

        public ClienteModel clienteModel { get; set; }

        private Conexao con;

        public void Insert(ContasModel contas)
        {
            var strQuery = "";
            strQuery += "call cadastrarconta(";
            strQuery += string.Format("'{0}',STR_TO_DATE('{1}', '%d/%m/%Y %H:%i:%s'),'{2}', '{3}', '{4}', replace(replace('{5}','.',''),',','.'), replace(replace('{6}','.',''),',','.'),'{7}',0);",
                contas.CodClienteConta, contas.VencConta, contas.NomeConta, contas.TipoConta,  contas.DescricaoConta, contas.ValorConta, contas.MultaConta, contas.ObsConta
                );

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

        public List<ContasModel> ListaDeConta(MySqlDataReader retorno)
        {
            var usuarios = new List<ContasModel>();
            while (retorno.Read())
            {
                var TempUsuario = new ContasModel()
                {
                    CodConta = int.Parse(retorno["Cod_Conta"].ToString()),
                    NomeConta = retorno["Nome_Conta"].ToString(),
                    DescricaoConta = retorno["Desc_Conta"].ToString(),
                    TipoConta = retorno["Tipo_Conta"].ToString(),
                    VencConta = DateTime.Parse(retorno["Venc_Conta"].ToString()),
                    ValorConta = Decimal.Parse(retorno["Valor_Conta"].ToString()),
                    MultaConta = Decimal.Parse(retorno["Multa"].ToString()),
                    ObsConta = retorno["Observação_Conta"].ToString(),
                    Situacao = int.Parse(retorno["Situacao"].ToString())
                };
                usuarios.Add(TempUsuario);
            }
            retorno.Close();
            return usuarios;
        }

        public List<ContasModel> Listar(int Id)
        {
            using (con = new Conexao())
            {
                var strQuery =string.Format("call vercontasdedeter('{0}')",Id);
                var retorno = con.RetornaComando(strQuery);
                return ListaDeConta(retorno);
            }
        }

        public ContasModel ListarId(int Id)
        {
            using (con = new Conexao())
            {
                var strQuery = string.Format("call vercontasdedeter('{0}')", Id);
                var retorno = con.RetornaComando(strQuery);
                return ListaDeConta(retorno).FirstOrDefault();
            }
        }

        public ContasModel ListarIdConta(int Id)
        {
            using (con = new Conexao())
            {
                var strQuery = string.Format("select * from Tb_Conta where Cod_Conta = {0};", Id);
                var retorno = con.RetornaComando(strQuery);
                return ListaDeConta(retorno).FirstOrDefault();
            }
        }

        public void Atualizar(ContasModel contas)
        {
            var strQuery = "";
            strQuery += "UPDATE Tb_Conta SET";
            strQuery += string.Format(" Venc_Conta = STR_TO_DATE('{0}', '%d/%m/%Y %H:%i:%s'),", contas.VencConta);
            strQuery += string.Format(" Nome_Conta = '{0}',", contas.NomeConta);
            strQuery += string.Format(" Tipo_Conta = '{0}',", contas.TipoConta);
            strQuery += string.Format(" Desc_Conta = '{0}',", contas.DescricaoConta);
            strQuery += string.Format(" Valor_Conta = replace(replace('{0}','.',''),',','.'),", contas.ValorConta);
            strQuery += string.Format(" Multa = replace(replace('{0}','.',''),',','.'),", contas.MultaConta);
            strQuery += string.Format(" Observação_Conta = '{0}'", contas.ObsConta);
            strQuery += string.Format(" where Cod_Conta = {0};", contas.CodConta);

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

        public void Excluir(ContasModel id)
        {
            var strQuery = "";
            strQuery += "DELETE FROM Tb_Conta";
            strQuery += string.Format(" WHERE Cod_Conta = {0};", id.CodConta);

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

        public void SituacaoPag(ContasModel id)
        {
            var strQuery = "";
            strQuery += "update Tb_Conta set";
            strQuery += " Situacao = 1";
            strQuery += string.Format(" where Cod_Conta = {0};", id.CodConta);

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

        public void AtualizarQtd()
        {
            var strQuery = "";
            strQuery += "call atualizarcontas();";

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

    }
}