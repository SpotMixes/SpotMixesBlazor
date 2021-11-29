using System.Collections.Generic;

namespace SpotMixesBlazor.Client.ViewModels
{
    public class DniData
    {
        public string numero { get; set; }
        public string nombre_completo { get; set; }
        public string nombres { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public int codigo_verificacion { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string direccion { get; set; }
        public string direccion_completa { get; set; }
        public string ubigeo_reniec { get; set; }
        public string ubigeo_sunat { get; set; }
        public List<string> ubigeo { get; set; }
    }
}