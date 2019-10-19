using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Common.Contracts;
using Core.Common.Utils;

namespace Core.Common.Data
{
    public abstract class DataRepositoryBase<T, U> : IDataRepository<T>
        where T : class , IIdentifiableEntity, new()
        where U : DbContext, new()
    {
        #region
        /*
         * Sub-class lar tarafından override edilmesi zorunlu olan metotlar
         * Bu metorları sub class'da ovveride ederek Get, Update
         *  vb. gibi metotlar repository'ye göre özelleştirilebilir.
         *   (Filling the blanks)
         *  
         * Herhangi bir repository Get() metodunu çağırdığında
         * İstek bu sınıftaki Get() metoduna geliyor,
         * Get() metodu ise GetEntity metodunu çağırıyor 
         *  (ki bu metot abstract olduğundan ve abstract olan metotlar
         *  override edilmesi zorunlu olan metotlar olduğundan) 
         * bu metotta hangi entity (mesela movie) 
         * isteniyorsan onun repository'sine gidiyor, (MovieRepository),
         * oradaki implementasyonu çalıştırıyor.
         */
        protected abstract T AddEntity(U entityContext, T entity);
        protected abstract T UpdateEntity(U entityContext, T entity);
        protected abstract T GetEntity(U entityContext, int id);
        protected abstract IEnumerable<T> GetEntities(U entityContext);
        # endregion

        public T Add(T entity)
        {
            using (U entityContext = new U())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public void Remove(T entity)
        {
            using (U entityContext = new U())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (U entityContext = new U())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public T Update(T entity)
        {
            using (U entityContext = new U())
            {
                T existingEntity = UpdateEntity(entityContext, entity);
                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        public IEnumerable<T> Get()
        {
            using (U entityContext = new U())
                return (GetEntities(entityContext)).ToList();
        }

        public T Get(int id)
        {
            using (U entityContext = new U())
            {
                return (GetEntity(entityContext, id));
            }
        }
    }
}

