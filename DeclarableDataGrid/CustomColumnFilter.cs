using System;

namespace DeclarableDataGrid
{
    internal sealed class CustomColumnFilter : IColumnFilter
    {
        private readonly Func<DeclarableDataGridColumn, bool> _filter;

        public CustomColumnFilter(Func<DeclarableDataGridColumn, bool> filter)
        {
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public bool MatchColumn(DeclarableDataGridColumn declarableDataGridColumn)
        {
            return _filter.Invoke(declarableDataGridColumn);
        }
    }
}