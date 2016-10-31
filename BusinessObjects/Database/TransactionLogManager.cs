using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;

namespace BusinessObjects.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionLogManager
    {
        private readonly SqlHelper m_db;
        private int? m_InstanceId = null;
        private readonly List<BaseSqlToCS> m_Tables = new List<BaseSqlToCS>();
        private int m_TransactionId = -1;
        CdmTransactionLogData m_TransactionLog = null;

        /// <summary>
        /// default contructor with a connection
        /// </summary>
        public TransactionLogManager(SqlHelper db)
        {
            m_db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        public TransactionLogManager(SqlHelper db, params BaseSqlToCS[] items) : this(db)
        {
            m_Tables.AddRange(items);
        }

        /// <summary>
        /// 
        /// </summary>
        public static string UserContext
        {
            get
            {
                if (System.Threading.Thread.CurrentPrincipal.Identity.Name.Length > 0)
                {
                    return Thread.CurrentPrincipal.Identity.Name;
                }
                else
                {
                    return WindowsIdentity.GetCurrent().Name;
                }
            }
        }

        /// <summary>
        /// Adds the sql text and sql parameters from a table to this transaction
        /// </summary>
        public void Add(BaseSqlToCS table)
        {
            m_Tables.Add(table);
        }

        /// <summary>
        /// Adds the sql text and sql parameters from a list to this transaction
        /// </summary>
        /// <param name="list"></param>
        public void Add(AbstractTableList list)
        {
            m_Tables.Add(list);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Syncronize()
        {
            foreach (object item in m_Tables)
            {
                if (item is BaseSqlToCS && item is ITransactionLog)
                {
                    SyncronizeTable((BaseSqlToCS)item);
                }
                else if (item is AbstractTableList)
                {
                    AbstractTableList list = item as AbstractTableList;
                    foreach (AbstractTable table in list.TableItems)
                    {
                        SyncronizeTable(table);
                    }
                }
            }
        }

        /// <summary>
        /// InstanceId that will be logged to cdm_transaction_log
        /// </summary>
        public int? InstanceId
        {
            get
            {
                return m_InstanceId;
            }
            set
            {
                m_InstanceId = value;
            }
        }

        private int TransactionId
        {
            get
            {
                if (m_TransactionId < 0)
                {
                    CdmTransactionLog tl = new CdmTransactionLog(m_db);
                    tl.UserName = TransactionLogManager.UserContext;
                    tl.InstanceId = InstanceId;
                    tl.Save();
                    m_TransactionId = tl.TransactionId.GetValueOrDefault();
                    m_TransactionLog = tl.GetCurrentValues();
                }
                return m_TransactionId;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CdmTransactionLogData TransactionLog
        {
            get
            {
                return m_TransactionLog;
            }
        }

        private void SyncronizeTable(BaseSqlToCS table)
        {
            if (table.Mode != EditMode.None)
            {
                ITransactionLog create = table as ITransactionLog;
                if (create != null)
                    create.TransactionId = TransactionId;
            }
        }
    }
}