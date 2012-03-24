using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CodeBase.Tests.Models
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private HashSet<T> _data;

        public FakeDbSet()
        {
            _data = new HashSet<T>();
        }


        private List<PropertyInfo> _keyProperties;

        public virtual T Find(params object[] keyValues)
        {
            if (keyValues.Length != _keyProperties.Count)
                throw new ArgumentException("Incorrect number of keys passed to find method");

            IQueryable<T> keyQuery = this.AsQueryable<T>();
            for (int i = 0; i < keyValues.Length; i++)
            {
                var x = i; // nested linq
                keyQuery = keyQuery
                   .Where(entity => _keyProperties[x].GetValue(entity, null).Equals(keyValues[x]));
            }

            return keyQuery.SingleOrDefault();
        }

        private void GetKeyProperties()
        {
            _keyProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                foreach (Attribute attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is KeyAttribute)
                    {
                        _keyProperties.Add(property);
                    }
                }
            }
        }



        private int _identity = 1;

        private void GenerateId(T entity)
        {
            // If non-composite integer key
            if (_keyProperties.Count == 1 && _keyProperties[0].PropertyType == typeof(Int32))
                _keyProperties[0].SetValue(entity, _identity++, null);
        }

        public T Add(T item)
        {
            GenerateId(item);
            _data.Add(item);
            return item;
        }



        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public void Detach(T item)
        {
            _data.Remove(item);
        }

        Type IQueryable.ElementType
        {
            get { return _data.AsQueryable().ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _data.AsQueryable().Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public ObservableCollection<T> Local
        {
            get
            {
                return new ObservableCollection<T>(_data);
            }
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }

}
