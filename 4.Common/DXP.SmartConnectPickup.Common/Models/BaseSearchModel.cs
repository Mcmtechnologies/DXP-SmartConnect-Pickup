using System;
using System.Collections.Generic;
using System.Linq;

namespace DXP.SmartConnectPickup.Common.Models
{
    public class BaseSearchModel
    {
        public BaseSearchModel()
        {
            Filters = new List<FilterModel>();
        }

        public string Keyword { get; set; }

        public IList<FilterModel> Filters { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Sort { get; set; }

        public dynamic GetFilterByName(string name, Type castTo)
        {
            if (Filters == null)
            {
                Filters = new List<FilterModel>();
            }

            var filter = Filters.FirstOrDefault(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));
            if (filter != null)
            {
                if (filter.Type == EnumFilterType.Value)
                {
                    var value = filter.Values;
                    if (value == null || !value.Any())
                    {
                        if (castTo.IsValueType)
                        {
                            return Activator.CreateInstance(castTo);
                        }

                        return null;
                    }

                    if (castTo.IsGenericType && (castTo.GetGenericTypeDefinition() == typeof(List<>) || castTo.GetGenericTypeDefinition() == typeof(IList<>)))
                    {
                        var elementTypes = castTo.GetGenericArguments();
                        return value.Select(x => Convert.ChangeType(x, elementTypes.FirstOrDefault()));
                    }
                    else if (castTo.IsArray)
                    {
                        var elementType = castTo.GetElementType();
                        return value.Select(item => Convert.ChangeType(item, elementType));
                    }

                    return Convert.ChangeType(value.FirstOrDefault(), castTo);
                }
                else
                {
                    var result = new List<dynamic>();
                    result.Add(Convert.ChangeType(filter.FromValue, castTo));
                    result.Add(Convert.ChangeType(filter.ToValue, castTo));
                    return result;
                }
            }

            return null;
        }
    }

    public enum EnumFilterType
    {
        Value = 0,
        Range = 1
    }

    public class FilterModel
    {
        public FilterModel()
        {
            Values ??= new List<string>();
        }

        public string Name { get; set; }

        public IList<string> Values { get; set; }

        public string FromValue { get; set; }

        public string ToValue { get; set; }

        public EnumFilterType Type { get; set; }
    }
}
