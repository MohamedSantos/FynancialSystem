using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoContas.Models
{
    public class LoginModel
    {
        public string Adm { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}