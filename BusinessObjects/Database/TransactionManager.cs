using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BusinessObjects.Database
{
    /// <summary>
    /// Helper class used to inlist table objects and list objects to perform a single 
    /// sql batch and a single transaction.
    /// </summary>
    public class TransactionManager
    {
        private readonly SqlHelper m_db;
        private readonly List<BaseSqlToCS> m_Items = new List<BaseSqlToCS>();

        /// <summary>
        /// default contructor with a connection
        /// </summary>
        public TransactionManager(SqlHelper db)
        {
            m_db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        public TransactionManager(SqlHelper db, params BaseSqlToCS[] items) : this(db)
        {
            m_Items.AddRange(items);
        }

        /// <summary>
        /// Adds the sql text and sql parameters from a table to this transaction
        /// </summary>
        public void Add(BaseSqlToCS table)
        {
            m_Items.Add(table);
        }

        /// <summary>
        /// Adds the sql text and sql parameters from a list to this transaction
        /// </summary>
        /// <param name="list"></param>
        public void Add(AbstractTableList list)
        {
            foreach (AbstractTable table in list.TableItems)
            {
                Add(table);
            }
        }

        /// <summary>
        /// Executes the sql and parameters as one transaction
        /// </summary>
        /// <returns></returns>
        public void Save()
        {
            if (m_Items.Count > 0)
            {
                SqlTransaction tx = null;
                try
                {
                    tx = m_db.BeginTransaction();
                    foreach (BaseSqlToCS item in m_Items)
                    {
                        item.Save(tx);
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    if (tx != null)
                        tx.Rollback();
                    throw ex;
                }
            }
        }
        //public void BatchSave()
        //{
        //    if (m_Items.Count > 0)
        //    {
        //        SqlTransaction tx = null;
        //        try
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            List<SqlParameter> param = new List<SqlParameter>();
        //            int context = 1;
        //            foreach (BaseSqlToCS item in m_Items)
        //            {
        //                if (item is AbstractTable)
        //                {
        //                    AbstractTable table = item as AbstractTable;
        //                    string sql1 = table.GenerateSql(context);
        //                    if (sql1.Length > 0)
        //                    {
        //                        sql.AppendLine(sql1);
        //                        param.AddRange(table.GenerateSqlParameters(context));
        //                        context++;
        //                    }
        //                }
        //                else if (item is AbstractTableList)
        //                {
        //                    foreach (AbstractTable table2 in ((AbstractTableList)(item)).TableItems)
        //                    {
        //                        string sql1 = table2.GenerateSql(context);
        //                        if (sql1.Length > 0)
        //                        {
        //                            sql.AppendLine(sql1);
        //                            param.AddRange(table2.GenerateSqlParameters(context));
        //                            context++;
        //                        }
        //                    }
        //                }
        //            }
        //            if (sql.Length > 0)
        //            {
        //                tx = m_db.BeginTransaction();
        //                int result = m_db.ExecuteNonQuery(tx, CommandType.Text, sql.ToString(), param.ToArray());
        //                tx.Commit();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            if (tx != null) tx.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
    }
}