using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VentasProyect.Models;
using VentasProyect.Models.Persona;

namespace VentasProyect.Repository
{
    public class PersonaRepository
    {
        private readonly VENTAS_DBEntities1 _dbContext;

        public PersonaRepository()
        {
            _dbContext = new VENTAS_DBEntities1();
        }
        public IEnumerable<Models.Persona.Persona> GetData(string type)
        {
            string diftype = (type == "Cliente")? "Proveedor": "Cliente";

            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_persona.Where(xh => xh.per_tipo != diftype).Select(xh => new Models.Persona.Persona
                {
                    per_vista = type,
                    per_id = xh.per_id,
                    per_nombre = xh.per_nombre,
                    per_direccion = xh.per_direccion,
                    per_telefono = xh.per_telefono,
                    per_cuenta_bancaria = xh.per_cuenta_bancaria,
                    per_correo = xh.per_correo,
                    per_nit =(int) xh.per_nit,
                    per_tipo = xh.per_tipo


                }).ToList();

                return data;
            }
        }
        public void Create(Persona model)
        {
            int ciu_id = Convert.ToInt32(model.ciu_id);
            var newData = new t_persona
            {
                per_id = model.per_id,
                ciu_id = ciu_id,
                per_nombre = model.per_nombre,
                per_direccion = model.per_direccion,
                per_telefono = model.per_telefono,
                per_cuenta_bancaria = model.per_cuenta_bancaria,
                per_correo = model.per_correo,
                per_nit = model.per_nit,
                per_tipo = model.per_tipo

            };

            _dbContext.t_persona.Add(newData);

            _dbContext.SaveChanges();
        }

        public void Update(Persona model)
        {
            var existingUsuario = _dbContext.t_persona.FirstOrDefault(u => u.per_id == model.per_id);

            if (existingUsuario != null)
            {

                existingUsuario.per_nombre = model.per_nombre;
                existingUsuario.per_direccion = model.per_direccion;
                existingUsuario.per_telefono = model.per_telefono;
                existingUsuario.per_cuenta_bancaria = model.per_cuenta_bancaria;
                existingUsuario.per_correo = model.per_correo;
                existingUsuario.per_nit = model.per_nit;
                existingUsuario.per_tipo = model.per_tipo;

                _dbContext.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var toDelete = _dbContext.t_persona.FirstOrDefault(u => u.per_id == id);
            if (toDelete != null)
            {
                _dbContext.t_persona.Remove(toDelete);
                _dbContext.SaveChanges();
            }

        }

        public Models.Persona.Persona GetDataById(int id,string typeView)
        {
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_persona.FirstOrDefault(u => u.per_id == id);

                if (data != null)
                {
                    return new Models.Persona.Persona
                    {
                        per_vista = typeView,
                        per_id = data.per_id,
                        per_nombre = data.per_nombre,
                        per_direccion = data.per_direccion,
                        per_telefono = data.per_telefono,
                        per_cuenta_bancaria = data.per_cuenta_bancaria,
                        per_correo = data.per_correo,
                        per_nit = (int)data.per_nit,
                        per_tipo = data.per_tipo,
                        ciu_id = Convert.ToString(data.ciu_id),
                        per_tipo_cuenta = data.per_tipo_cuenta,
                        per_tipo_documento = data.per_tipo_documento

                    };
                }
                else
                {

                    return null;
                }
            }
        }
    }
}
