using System.Collections.Generic;

namespace StudiePlusPlus.Application.Abstractions.Mapping;

public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);
    IEnumerable<TDestination> Map(IEnumerable<TSource> source);
    void Update(TSource source, TDestination destination);
}
