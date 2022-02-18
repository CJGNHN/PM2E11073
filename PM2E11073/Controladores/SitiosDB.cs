using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using PM2E11073.Modelos;

namespace PM2E11073.Controladores
{
    public class SitiosDB
    {
        readonly SQLiteAsyncConnection db;

        // CONSTRUCTOR VACIO
        public SitiosDB()
        {
        }

        // CONSTRUCTOR CON PARAMETROS
        public SitiosDB(String pathbasedatos)
        {
            db = new SQLiteAsyncConnection(pathbasedatos);

            // CREACION DE TABLA
            db.CreateTableAsync<Sitios>();

        }

        // Procedimientos y funciones CRUD

        // Retorna la tabla de personas como una lista
        public Task<List<Sitios>> ListaSitios()
        {

            return db.Table<Sitios>().ToListAsync();


        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        // GUARDAR O ACTUALIZAR SITIO
       public Task<Int32> GuardarSitio(Sitios sitio)
        {
            if (sitio.Codigo != 0)
            {
                return db.UpdateAsync(sitio);
            }
            else
            {
                return db.InsertAsync(sitio);
            }
        }

        // BUSCAR POR ID
        public Task<Sitios> BuscarSitio(int bcodigo)
        {
            return db.Table<Sitios>()
                .Where(i => i.Codigo == bcodigo)
                .FirstOrDefaultAsync();
        }

        

        // ELIMINAR SITIO
        public Task<Int32> EliminarSitio(Sitios sitio)
        {
            return db.DeleteAsync(sitio);
        }

        
    }
}
