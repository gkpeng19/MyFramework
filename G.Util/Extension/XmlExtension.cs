using GOMFrameWork.DataEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace G.Util.Extension
{
    public class XmlKeyAttribute : Attribute { }
    public class XmlEntityAttribute : Attribute { }
    public class XmlListAttribute : Attribute { }

    public static class XmlExtension
    {
        static EntityBase Convert2Entity(XmlNode node, Type type)
        {
            EntityBase entity = Activator.CreateInstance(type) as EntityBase;
            if (entity == null)
            {
                return null;
            }
            foreach (XmlNode cnode in node.ChildNodes)
            {
                PropertyInfo pi = type.GetProperty(cnode.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (pi.GetCustomAttributes(typeof(XmlKeyAttribute), false).Length > 0)
                {
                    entity.SetUIValue(cnode.Name, cnode.InnerText);
                }
                else if (pi.GetCustomAttributes(typeof(XmlListAttribute), false).Length > 0)
                {
                    IList list = Activator.CreateInstance(pi.PropertyType) as IList;
                    foreach (XmlNode nd in cnode.ChildNodes)
                    {
                        list.Add(Convert2Entity(nd, pi.PropertyType.GetGenericArguments()[0]));
                    }
                    pi.SetValue(entity, list, null);
                }
                else if (pi.GetCustomAttributes(typeof(XmlEntityAttribute), false).Length > 0)
                {
                    pi.SetValue(entity, Convert2Entity(cnode, pi.PropertyType), null);
                }
            }

            return entity;
        }
        public static T ReadToEntity<T>(string path) where T : EntityBase
        {
            object entity = null;
            if (!File.Exists(path))
            {
                return null;
            }

            using (StreamReader sr = new StreamReader(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(sr);
                entity = Convert2Entity(doc.DocumentElement, typeof(T));
            }
            return entity as T;
        }


        static XmlElement Convert2XmlElement(XmlDocument doc, EntityBase entity)
        {
            Type type = entity.GetType();
            XmlElement root = doc.CreateElement(type.Name);
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (pi.GetCustomAttributes(typeof(XmlKeyAttribute), false).Length > 0)
                {
                    var v = entity.GetString(pi.Name);
                    if (v.Length > 0)
                    {
                        XmlElement cele = doc.CreateElement(pi.Name);
                        cele.InnerText = v;
                        root.AppendChild(cele);
                    }
                }
                else if (pi.GetCustomAttributes(typeof(XmlListAttribute), false).Length > 0)
                {
                    IList list = pi.GetValue(entity, null) as IList;
                    if (list == null)
                    {
                        continue;
                    }

                    XmlElement ele = doc.CreateElement(pi.Name);
                    root.AppendChild(ele);

                    foreach (EntityBase l in list)
                    {
                        ele.AppendChild(Convert2XmlElement(doc, l));
                    }
                }
                else if (pi.GetCustomAttributes(typeof(XmlEntityAttribute), false).Length > 0)
                {
                    var e = pi.GetValue(entity, null) as EntityBase;
                    if (e == null)
                    {
                        continue;
                    }
                    root.AppendChild(Convert2XmlElement(doc, e));
                }
            }

            return root;
        }
        public static void SaveToXml(this EntityBase entity, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string directory = path.Substring(0, path.LastIndexOf('\\') + 1);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            XmlDocument doc = new XmlDocument();
            doc.AppendChild(Convert2XmlElement(doc, entity));
            using (StreamWriter sw = new StreamWriter(path))
            {
                doc.Save(sw);
            }
        }
    }
}
