using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Extensions.ObjectEx
{
    public static class ObjectExtension
    {
        public static string ToXml(this object value)
        {
            var xs = new XmlSerializer(value.GetType());
    
            // use new UTF8Encoding here, not Encoding.UTF8. 
            using (var memoryStream = new MemoryStream())
            using (var xmlTextWriter 
                = new XmlTextWriter(memoryStream, new UTF8Encoding()))
            {
                xs.Serialize(xmlTextWriter, value);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}
