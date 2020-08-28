﻿using System;
using Npgsql;
using Pgcode.Protos;

namespace Pgcode.Execution
{
    public static partial class ExecuteExtension
    {
        private static ExecuteReply GetHeaderReply(NpgsqlDataReader reader)
        {
            var headerReply = new ExecuteReply { RowNumber = 0 };
            for (var index = 0; index < reader.FieldCount; index++)
            {
                headerReply.Data.Add(
                    $"{{\"name\":\"{reader.GetName(index)}\",\"type\":\"{reader.GetDataTypeName(index)}\"}}");
            }
            return headerReply;
        }

        private static ExecuteReply GetRowReply(ulong row, NpgsqlDataReader reader)
        {
            var values = new object[reader.FieldCount];
            reader.GetProviderSpecificValues(values);

            var rowReply = new ExecuteReply { RowNumber = (uint)row };
            for (ulong index = 0; index < (uint)values.Length; index++)
            {
                var value = values[index];
                rowReply.Data.Add(value.ToString());
                if (value == DBNull.Value)
                {
                    rowReply.NullIndexes.Add((uint)index);
                }
            }
            return rowReply;
        }
    }
}