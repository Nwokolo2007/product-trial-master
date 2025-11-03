namespace AltenShop.Application.Common.Models
{
	public class PaginatedList<T>
	{
		public IReadOnlyList<T> Items { get; }
		public int TotalCount { get; }
		public int PageNumber { get; }
		public int PageSize { get; }
		public bool HasNext => PageNumber * PageSize < TotalCount;
		public bool HasPrevious => PageNumber > 1;

		public PaginatedList(IReadOnlyList<T> items, int totalCount, int pageNumber, int pageSize)
		{
			Items = items;
			TotalCount = totalCount;
			PageNumber = pageNumber;
			PageSize = pageSize;
		}
	}
}
