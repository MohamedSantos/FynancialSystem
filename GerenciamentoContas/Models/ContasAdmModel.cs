using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoContas.Models
{
    public class ContasAdmModel
    {
        [Key]
        public int CodConta { get; set; }

        [Required(ErrorMessage = "Informe o nome da conta")]
        public string NomeConta { get; set; }

        [Required(ErrorMessage = "Informe a data de vencimento da conta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VencConta { get; set; }

        [Required(ErrorMessage = "Informe qual é o tipo da conta")]
        public string TipoConta { get; set; }


        [Required(ErrorMessage = "Informe a descrição da conta")]
        public string DescricaoConta { get; set; }

        [Required(ErrorMessage = "Informe o valor da conta")]
        public decimal ValorConta { get; set; }
        [Required(ErrorMessage = "Informe o valor da multa")]
        public decimal MultaConta { get; set; }

        public int Situacao { get; set; }

        public string ObsConta { get; set; }

        

        private Conexao con;
        public void InsertContasAdm(ContasAdmModel contas)
        {
            var strQuery = "";
            strQuery += "call insertcontaadm(";
            strQuery += string.Format("STR_TO_DATE('{0}', '%d/%m/%Y %H:%i:%s'),'{1}', '{2}', '{3}', replace(replace('{4}','.',''),',','.'), replace(replace('{5}','.',''),',','.'),'{6}',0);",
                contas.VencConta, contas.NomeConta, contas.TipoConta, contas.DescricaoConta, contas.ValorConta, contas.MultaConta, contas.ObsConta
                );

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

        public List<ContasAdmModel> ListaDeConta(MySqlDataReader retorno)
        {
            var usuarios = new List<ContasAdmModel>();
            while (retorno.Read())
            {
                var TempUsuario = new ContasAdmModel()
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

        public List<ContasAdmModel> Listar()
        {
            using (con = new Conexao())
            {
                var strQuery = string.Format("select * from Tb_ContaAdm");
                var retorno = con.RetornaComando(strQuery);
                return ListaDeConta(retorno);
            }
        }

        public ContasAdmModel ListarIdConta(int Id)
        {
            using (con = new Conexao())
            {
                var strQuery = string.Format("select * from Tb_ContaAdm where Cod_Conta = {0};", Id);
                var retorno = con.RetornaComando(strQuery);
                return ListaDeConta(retorno).FirstOrDefault();
            }
        }

        public void Atualizar(ContasAdmModel contas)
        {
            var strQuery = "";
            strQuery += "UPDATE Tb_ContaAdm SET";
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

        public void SituacaoPag(ContasAdmModel id)
        {
            var strQuery = "";
            strQuery += "update Tb_ContaAdm set";
            strQuery += " Situacao = 1";
            strQuery += string.Format(" where Cod_Conta = {0};", id.CodConta);

            using(con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }


        public void Excluir(ContasAdmModel id)
        {
            var strQuery = "";
            strQuery += "DELETE FROM Tb_ContaAdm";
            strQuery += string.Format(" WHERE Cod_Conta = {0};", id.CodConta);

            using (con = new Conexao())
            {
                con.ExecutaComando(strQuery);
            }
        }

    }
}