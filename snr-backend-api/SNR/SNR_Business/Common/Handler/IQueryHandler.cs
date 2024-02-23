
namespace SNR_Business.Common.Handler
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Get(TQuery query);
    }
}
