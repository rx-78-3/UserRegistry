namespace BuildingBlocks.Pagination;

public record PaginatedResult<TEntity>(long TotalCount, IEnumerable<TEntity> Data)
    where TEntity : class;
