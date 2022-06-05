namespace DeclarableDataGrid
{
    public interface IColumnFilter
    {
        bool MatchColumn(DeclarableDataGridColumn declarableDataGridColumn);
    }
}