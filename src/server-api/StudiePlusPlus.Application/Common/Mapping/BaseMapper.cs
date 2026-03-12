using System.Collections.Generic;
using System.Linq;
using StudiePlusPlus.Application.Abstractions.Mapping;

namespace StudiePlusPlus.Application.Common.Mapping;

public abstract class BaseMapper<TSource, TDestination> : IMapper<TSource, TDestination>
{
    public abstract TDestination Map(TSource source);

    public virtual IEnumerable<TDestination> Map(IEnumerable<TSource> source)
    {
        return source?.Select(Map) ?? Enumerable.Empty<TDestination>();
    }

    public abstract void Update(TSource source, TDestination destination);
}
