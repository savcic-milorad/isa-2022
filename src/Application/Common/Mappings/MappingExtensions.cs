using TransfusionAPI.Application.Common.Models;

namespace TransfusionAPI.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListOrderedAsync<TDestination>(this IOrderedQueryable<TDestination> source, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(source, pageNumber, pageSize);

    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> source, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(source, pageNumber, pageSize);

    public static PaginatedList<TDestination> PaginatedListMap<TSource,TDestination>(this PaginatedList<TSource> source, Func<TSource, TDestination> MapSourceToDestination) 
        where TDestination : class 
        where TSource : class => new PaginatedList<TDestination>(source.Items.Select(MapSourceToDestination).ToList(), source.TotalCount, source.PageNumber, source.PageSize);
}
