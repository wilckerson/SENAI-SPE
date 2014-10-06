using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }

        public CustomException()
            : base() { }

        public CustomException(string message)
            : base(message) { }

        /// <summary>
        /// 1- Já existe no banco. 
        /// 2- Tabelas Associadas.
        /// </summary>
        /// <param name="errorCode"></param>
        public CustomException(int errorCode)
            : base() { this.ErrorCode = errorCode; }

        /// <summary>
        /// 1- Já existe no banco. 
        /// 2- Tabelas Associadas.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        public CustomException(string message, int errorCode)
            : base(message) { this.ErrorCode = errorCode; }

        //public CustomException(string format, params object[] args)
        //    : base(string.Format(format, args)) { }

        public CustomException(string message, Exception innerException)
            : base(message, innerException) { }

        //public CustomException(string format, Exception innerException, params object[] args)
        //    : base(string.Format(format, args), innerException) { }

        //protected CustomException(SerializationInfo info, StreamingContext context)
        //    : base(info, context) { }
    }
}
