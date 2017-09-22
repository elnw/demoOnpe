using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ONPE.WEB.Models;

namespace ONPE.WEB.ViewModel
{
    public class DashboardViewModel
    {
        public int NroCandidato { get; set; }
        public int NroPartidoPolitico { get; set; }
        public int NroDistrito { get; set; }

        public DashboardViewModel()
        {
            ONPEEntities context = new ONPEEntities();
            NroCandidato = context.Candidato.Count();
            NroPartidoPolitico = context.PartidoPolitico.Count();
            NroDistrito = context.Distrito.Count();
        }
    }
}