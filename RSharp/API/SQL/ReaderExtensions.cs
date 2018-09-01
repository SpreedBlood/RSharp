﻿using System.Data.Common;

namespace RSharp.API.SQL
{
    public static class ReaderExtensions
    {
        public static T ReadData<T>(this DbDataReader reader, string columnName) =>
            (T)reader[columnName];
    }
}
