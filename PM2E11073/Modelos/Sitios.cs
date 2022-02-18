using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E11073.Modelos
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public int Codigo { get; set; }

        [MaxLength(20)]
        public String Latitud { get; set; }

        [MaxLength(20)]
        public String Longitud { get; set; }

        [MaxLength(150)]
        public String Descripcion { get; set; }

        public byte[] Imagen { get; set; }
    }
}
