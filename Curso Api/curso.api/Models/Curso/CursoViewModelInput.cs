using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Models.Curso
{
    public class CursoViewModelInput
    {
        [Required(ErrorMessage = "Nome do Curso obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Descrição do Curso obrigatório")]
        public string Descricao { get; set; }
    }
}
