﻿using System.Collections.Generic;
using System.Data;

namespace MyLib.Data.Common
{
    public abstract class QueryAdapterBase
    {
        public abstract ParameterListBase CreateParameterList();
        public abstract ParameterListBase CreateParameterList<T>(string name, T t);

        public abstract IDataReader GetDataReader(
            QueryBase query
        );

        public abstract int Execute(
            QueryBase query
        );

        public abstract T Get<T>(
            QueryBase query
        );

        //public abstract void BulkCopy(
        //	DataRow[] rows
        //	, string table
        //	, int numFields
        //	, bool deleteRecords = true
        //);

        //public abstract void BulkCopy(
        //	DataRow[] rows
        //	, string table
        //	, int numFields
        //	, IDbTransaction trans
        //	, bool deleteRecords = true
        //);

        public abstract void BulkCopy(
            IDataReader reader
            , string table
            , int numFields
            , bool deleteRecords = true
        );

        public abstract void BulkCopy(
            IDataReader reader
            , string table
            , int numFields
            , IDbTransaction trans
            , bool deleteRecords = true
        );

        public abstract void BulkCopy(
            DataTable dt
            , string table
            , int numFields
            , bool deleteRecords = true
        );

        public abstract void BulkCopy(
            DataTable dt
            , string table
            , int numFields
            , IDbTransaction trans
            , bool deleteRecords = true
        );

        public abstract QueryBase CreateQueryGet(
            string tableName
            , string keyName
            , object keyValue
        );

        public abstract QueryBase CreateQuerySelect(
            string tableName
            , ListFilter listFilter
            , OrderList orderList = null
        );

        public abstract QueryBase CreateQuerySelect(
            string tableName
        );

        public abstract QueryBase CreateQueryUpdate(
            string tableName
            , List<IField> fields
            , IDbTransaction transaction
        );

        public abstract QueryBase CreateQueryInsert(
            string tableName
            , List<IField> fields
        );

        public abstract QueryBase CreateQueryDelete(
            string tableName
            , string keyName
        );
    }
}
