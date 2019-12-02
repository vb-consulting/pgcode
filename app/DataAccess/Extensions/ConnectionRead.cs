﻿using System.Collections.Generic;
using System.Data.Common;

namespace Pgcode.DataAccess.Extensions
{
    public static partial class ConnectionExtensions
    {
        public static IEnumerable<IList<(string name, object value)>> Read(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read(command);

        public static IEnumerable<IList<(string name, object value)>> Read(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read(command, parameters);

        public static IEnumerable<IList<(string name, object value)>> Read(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read(command, parameters);

        public static IEnumerable<T> Read<T>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T>(command);

        public static IEnumerable<T> Read<T>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T>(command, parameters);

        public static IEnumerable<T> Read<T>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T>(command, parameters);

        public static IEnumerable<(T1, T2)> Read<T1, T2>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2>(command);

        public static IEnumerable<(T1, T2)> Read<T1, T2>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2>(command, parameters);

        public static IEnumerable<(T1, T2)> Read<T1, T2>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2>(command, parameters);

        public static IEnumerable<(T1, T2, T3)> Read<T1, T2, T3>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3>(command);

        public static IEnumerable<(T1, T2, T3)> Read<T1, T2, T3>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3>(command, parameters);

        public static IEnumerable<(T1, T2, T3)> Read<T1, T2, T3>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4)> Read<T1, T2, T3, T4>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4>(command);

        public static IEnumerable<(T1, T2, T3, T4)> Read<T1, T2, T3, T4>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4)> Read<T1, T2, T3, T4>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5)> Read<T1, T2, T3, T4, T5>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5>(command);

        public static IEnumerable<(T1, T2, T3, T4, T5)> Read<T1, T2, T3, T4, T5>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5)> Read<T1, T2, T3, T4, T5>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6)> Read<T1, T2, T3, T4, T5, T6>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6>(command);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6)> Read<T1, T2, T3, T4, T5, T6>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6)> Read<T1, T2, T3, T4, T5, T6>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> Read<T1, T2, T3, T4, T5, T6, T7>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7>(command);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> Read<T1, T2, T3, T4, T5, T6, T7>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> Read<T1, T2, T3, T4, T5, T6, T7>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> Read<T1, T2, T3, T4, T5, T6, T7, T8>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8>(command);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> Read<T1, T2, T3, T4, T5, T6, T7, T8>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> Read<T1, T2, T3, T4, T5, T6, T7, T8>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(command);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8, T9>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this DbConnection connection, string command) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(command);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this DbConnection connection, string command, params object[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(command, parameters);

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this DbConnection connection, string command, params (string name, object value)[] parameters) =>
            connection.GetDataAccessInstance().Read<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(command, parameters);
    }
}