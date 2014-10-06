using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common.Security
{
    public class LoggedUser
    {
        private int _id;
        private int _perfilId;
        private String _name;
        private String _login;
        private String _password;
        private String _email;

        public LoggedUser()
        {
            this._id = HttpContext.Current.Session["Usuario"] != null ? ((LoggedUser)HttpContext.Current.Session["Usuario"]).Id : 0;
            this._perfilId = HttpContext.Current.Session["Usuario"] != null ? ((LoggedUser)HttpContext.Current.Session["Usuario"]).PerfilId : 0;
            this._name = HttpContext.Current.Session["Usuario"] != null ? ((LoggedUser)HttpContext.Current.Session["Usuario"]).Name : "";
            this._login = HttpContext.Current.Session["Usuario"] != null ? ((LoggedUser)HttpContext.Current.Session["Usuario"]).Login : "";
            this._password = HttpContext.Current.Session["Usuario"] != null ? ((LoggedUser)HttpContext.Current.Session["Usuario"]).Password : "";
            this._email = HttpContext.Current.Session["Usuario"] != null ? ((LoggedUser)HttpContext.Current.Session["Usuario"]).Email : "";
          
        }

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public int PerfilId
        {
            get
            {
                return this._perfilId;
            }
            
            set
            {
                this._perfilId = value;
            }
        }

        public String Name
        {
            get
            {
                return this._name;
            }
            
            set
            {
                this._name = value;
            }
        }

        public String Login
        {
            get
            {
                return this._login;
            }

            set
            {
                this._login = value;
            }
        }

        public String Password
        {
            get
            {
                return this._password;
            }

            set
            {
                this._password = value;
            }
        }

        public String Email
        {
            get
            {
                return this._email;
            }

            set
            {
                this._email = value;
            }
        }
    }
}