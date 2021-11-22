using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Tests
{
    public class TestDbSet<T> : DbSet<T>, IQueryable, IEnumerable<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }        

        public override T Add(T item)
        {
            _data.Add(item);
            return item;
        }
    }
}