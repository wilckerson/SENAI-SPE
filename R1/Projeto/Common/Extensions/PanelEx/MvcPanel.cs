using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics.CodeAnalysis;

namespace Common.Extensions.PanelEx
{
    public class MvcPanel : IDisposable
    {
        #region Fields

        private bool disposed;
        private readonly HttpResponseBase httpResponse;

        #endregion

        #region CTOR

        public MvcPanel(HttpResponseBase httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException("httpResponse");
            }
            this.httpResponse = httpResponse;
        }

        #endregion

        #region Methods

        [SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.httpResponse.Write("</div>");
            }
        }

        public void EndPanel()
        {
            Dispose(true);
        }

        #endregion
    }
}
