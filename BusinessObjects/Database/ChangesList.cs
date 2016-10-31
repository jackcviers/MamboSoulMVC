using System;
using System.Collections.Generic;

namespace BusinessObjects.Database
{
    /// <summary>
    /// public struct to hold a single changed field
    /// </summary>
    public class Change
    {
        /// <summary>field name</summary>
        public string FieldName;
        /// <summary>old value</summary>
        public string OldValue;
        /// <summary>new value</summary>
        public string NewValue;

        /// <summary></summary>
        public Change()
        {
        }

        /// <summary></summary>
        public Change(string fieldName)
        {
            FieldName = fieldName;
        }

        /// <summary></summary>
        public Change(string fieldName, string oldValue, string newValue)
        {
            FieldName = fieldName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    /// <summary>
    /// This class manages a list of changed fields.
    /// </summary>
    public class ChangesList
    {
        private readonly SortedList<string, Change> m_ChangeList = new SortedList<string, Change>();

        /// <summary>
        /// returns the list count
        /// </summary>
        public int Count
        {
            get
            {
                return this.m_ChangeList.Count;
            }
        }

        /// <summary>
        /// adds an object to the list
        /// </summary>
        public void Add(Change change)
        {
            if (change == null)
            {
                throw new ArgumentNullException();
            }

            m_ChangeList.Add(change.FieldName, change);
        }

        /// <summary>
        /// checks the list to see if a specific field exists
        /// </summary>
        /// <param name="fieldName">The fieldname is not case sensitive</param>
        /// <returns>bool true if fieldname found in the list</returns>
        public bool ItemExists(string fieldName)
        {
            bool result = false;
            result = m_ChangeList.ContainsKey(fieldName);
            return result;
        }

        /// <summary>
        /// returns a count of changes, excluding a list of field that are provided as parameters
        /// </summary>
        /// <param name="excludedFields"></param>
        /// <returns>count of changes found</returns>
        //public ChangesList GetChanges(params string[] excludedFields)
        //{
        //    ChangesList list = new ChangesList();

        //    // just to make sure, let's make sure case does not cause us problems
        //    for (int i = 0; i < excludedFields.Length; i++)
        //    {
        //        excludedFields[i] = excludedFields[i].ToLower();
        //    }

        //    // Now loop the changes and let's find any changes, except the ones that we are filtering
        //    foreach (Change change in m_ChangeList.Values)
        //    {
        //        if (change.OldValue != change.NewValue && !CommonUtility.InList(change.FieldName.ToLower(), excludedFields))
        //        {
        //            list.Add(change);
        //        }
        //    }

        //    return list;
        //}

        /// <summary>
        /// Returns the list of changes
        /// </summary>
        public IList<Change> Items
        {
            get
            {
                return m_ChangeList.Values;
            }
        }
    }
}