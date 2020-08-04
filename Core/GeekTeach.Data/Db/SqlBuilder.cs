using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Geek.Framework.Extensions;

namespace GeekTeach.Data.Db
{
    /// <summary>
    /// Sql生成器
    /// </summary>
    public static class SqlBuilder
    {
        public static string And(object clause)
        {
            if (clause == null)
            {
                return "1=1";
            }
            var names = GetParamNames(clause);
            var type = clause.GetType();

            var res = string.Join(" AND ", names.Select(x =>
            {
                var propVal = type.GetProperty(x).GetValue(clause);
                if (propVal == null)
                {
                    return x + " IS NULL ";
                }
                else if (propVal is IEnumerable && !(propVal is string))
                {
                    return x + " IN @" + x;
                }
                else
                {
                    return x + "=@" + x;
                }
            }));

            return res;
        }

        public static string Where(object clause)
        {
            return "WHERE " + And(clause);
        }

        public static string Select(string tableName, object clause)
        {
            var sql = $"SELECT * FROM {tableName} ";
            if (clause != null)
            {
                sql += Where(clause);
            }
            return sql;
        }

        public static string[] GetParamNames(object param)
        {
            if (param == null)
            {
                return new string[] { };
            }

            if (param is string str)
            {
                if (str.Contains(','))
                    return str
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim()).ToArray();
                return new[] { str };
            }

            if (param is IEnumerable<string> array)
            {
                return array.ToArray();
            }

            if (param is DynamicParameters dynamicParameters)
            {
                return dynamicParameters.ParameterNames.ToArray();
            }

            var type = param is Type ? (param as Type) : param.GetType();

            return type
                .GetProperties()
                .Where(x => x.PropertyType.IsSimpleType() || x.PropertyType.GetAnyElementType().IsSimpleType())
                .Select(x => x.Name).ToArray();
        }
    }
}
