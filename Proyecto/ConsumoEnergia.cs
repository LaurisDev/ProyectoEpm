//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConsumoEnergia
    {
        public int id_ConsumoEnergia { get; set; }
        public int Cedula { get; set; }
        public int MetaAhorroEnergia { get; set; }
        public int ConsumoActualEnergia { get; set; }
        public int PeriodoConsumo { get; set; }
    
        public virtual Clientes Clientes { get; set; }
    }
}
