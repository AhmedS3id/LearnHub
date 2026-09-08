namespace LearnHub_Api.Abstractions
{
    public class PaginatedList<T>(List<T> items, int pageNumber, int count, int pageSize)
    {
        public List<T> Items { get; private set; } = items;
        public int PageNumber { get; set; } = pageNumber;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPages => PageNumber > 1;

        public static async Task<PaginatedList<T>>CreateAsync(IQueryable<T> query,int pageNumber, int pageSize)
        {
            var count =await query.CountAsync();
            var items= await query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

            return  new PaginatedList<T>(items,pageNumber,count,pageSize);
        }

    }
}
