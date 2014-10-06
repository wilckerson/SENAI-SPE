using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using System.Web;

namespace Common
{
    public class Logging
    {
        private static Logging _Instance = null;
        private static object _objLock = new object();

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static Logging getInstance()
        {
            lock (_objLock)
            {
                if (_Instance == null)
                {
                    _Instance = new Logging();
                }
                return _Instance;
            }
        }

        public void Dispose()
        {
            _objLock = new object();
            _Instance = null;
        }

        public void HandlerDBErrorLog(Exception ex)
        {
            log4net.Config.XmlConfigurator.Configure();

            if (ex is System.Data.Entity.Validation.DbEntityValidationException)
            {
                var ve = ex as System.Data.Entity.Validation.DbEntityValidationException;
                //var error = ve. EntityValidationErrors.First().ValidationErrors.First();

                foreach (var entity in ve.EntityValidationErrors)
                {
                    var entityName = entity.Entry.Entity.GetType().Name;

                    foreach (var error in entity.ValidationErrors)
                    {
                        log.Error(String.Format("Erro ao salvar o domínio: {0} - Mensagem de Erro: {1} - Propriedade com Erro: {2}",
                            entityName, error.ErrorMessage, error.PropertyName));
                    }
                }
            }
        }

        public void Error(string message, Exception e)
        {
            log.Error(message + "; Url: " + HttpContext.Current.Request.Url.AbsoluteUri + "; Error: ", e);
        }

        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Fatal(string message)
        {
            log.Fatal(message);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }
    }
}
