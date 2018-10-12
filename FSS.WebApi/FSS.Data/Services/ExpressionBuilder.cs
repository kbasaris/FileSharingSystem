using FSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FSS.Data.Services
{
    public class ExpressionBuilder
    {
        public ExpressionBuilder() { }


        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");

        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });

        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(IList<FilterDto> filters)
        {
            if (filters.Count == 0)
                return null;
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;
            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var fl1 = filters[0];
                    var fl2 = filters[1];
                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));
                    filters.Remove(fl1);
                    filters.Remove(fl2);
                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }

                }
            }
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }


        public static Expression GetExpression<T>(ParameterExpression param, FilterDto filter)
        {
            if (filter.PropertyName.Contains("."))
            {
                if (filter.PropertyName.Split('.').Length == 2)
                {
                    MemberExpression member2 = Expression.Property(param, filter.PropertyName.Split('.')[0]);

                    MemberExpression member = Expression.Property(member2, filter.PropertyName.Split('.')[1]);
                    ConstantExpression constant = Expression.Constant(filter.Value);
                    switch (filter.Operation)
                    {
                        case Operation.Equals:
                            return Expression.Equal(member, Expression.Convert(constant, member.Type));
                        case Operation.GreaterThan:
                            return Expression.GreaterThan(member, Expression.Convert(constant, member.Type));
                        case Operation.GreaterThanOrEqual:
                            return Expression.GreaterThanOrEqual(member, Expression.Convert(constant, member.Type));
                        case Operation.LessThan:
                            return Expression.LessThan(member, Expression.Convert(constant, member.Type));
                        case Operation.LessThanOrEqual:
                            return Expression.LessThanOrEqual(member, Expression.Convert(constant, member.Type));
                        case Operation.Contains:
                            return Expression.Call(member, containsMethod, constant);
                        case Operation.StartsWith:
                            return Expression.Call(member, startsWithMethod, constant);
                        case Operation.EndsWith:
                            return Expression.Call(member, endsWithMethod, constant);

                    }
                }

            }
            else
            {


                MemberExpression member = Expression.Property(param, filter.PropertyName);
                ConstantExpression constant = Expression.Constant(filter.Value);
                switch (filter.Operation)
                {
                    case Operation.Equals:
                        return Expression.Equal(member, Expression.Convert(constant, member.Type));
                    case Operation.GreaterThan:
                        return Expression.GreaterThan(member, Expression.Convert(constant, member.Type));
                    case Operation.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, Expression.Convert(constant, member.Type));
                    case Operation.LessThan:
                        return Expression.LessThan(member, Expression.Convert(constant, member.Type));
                    case Operation.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, Expression.Convert(constant, member.Type));
                    case Operation.Contains:
                        return Expression.Call(member, containsMethod, constant);
                    case Operation.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);
                    case Operation.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);

                }
            }


            return null;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, FilterDto filter1, FilterDto filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);
            if (filter1.ComparisonRule == "Or")
            {
                return Expression.Or(bin1, bin2);
            }
            else
            {
                return Expression.AndAlso(bin1, bin2);
            }

        }
    }
}
