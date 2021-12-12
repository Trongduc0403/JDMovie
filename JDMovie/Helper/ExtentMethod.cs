using System.ComponentModel;
using System.Dynamic;
using System.Xml;

namespace JDMovie.Helper
{
    public static class ExtentMethod
    {
        #region Anonymous Object Processing

        #region Will object[Primarily anonymous objects]Convert to dynamic
        /// <summary>
        /// Will object[Primarily anonymous objects]Convert to dynamic
        /// </summary>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            var type = value.GetType();
            var properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.ToDynamic();
                    expando.Add(property.Name, dval);
                }
                else
                {
                    expando.Add(property.Name, val);
                }
            }
            return expando as ExpandoObject;
        }
        #endregion

        #region Will object[Primarily anonymous objects]Convert to List<dynamic>
        /// <summary>
        /// Will object[Primarily anonymous objects]Convert to List<dynamic>
        /// </summary>
        public static List<dynamic> ToDynamicList(this IEnumerable<dynamic> values)
        {
            var list = new List<dynamic>();
            if (values != null)
            {
                if (values.Any())
                {
                    list.AddRange(values.Select(v => ((object)v).ToDynamic()));
                }
            }

            return list;
        }
        #endregion

        #region Converting an anonymous collection of objects to XML
        /// <summary>
        /// Converting an anonymous collection of objects to XML
        /// </summary>
        public static XmlDocument ListObjertToXML(this IEnumerable<dynamic> values)
        {
            var xmlDoc = new XmlDocument();
            var xmlElem = xmlDoc.CreateElement("DocumentElement");
            xmlDoc.AppendChild(xmlElem);
            if (values != null)
            {
                if (values.Any())
                {
                    var node = xmlDoc.SelectSingleNode("DocumentElement");
                    foreach (var item in values)
                    {
                        var xmlRow = xmlDoc.CreateElement("Row");
                        ObjectToXML(item, xmlDoc, xmlRow);
                        node.AppendChild(xmlRow);
                    }
                }
            }

            return xmlDoc;
        }
        #endregion

        #region Fill in anonymous objects XML node
        /// <summary>
        /// Fill in anonymous objects XML node
        /// </summary>
        private static void ObjectToXML(object value, XmlDocument xmlDoc, XmlElement xmlRow)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            var type = value.GetType();
            var properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor property in properties)
            {
                var val = property.GetValue(value);
                xmlRow.CloneNode(false);
                var xmlTemp = xmlDoc.CreateElement(property.Name);
                XmlText xmlText;
                if (property.PropertyType.FullName.StartsWith("<>f__AnonymousType"))
                {
                    dynamic dval = val.ToDynamic();
                    xmlText = xmlDoc.CreateTextNode(dval.ObjectToString());
                }
                else
                {
                    xmlText = xmlDoc.CreateTextNode(val.ToString());
                }

                xmlTemp.AppendChild(xmlText);
                xmlRow.AppendChild(xmlTemp);
            }
        }
        #endregion

        #endregion
    }
}   

