using Microsoft.SharePoint.Client;
using SPGenerator.Model;
using SPGenerator.Model.Column;
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
    public class SharePointService
    {
        private readonly SharePointContextHelper contextHelper;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="httpContext">Http context which will be used to communicate
        /// with SharePoint.</param>
        public SharePointService(SharePointContextHelper contextHelper)
        {
            this.contextHelper = contextHelper;
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
        public List<SPGList> AllSPGLists
        {
            get
            {
                var lists = GetLists(list => !list.Hidden);
                return TranslateToAppDomain(lists);
            }
        }

        /// <summary>
        /// Used for fetching detailed information about a SharePoint list.
        /// </summary>
        /// <param name="listTitle">Title of the list to fetch.</param>
        /// <returns>Detailed information about a SharePoint list. Null if list not found.</returns>
        public SPGList GetSPGList(string listTitle)
        {
            var lists = GetLists(l => l.Title == listTitle && !l.Hidden);
            var list = lists.FirstOrDefault();
            if (list == null)
            {
                return null;
            }
            var fields = GetFields(list);
            return TranslateToAppDomain(list, fields);
        }

        /// <summary>
        /// Translates IEnumerable of List to List of SPGList.
        /// </summary>
        /// <param name="lists">Lists to be translated.</param>
        /// <returns>Translation result.</returns>
        private static List<SPGList> TranslateToAppDomain(IEnumerable<List> lists)
        {
            return lists.Select(list => new SPGList()
            {
                Title = list.Title,
                ServerRelativeUrl = list.DefaultViewUrl
            }).ToList();
        }

        /// <summary>
        /// Translates List with Fields to SPGList.
        /// </summary>
        /// <param name="spList">List to be translated.</param>
        /// <param name="fields">Fields of list to be translated.</param>
        /// <returns>Translation result.</returns>
        private static SPGList TranslateToAppDomain(List spList, IEnumerable<Field> fields)
        {
            return new SPGList()
            {
                Title = spList.Title,
                ServerRelativeUrl = spList.DefaultViewUrl,
                SPGColumns = fields.Select(field => new SPGColumn()
                {
                    ColumnName = field.Title
                }).ToList()
            };
        }

        /// <summary>
        /// Gets lists from context by given predicate.
        /// </summary>
        /// <param name="context">Context from which lists will be retrieved.</param>
        /// <param name="wherePredicate">Additional where predicate.</param>
        /// <returns>Lists for given predicate.</returns>
        private IEnumerable<List> GetLists(Expression<Func<List, bool>> wherePredicate)
        {
            using (var context = contextHelper.ClientContext)
            {
                var listQuery = context.Web.Lists
                                    .Select(list => list)
                                    .Where(wherePredicate)
                                    .Include(list => list.Title, list => list.DefaultViewUrl);
                return GetByQuery(listQuery, context);
            }
        }

        /// <summary>
        /// Gets fields of specified list from context.
        /// </summary>
        /// <param name="spList">List which fields are to be returned.</param>
        /// <returns>Fields for specified list.</returns>
        private IEnumerable<Field> GetFields(List spList)
        {
            using (var context = contextHelper.ClientContext)
            {
                var fieldsQuery = spList.Fields
                    .Select(field => field)
                    .Where(field => !field.FromBaseType || field.Required);
                return GetByQuery(fieldsQuery, context);
            }
        }

        /// <summary>
        /// Gets SharePoint client objects by specified query
        /// </summary>
        /// <typeparam name="T">Type of objects to retrieve.</typeparam>
        /// <param name="query">Query for objects.</param>
        /// <param name="context">Context from which objects will be retrieved.</param>
        /// <returns>Objects that fit the query.</returns>
        private IEnumerable<T> GetByQuery<T>(IQueryable<T> query, ClientContext context) where T : ClientObject
        {
            var enumerable = context.LoadQuery(query);
            context.ExecuteQuery();
            return enumerable;
        }

        /// <summary>
        /// Used for saving collection of entries to SharePoint list.
        /// </summary>
        /// <param name="entries">Collection of entries to save.</param>
        /// <param name="list">Target list for entries to be saved to.</param>
        public void Save(IEnumerable<SPGEntry> entries, SPGList list)
        {
            throw new NotImplementedException();
        }
    }
}