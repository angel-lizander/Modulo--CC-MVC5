using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Cuentas_x_Cobrar.Helpers
{
    public static class Extensions
    {
        public static string GetDescription(this System.Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());

            if (fieldInfo == null)
                return String.Empty;

            var attribs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            var result = String.Empty;
            if (attribs.Any())
            {
                var attr = attribs[0];
                result = attr.Description;
            }
            else
                result = value.ToString();

            return result;
        }

        public static List<SelectListItem> ToSelectList<T>()
            where T : struct
        {
            var result = new List<SelectListItem>();

            if (typeof(T).IsEnum)
            {
                foreach (var item in Enum.GetValues(typeof(T)))
                {
                    result.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = ((Enum)item).GetDescription(),
                        Value = item.ToString()
                    });
                }
            }

            return result;
        }
    }
}