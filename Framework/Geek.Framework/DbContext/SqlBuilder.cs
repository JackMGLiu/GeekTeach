using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Geek.Framework.Extensions;
using Geek.Framework.Utilities;

namespace Geek.Framework.Db
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

        public static string Insert(string tableName, object columns)
        {
            var colNames = GetParamNames(columns);
            var sql = $"INSERT INTO {tableName}({string.Join(", ", colNames)}) VALUES(@{string.Join(", @", colNames)})";
            return sql;
        }

        public static string Update(string tableName, object update, object clause)
        {
            var updateCols = GetParamNames(update);
            var clauseCols = GetParamNames(clause);
            var sql = $"UPDATE {tableName} SET {string.Join(", ", updateCols.Select(x => x + "=@" + x))} ";
            if (clauseCols != null && clauseCols.Count() > 0)
                sql += Where(clause);
            return sql;
        }

        public static string Delete(string tableName, object clause)
        {
            var clauseCols = GetParamNames(clause);
            var sql = $"DELETE FROM {tableName} ";
            if (clauseCols != null && clauseCols.Count() > 0)
                sql += Where(clause);
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

        /// <summary>
        /// Merge parameters.
        /// </summary>
        /// <param name="firstParam">The first parameter to merge.</param>
        /// <param name="otherParams">The other prameters to merge.</param>
        /// <returns>Merged parameters.</returns>
        public static DynamicParameters MergeParams(object firstParam, params object[] otherParams)
        {
            Guard.NotNull(firstParam, nameof(firstParam));

            var parameters = new DynamicParameters();
            parameters.AddDynamicParams(firstParam);
            foreach (var param in otherParams)
            {
                parameters.AddDynamicParams(param);
            }
            return parameters;
        }
    }
}
