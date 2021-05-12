using System;

namespace DIO.Series
{
    public class Serie : EntidadeBase
    {
        #region Atributos
        private Genero Genero { get; set; }
        private string Titulo { get; set; }
        private string Descricao { get; set; }
        private int Ano { get; set; }
        private bool Excluido { get; set; }
        #endregion

        public Serie(int id, Genero genero, string titulo, string descricao, int ano)
        {
            this.Id = id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }

        public override string ToString()
        {
            return $@"Gênero: {this.Genero + Environment.NewLine}
                    Titulo: {this.Titulo + Environment.NewLine}
                    Descrição: {this.Descricao + Environment.NewLine}
                    Ano de Início: {this.Ano + Environment.NewLine}
                    Registro Excluído: {this.Excluido + Environment.NewLine}";
        }

        public string RetornaTitulo() => this.Titulo;
        public int RetornaId() => this.Id;
        public bool RetornaExcluido() => this.Excluido;
        public void Exclui() => this.Excluido = true;

    }

}