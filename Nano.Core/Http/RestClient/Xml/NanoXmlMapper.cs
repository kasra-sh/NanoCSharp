using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Nano.Core.Http.RestClient.Xml
{
    public static class NanoXmlMapper
    {
        public static T Map<T>(string xml) where T : class
        {
            var t = typeof(T);
            var x = XElement.Parse(xml);
            if (!GetTypeName(t).ToLower().Equals(x.Name.LocalName.ToLower()))
            {
                throw new XmlException("Root does not match");
            }

//            var obj = Activator.CreateInstance<T>();
            return (T) MapType(t, x);
        }

        static bool IsEnumerable(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }


        // TODO Separate list mapper
        static object MapType(Type type, XElement element)
        {
            if (element == null)
            {
                return null;
            }

            var obj = Activator.CreateInstance(type);

            var properties = type.GetProperties();

            var typeName = GetTypeName(type);
            if (typeName == type.Name)
            {
                typeName = null;
            }

            foreach (var propertyInfo in properties)
            {
                var pType = propertyInfo.PropertyType;
                var name = GetPropertyName(propertyInfo);
//                if (typeName != null)
//                {
//                    name = typeName;
//                }

                // IGNORE
                if (GetPropertyAttributeData<XmlIgnoreAttribute>(propertyInfo) != null)
                {
                    continue;
                }

                // TEXT
                if (GetPropertyAttributeData<XmlTextAttribute>(propertyInfo) != null)
                {
                    SetProperty(obj, propertyInfo, element.Value);
                    continue;
                }

                // ATTRIBUTE
                var attr = GetPropertyAttributeData<XmlAttributeAttribute>(propertyInfo);
                if (attr != null)
                {
                    if (!pType.IsAssignableFrom(typeof(string)))
                    {
                        if (IsEnumerable(pType)) throw new XmlException("Cannot map Attribute to List: " + name);
                        if (pType.IsClass) throw new XmlException("Cannot map Attribute to Class: " + name);
                    }

                    SetProperty(obj, propertyInfo, GetElementAttribute(element, name));
                    continue;
                }

                // ELEMENT
                var elem = GetPropertyAttributeData<XmlElementAttribute>(propertyInfo);
                if (elem != null)
                {
                    if (pType.IsAssignableFrom(typeof(string)))
                    {
                        SetProperty(obj, propertyInfo, GetElementValue(element, name));
                        continue;
                    }

                    // Enumerable - List
                    if (IsEnumerable(pType))
                    {
//                        Console.WriteLine("> Mapping List of "+pType);
                        var elements = element.Elements(XName.Get(name));
                        var elems = element.ElementsAfterSelf(XName.Get(name));
                        if (!elements.Any())
                        {
                            elements = elems;
                        }

                        var genericArgs = pType.GetGenericArguments();
                        Type genericType = typeof(string);
                        if (genericArgs != null)
                        {
                            if (genericArgs.Length > 0)
                            {
                                genericType = genericArgs[0];
                            }
                        }

                        var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(genericType));

                        foreach (var xElement in elements)
                        {
                            if (genericType.IsAssignableFrom(typeof(string)))
                            {
                                list.Add(xElement.Value);
                            }
                            else
                            {
                                if (genericType.IsClass)
                                {
                                    list.Add(MapType(genericType, xElement));
                                }
                                else
                                {
                                    try
                                    {
                                        list.Add(CastSimpleType(genericType, xElement.Value));
                                    }
                                    catch (Exception e)
                                    {
                                    }
                                }
                            }
                        }

                        propertyInfo.SetValue(obj, list);
                        continue;
                    }

                    // Data Class
                    if (pType.IsClass)
                    {
                        propertyInfo.SetValue(obj, MapType(pType, element.Element(XName.Get(name))));
                        continue;
                    }

	                SetProperty(obj, propertyInfo, GetElementValue(element, name));
                }
			}

            return obj;
        }


        #region Internal

        private static object CastSimpleType(Type type, string value)
        {
            if (type.IsAssignableFrom(typeof(string)))
            {
                return value;
            }

            if (value == null)
            {
                return null;
            }

            if (value == "")
            {
                return null;
            }

            if (typeof(bool?).IsAssignableFrom(type))
            {
                bool res;
                if (bool.TryParse(value, out res))
                    return res;
                else
                {
                    return null;
                }
            }

            if (typeof(int?).IsAssignableFrom(type))
            {
                int res;
                if (int.TryParse(value, out res))
                    return res;
                else
                {
                    return null;
                }
            }

            if (typeof(long?).IsAssignableFrom(type))
            {
                long res = 0;
                if (long.TryParse(value, out res))
                    return res;
                else
                {
                    return null;
                }
            }

            if (typeof(float?).IsAssignableFrom(type))
            {
                float res = 0;
                if (float.TryParse(value, out res))
                    return res;
                else
                {
                    return null;
                }
            }

            if (typeof(double?).IsAssignableFrom(type))
            {
                double res = 0;
                if (double.TryParse(value, out res))
                    return res;
                else
                {
                    return null;
                }
            }

            if (typeof(decimal?).IsAssignableFrom(type))
            {
                decimal res = 0;
                if (decimal.TryParse(value, out res))
                    return res;
                else
                {
                    return null;
                }
            }

            if (typeof(DateTime?).IsAssignableFrom(type))
            {
                DateTime res;
                if (DateTime.TryParse(value, out res))
                    return res;
                else
                {
                    return null;
                }
            }

            throw new InvalidCastException();
        }

        private static void SetProperty(object src, PropertyInfo prop, string value)
        {
            var type = prop.PropertyType;
            var c = CastSimpleType(type, value);
            if (c != null)
            {
                prop.SetValue(src, c);
            }
        }
//        public static object MapObject(object source, XElement element)
//        {
//            var props = source.GetType().GetProperties();
//            foreach (var propertyInfo in props)
//            {
//                var type = propertyInfo.PropertyType;
//                if (type.IsPrimitive)
//                {
////                    type
//                }
//                try
//                {
//                    propertyInfo.SetValue(source, GetElementValue(element, GetPropertyName(propertyInfo)));
//                }
//                catch (Exception e)
//                {
//                    // ignored
//                }
//            }
//        }

//        public static void Deserialize

        private static string GetElementValue(XElement element, string name)
        {
            return element.Element(XName.Get(name))?.Value;
        }

        private static XElement GetElement(XElement element, string name)
        {
            return element.Element(XName.Get(name));
        }

        private static XAttribute GetAttribute(XElement element, string name)
        {
            return element.Attribute(XName.Get(name));
        }

        private static string GetElementAttribute(XElement element, string name)
        {
            return element.Attribute(XName.Get(name))?.Value;
        }

        private static E GetPropertyAttributeData<E>(PropertyInfo info) where E : Attribute
        {
            return info.GetCustomAttribute<E>();
        }

        private static E GetClassAttributeData<E>(Type type) where E : Attribute
        {
            return type.GetCustomAttribute<E>();
        }

        private static string GetPropertyName(PropertyInfo prop)
        {
            var attr = GetPropertyAttributeData<XmlAttributeAttribute>(prop);
            if (attr == null)
            {
//                Console.WriteLine("Attr NULL");
            }
            else
            {
                if (attr.AttributeName != null)
                    if (attr.AttributeName != "")
                        return attr.AttributeName;
            }

            var el = GetPropertyAttributeData<XmlElementAttribute>(prop);
            if (el == null)
            {
//                Console.WriteLine("Element NULL");
            }
            else
            {
                if (el.ElementName != null)
                    if (el.ElementName != "")
                        return el.ElementName;
            }

            return prop.Name;
        }

        private static string GetTypeName(Type prop)
        {
            var root = GetClassAttributeData<XmlRootAttribute>(prop);
            if (root == null)
            {
//                Console.WriteLine("Root NULL");
            }
            else
            {
                if (root.ElementName != null)
                    if (root.ElementName != "")
                        return root.ElementName;
            }

            var attr = GetClassAttributeData<XmlAttributeAttribute>(prop);
            if (attr == null)
            {
//                Console.WriteLine("Attr NULL");
            }
            else
            {
                if (attr.AttributeName != null)
                    if (attr.AttributeName != "")
                        return attr.AttributeName;
            }

            var el = GetClassAttributeData<XmlElementAttribute>(prop);
            if (el == null)
            {
//                Console.WriteLine("Element NULL");
            }
            else
            {
                if (el.ElementName != null)
                    if (el.ElementName != "")
                        return el.ElementName;
            }

            return prop.Name;
        }

        #endregion
    }

    public class PropertyData
    {
        public string Name { get; set; }

        public bool IsAttribute { get; set; }

        public bool IsElement { get; set; }

        public bool IsText { get; set; }

        public bool IsRoot { get; set; }
    }
}