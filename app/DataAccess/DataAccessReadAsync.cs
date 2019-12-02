﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Pgcode.DataAccess.Extensions;

namespace Pgcode.DataAccess
{
    public partial class DataAccess
    {
        public IAsyncEnumerable<IList<(string name, object value)>> ReadAsync(string command) =>
            ReadInternalAsync(command, r => r.ToList());

        public IAsyncEnumerable<IList<(string name, object value)>> ReadAsync(string command,
            params object[] parameters) =>
            ReadInternalAsync(command, r => r.ToList(), cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<IList<(string name, object value)>> ReadAsync(string command,
            params (string name, object value)[] parameters) =>
            ReadInternalAsync(command, r => r.ToList(), cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<T> ReadAsync<T>(string command) =>
            ReadInternalAsync(command,
                async r => await GetFieldValueAsync<T>(r, 0));

        public IAsyncEnumerable<T> ReadAsync<T>(string command, params object[] parameters) =>
            ReadInternalAsync(command,
                async r => await GetFieldValueAsync<T>(r, 0),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<T> ReadAsync<T>(string command, params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => await GetFieldValueAsync<T>(r, 0),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2)> ReadAsync<T1, T2>(string command) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1)));

        public IAsyncEnumerable<(T1, T2)> ReadAsync<T1, T2>(string command, params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2)> ReadAsync<T1, T2>(string command,
            params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3)> ReadAsync<T1, T2, T3>(string command) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2)));

        public IAsyncEnumerable<(T1, T2, T3)> ReadAsync<T1, T2, T3>(string command, params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3)> ReadAsync<T1, T2, T3>(string command,
            params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4)> ReadAsync<T1, T2, T3, T4>(string command) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2), await GetFieldValueAsync<T4>(r, 3)));

        public IAsyncEnumerable<(T1, T2, T3, T4)>
            ReadAsync<T1, T2, T3, T4>(string command, params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2), await GetFieldValueAsync<T4>(r, 3)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4)> ReadAsync<T1, T2, T3, T4>(string command,
            params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2), await GetFieldValueAsync<T4>(r, 3)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5)> ReadAsync<T1, T2, T3, T4, T5>(string command) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2), await GetFieldValueAsync<T4>(r, 3),
                    await GetFieldValueAsync<T5>(r, 4)));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5)> ReadAsync<T1, T2, T3, T4, T5>(string command,
            params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2), await GetFieldValueAsync<T4>(r, 3),
                    await GetFieldValueAsync<T5>(r, 4)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5)> ReadAsync<T1, T2, T3, T4, T5>(string command,
            params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2), await GetFieldValueAsync<T4>(r, 3),
                    await GetFieldValueAsync<T5>(r, 4)),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6)> ReadAsync<T1, T2, T3, T4, T5, T6>(string command) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5)
                ));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6)> ReadAsync<T1, T2, T3, T4, T5, T6>(string command,
            params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6)> ReadAsync<T1, T2, T3, T4, T5, T6>(string command,
            params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7)> ReadAsync<T1, T2, T3, T4, T5, T6, T7>(string command) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6)
                ));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7)> ReadAsync<T1, T2, T3, T4, T5, T6, T7>(string command,
            params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7)> ReadAsync<T1, T2, T3, T4, T5, T6, T7>(string command,
            params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
            string command) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7)
                ));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
            string command, params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
            string command, params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            string command) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7),
                    await GetFieldValueAsync<T9>(r, 8)
                ));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            string command, params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7),
                    await GetFieldValueAsync<T9>(r, 8)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            string command, params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7),
                    await GetFieldValueAsync<T9>(r, 8)
                ),
                cmd => cmd.AddParameters(parameters));


        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9,
            T10>(string command) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7),
                    await GetFieldValueAsync<T9>(r, 8),
                    await GetFieldValueAsync<T10>(r, 9)
                ));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9,
            T10>(string command, params object[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7),
                    await GetFieldValueAsync<T9>(r, 8),
                    await GetFieldValueAsync<T10>(r, 9)
                ),
                cmd => cmd.AddParameters(parameters));

        public IAsyncEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> ReadAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9,
            T10>(string command, params (string name, object value)[] parameters) =>
            ReadInternalAsync(command,
                async r => (
                    await GetFieldValueAsync<T1>(r, 0), await GetFieldValueAsync<T2>(r, 1),
                    await GetFieldValueAsync<T3>(r, 2),
                    await GetFieldValueAsync<T4>(r, 3), await GetFieldValueAsync<T5>(r, 4),
                    await GetFieldValueAsync<T6>(r, 5),
                    await GetFieldValueAsync<T7>(r, 6), await GetFieldValueAsync<T8>(r, 7),
                    await GetFieldValueAsync<T9>(r, 8),
                    await GetFieldValueAsync<T10>(r, 9)
                ),
                cmd => cmd.AddParameters(parameters));


        private async IAsyncEnumerable<T> ReadInternalAsync<T>(string command, Func<DbDataReader, T> readerAction,
            Action<DbCommand> commandAction = null)
        {
            await using var cmd = Connection.CreateCommand();
            SetCommand(cmd, command);
            await Connection.EnsureIsOpenAsync();
            commandAction?.Invoke(cmd);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                yield return readerAction(reader);
            }
        }

        private async IAsyncEnumerable<T> ReadInternalAsync<T>(string command, Func<DbDataReader, Task<T>> readerAction,
            Action<DbCommand> commandAction = null)
        {
            await using var cmd = Connection.CreateCommand();
            SetCommand(cmd, command);
            await Connection.EnsureIsOpenAsync();
            commandAction?.Invoke(cmd);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                yield return await readerAction(reader);
            }
        }
    }
}