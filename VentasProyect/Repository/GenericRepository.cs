using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VentasProyect.Repository
{
    public class GenericRepository<T> where T : class
    {
        public Models.VENTAS_DBEntities1 Context { get; set; }        

        public GenericRepository(Models.VENTAS_DBEntities1 dbContext)
        {
            Context = dbContext;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }
        
        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}