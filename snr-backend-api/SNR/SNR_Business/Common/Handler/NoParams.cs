

namespace SNR_Business.Common.Handler
{
    public struct NoParams<TResult>: IQuery<TResult>
    {
        public static NoParams<TResult> Value = new NoParams<TResult>();
    }
}
