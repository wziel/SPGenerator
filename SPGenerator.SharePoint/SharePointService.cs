using Microsoft.SharePoint.Client;
using SPGenerator.Model;
using SPGenerator.Model.Column;
using SPGenerator.SharePoint.ColumnMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SPGenerator.SharePoint
{
    /// <summary>
    /// Class used for all communication with SharePoint.
    /// </summary>
    public class SharePointService : ISharePointService
    {
        private readonly ISharePointContextHelper contextHelper;
        private readonly IColumnMappingResolver columnMapping;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="httpContext">Http context which will be used to communicate
        /// with SharePoint.</param>
        public SharePointService(ISharePointContextHelper contextHelper, IColumnMappingResolver columnMapping)
        {
            this.contextHelper = contextHelper;
            this.columnMapping = columnMapping;
        }

        /// <summary>
        /// Url of SharePoint's site collection web url.
        /// </summary>
        public string HostWebUrl
        {
            get
            {
                using (var context = contextHelper.ClientContext)
                {
                    context.Load(context.Web);
                    context.ExecuteQuery();
                    return context.Web.Url;
                }
            }
        }

        /// <summary>
        /// All SharePoint Lists in site collection.
        /// </summary>
        public List<ListPOCO> AllListPOCO
        {
            get
            {
                using (var context = contextHelper.ClientContext)
                {
                    var lists = GetLists(list => !list.Hidden, context);
                    context.ExecuteQuery();
                    return TranslateToAppDomain(lists);
                }
            }
        }

        /// <summary>
        /// Used for fetching detailed information about a SharePoint list.
        /// </summary>
        /// <param name="listTitle">Title of the list to fetch.</param>
        /// <returns>Detailed information about a SharePoint list. Null if list not found.</returns>
        public ListPOCO GetListPOCO(string listTitle)
        {
            using (var context = contextHelper.ClientContext)
            {
                var list = GetList(listTitle, context);
                if (list == null)
                {
                    return null;
                }
                var fields = GetFields(list, context);
                context.ExecuteQuery();
                return TranslateToAppDomain(list, fields);
            }
        }

        /// <summary>
        /// Gets single list with specified title.
        /// </summary>
        /// <param name="listTitle">Title of the list to get.</param>
        /// <returns>sharePoint List</returns>
        private List GetList(string listTitle, ClientContext context)
        {
            var lists = GetLists(l => l.Title == listTitle && !l.Hidden, context);
            context.ExecuteQuery();
            return lists.FirstOrDefault();
        }

        /// <summary>
        /// Gets lists from context by given predicate.
        /// </summary>
        /// <param name="context">Context from which lists will be retrieved.</param>
        /// <param name="wherePredicate">Additional where predicate.</param>
        /// <returns>Lists for given predicate.</returns>
        private IEnumerable<List> GetLists(Expression<Func<List, bool>> wherePredicate, ClientContext context)
        {
            var listQuery = context.Web.Lists
                                .Select(list => list)
                                .Where(wherePredicate)
                                .Include(list => list.Title, list => list.DefaultViewUrl);
            return context.LoadQuery(listQuery);
        }

        /// <summary>
        /// Gets fields of specified list from context.
        /// </summary>
        /// <param name="spList">List which fields are to be returned.</param>
        /// <returns>Fields for specified list.</returns>
        private IEnumerable<Field> GetFields(List spList, ClientContext context)
        {
            var fieldsQuery = spList.Fields
                .Select(field => field)
                .Where(field => !field.FromBaseType || field.Required);
            return context.LoadQuery(fieldsQuery);
        }

        /// <summary>
        /// Translates IEnumerable of List to List of ListPOCO.
        /// </summary>
        /// <param name="lists">Lists to be translated.</param>
        /// <returns>Translation result.</returns>
        private List<ListPOCO> TranslateToAppDomain(IEnumerable<List> lists)
        {
            return lists.Select(list => new ListPOCO()
            {
                Title = list.Title,
                ServerRelativeUrl = list.DefaultViewUrl
            }).ToList();
        }

        /// <summary>
        /// Translates List with Fields to ListPOCO.
        /// </summary>
        /// <param name="spList">List to be translated.</param>
        /// <param name="fields">Fields of list to be translated.</param>
        /// <returns>Translation result.</returns>
        private ListPOCO TranslateToAppDomain(List spList, IEnumerable<Field> fields)
        {
            return new ListPOCO()
            {
                Title = spList.Title,
                ServerRelativeUrl = spList.DefaultViewUrl,
                ColumnPOCOList = fields.Select(field => columnMapping.Map(field)).ToList()
            };
        }

        /// <summary>
        /// Used for saving collection of entries to SharePoint list.
        /// </summary>
        /// <param name="entries">Collection of entries to save.</param>
        /// <param name="listPOCO">Target list for entries to be saved to.</param>
        public void Save(IEnumerable<EntryPOCO> entries, ListPOCO listPOCO)
        {
            if(entries.Any())
            {
                using (var context = contextHelper.ClientContext)
                {
                    var list = GetList(listPOCO.Title, context);
                    foreach (var entry in entries)
                    {
                        SaveEntry(entry, list, listPOCO);
                    }
                    context.ExecuteQuery();
                }
            }
        }

        /// <summary>
        /// Creates one entry in SP List.
        /// </summary>
        /// <param name="entry">entry to create</param>
        /// <param name="spList">list to which entry should be saved</param>
        /// <param name="listPOCO">poco model of the list</param>
        private void SaveEntry(EntryPOCO entry, List spList, ListPOCO listPOCO)
        {
            var item = spList.AddItem(new ListItemCreationInformation());
            foreach (var columnPOCO in listPOCO.ColumnPOCOList)
            {
                item[columnPOCO.InternalName] = entry.GetValue(columnPOCO);
            }
            item.Update();
        }
    }

    public interface ISharePointService
    {
        /// <summary>
        /// Url of SharePoint's site collection web url.
        /// </summary>
        string HostWebUrl { get; }

        /// <summary>
        /// All SharePoint Lists in site collection.
        /// </summary>
        List<ListPOCO> AllListPOCO { get; }

        /// <summary>
        /// Used for fetching detailed information about a SharePoint list.
        /// </summary>
        /// <param name="listTitle">Title of the list to fetch.</param>
        /// <returns>Detailed information about a SharePoint list. Null if list not found.</returns>
        ListPOCO GetListPOCO(string listTitle);

        /// <summary>
        /// Used for saving collection of entries to SharePoint list.
        /// </summary>
        /// <param name="entries">Collection of entries to save.</param>
        /// <param name="list">Target list for entries to be saved to.</param>
        void Save(IEnumerable<EntryPOCO> entries, ListPOCO list);
    }
}