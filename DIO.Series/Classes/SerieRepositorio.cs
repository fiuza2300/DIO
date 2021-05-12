using System.Collections.Generic;
using DIO.Series.Interfaces;
using System;

namespace DIO.Series
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();
        public void Atualiza(int id, Serie serie)
            => listaSerie[id] = serie;
        public void Exclui(int id)
            => listaSerie[id].Exclui();
        public void Insere(Serie serie)
            => listaSerie.Add(serie);
        public List<Serie> Lista()
            => listaSerie;
        public int ProximoId()
            => listaSerie.Count;
        public Serie RetornoPorId(int id)
            => listaSerie[id];
    }
}