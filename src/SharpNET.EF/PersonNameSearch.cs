using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpNET.EF
{
    public interface IPersonEntity
    {
        string FirstName { get; }
        string LastName { get; }
    }

    public class PersonNameSearch
    {
        private enum QueryType { Standard, FirstLast, LastFirst }

        public string Text { get; private set; }
        public string First { get; private set; }
        public string Last { get; private set; }

        private QueryType _queryType = QueryType.Standard;

        public PersonNameSearch(string text) //, Func<string> firstNameAccessor, Func<string> lastNameAccessor
        {
            Text = text;
            if (text.Contains(","))
            {
                var parts = text.Split(',');
                Last = parts[0].Trim();
                First = parts[1].Trim();
                _queryType = QueryType.LastFirst;
            }
            else if (text.Contains(" "))
            {
                var parts = text.Split(' ');
                First = parts[0].Trim();
                Last = parts[1].Trim();
                _queryType = QueryType.FirstLast;
            }
            else
            {
                First = text;
                Last = text;
                _queryType = QueryType.Standard;
            }
        }

        public IQueryable<TPersonEntity> AddToQuery<TPersonEntity>(IQueryable<TPersonEntity> query) where TPersonEntity : IPersonEntity
        {
            switch (_queryType)
            {
                case QueryType.FirstLast:
                    return query.Where(e => e.FirstName == First && e.LastName.Contains(Last));
                case QueryType.LastFirst:
                    return query.Where(e => e.FirstName.Contains(First) && e.LastName == Last);
                default:
                    return query.Where(e => e.FirstName.Contains(First) || e.LastName.Contains(Last));
            }
        }
    }
}
