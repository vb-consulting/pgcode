﻿using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Pgcode.DataAccess.Extensions
{
    public static partial class ConnectionExtensions
    {
        public static async ValueTask<IList<(string name, object value)>> SingleAsync(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync(command);

        public static async ValueTask<IList<(string name, object value)>> SingleAsync(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync(command, parameters);

        public static async ValueTask<IList<(string name, object value)>> SingleAsync(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync(command, parameters);

        public static async ValueTask<T> SingleAsync<T>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T>(command);

        public static async ValueTask<T> SingleAsync<T>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T>(command, parameters);

        public static async ValueTask<T> SingleAsync<T>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T>(command, parameters);

        public static async ValueTask<(T1, T2)> SingleAsync<T1, T2>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2>(command);

        public static async ValueTask<(T1, T2)> SingleAsync<T1, T2>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2>(command, parameters);

        public static async ValueTask<(T1, T2)> SingleAsync<T1, T2>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2>(command, parameters);

        public static async ValueTask<(T1, T2, T3)> SingleAsync<T1, T2, T3>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3>(command);

        public static async ValueTask<(T1, T2, T3)> SingleAsync<T1, T2, T3>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3>(command, parameters);

        public static async ValueTask<(T1, T2, T3)> SingleAsync<T1, T2, T3>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4)> SingleAsync<T1, T2, T3, T4>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4>(command);

        public static async ValueTask<(T1, T2, T3, T4)> SingleAsync<T1, T2, T3, T4>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4)> SingleAsync<T1, T2, T3, T4>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5)> SingleAsync<T1, T2, T3, T4, T5>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5>(command);

        public static async ValueTask<(T1, T2, T3, T4, T5)> SingleAsync<T1, T2, T3, T4, T5>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5)> SingleAsync<T1, T2, T3, T4, T5>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6)> SingleAsync<T1, T2, T3, T4, T5, T6>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6>(command);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6)> SingleAsync<T1, T2, T3, T4, T5, T6>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6)> SingleAsync<T1, T2, T3, T4, T5, T6>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7)> SingleAsync<T1, T2, T3, T4, T5, T6, T7>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7>(command);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7)> SingleAsync<T1, T2, T3, T4, T5, T6, T7>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7)> SingleAsync<T1, T2, T3, T4, T5, T6, T7>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(command);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this DbConnection connection, string command) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(command);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this DbConnection connection, string command, params object[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(command, parameters);

        public static async ValueTask<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            await connection.GetDataAccessInstance().SingleAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(command, parameters);
    }
}