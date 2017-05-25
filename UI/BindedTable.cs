using System;
using System.Collections.Generic;
using System.Linq;

namespace UI
{
    public class BindedTable<T>
    {
        public int PagesCount
        {
            get
            {
                if (_usePagination)
                {
                    return _data.Count() / _pageSize + (_data.Count() % _pageSize == 0 ? 0 : 1);
                }
                else
                    return _data.Count() == 0 ? 0 : 1;
            }
        }

        private IEnumerable<T> _data;
        private Func<T, Tuple<string, char, string>> _formatter;

        private bool _usePagination = false;
        private TablePage<T> _currentPage;
        private int _pageSize;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">Data source to bind</param>
        /// <param name="formatter">Entries format template with column names, Tuple.Item1 - column name, Tuple.Item2 - column separator, Tuple.Item3 - column format</param>
        public BindedTable(IEnumerable<T> source, Func<T, Tuple<string, char, string>> formatter)
        {
            _data = source;
            _formatter = formatter;
        }

        public void UsePageination(int pageSize = 3)
        {
            _pageSize = pageSize;
            _usePagination = true;
            _currentPage = new TablePage<T>(1, _data.Take(_pageSize));
        }

        public void NextPage()
        {
            if (_usePagination)
            {
                if (_currentPage.Id == PagesCount)
                {
                    GoToPage(1);
                }
                else
                {
                    GoToPage(++_currentPage.Id);
                }
            }
        }

        public void PreviousPage()
        {
            if (_usePagination)
            {
                if (_currentPage.Id == 1)
                {
                    GoToPage(PagesCount);
                }
                else
                {
                    GoToPage(--_currentPage.Id);
                }
            }
        }

        public void GoToPage(int id)
        {
            if (_usePagination)
            {
                if (id <= PagesCount && id > 0)
                {
                    _currentPage.Id = id;
                    _currentPage.Items = _data
                        .Skip(_pageSize * (_currentPage.Id - 1))
                        .Take(_pageSize);
                }
            }
        }

        public void WriteTable()
        {
            FormatedTable formatedTable = new FormatedTable();
            bool isHead = true;

            foreach (var item in _data)
            {
                Tuple<string, char, string> entry = _formatter(item);
                
                if (isHead)
                {
                    formatedTable = new FormatedTable(entry.Item1.Split(entry.Item2));
                    isHead = false;
                }

                formatedTable.AddEntry(entry.Item3.Split(entry.Item2));
            }
            formatedTable.WriteHead();
            formatedTable.WriteEntries();
        }
    }

    internal class TablePage<T>
    {
        /// <summary>
        /// Page number, startrd from 1
        /// </summary>
        public int Id { get; set; }
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Page number (startrd from 1)</param>
        /// <param name="items">Page items</param>
        public TablePage(int id, IEnumerable<T> items)
        {
            Id = id;
            Items = items;
        }

        /// <summary>
        /// Return new empty TablePage with number
        /// </summary>
        /// <param name="id">Page number (startrd from 1)</param>
        public TablePage(int id)
        {
            Id = id;
            Items = new List<T>();
        }
    }
}
