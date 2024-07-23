namespace Course.Catalog.Service.Api.Models.Pagination;

public class PagedList<T>
{
    public List<T> Items { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious { get; private set; }
    public bool HasNext { get; private set; }

    public PagedList(List<T> items, int pageNumber, int pageSize)
    {
        Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        TotalCount = items.Count;
        TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
        PageSize = pageSize;
        CurrentPage = pageNumber;
        HasPrevious = CurrentPage > 1;
        HasNext = CurrentPage < TotalPages;
    }
    public PagedList()
    {
        Items = [];
    }
}

