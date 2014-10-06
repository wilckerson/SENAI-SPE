using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Models.Base
{
    public class ModelBase<TEntity>
        where TEntity : class, new()
    {
        public TEntity data { get; set; }

        private static GenericBL<TEntity> BL = new GenericBL<TEntity>();

        public TEntity Add()
        {
            BL.Generic.Add(data);
            return data;
        }

        public TEntity Update()
        {
            BL.Generic.Update(data);
            return data;
        }

        public bool Delete()
        {
            try
            {
                BL.Generic.Delete(data);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public static IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null, int? qtPage = null)
        {
            return BL.Generic.Get(filter, orderBy, includeProperties);
        }

        public static int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return BL.Generic.GetCount(filter);
        }

        public static TEntity GetById(int id)
        {
            return BL.Generic.GetById(id);
        }

        public TEntity MapToEntity(TEntity model, string removeFromBind = null, string addOnBind = null)
        {
            TEntity entity = new TEntity();

            if (addOnBind != null)
            {
                foreach (string item in addOnBind.Trim().Split(','))
                {
                    setProperty(entity, item, getProperty(model, item).GetType().GetProperty(item).GetValue(model));
                }
            }
            else
            {
                string[] exclude = removeFromBind != null ? removeFromBind.Trim().Split(',') : new string[0];

                foreach (var prop in model.GetType().GetProperties().Where(a => !exclude.Equals(a.Name)))
                {
                    setProperty(entity, prop.Name, prop.GetValue(model));
                }
            }

            return entity;
        }

        private void setProperty(object containingObject, string propertyName, object newValue)
        {
            containingObject.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, containingObject, new object[] { newValue });
        }

        private object getProperty(object containingObject, string propertyName)
        {
            return containingObject.GetType().InvokeMember(propertyName, BindingFlags.GetProperty, null, containingObject, null);
        }
    }


}