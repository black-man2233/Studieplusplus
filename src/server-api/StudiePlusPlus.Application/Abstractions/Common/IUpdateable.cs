namespace StudiePlusPlus.Application.Abstractions.Common;

public interface IUpdateable<in TRequest>
{
    void Update(TRequest request);
}
