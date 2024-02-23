

namespace SNR_Business.Common.Handler
{
    public interface ICommandHandler<in TCmd>
    {
        void Handle(TCmd cmd);
    }

    public interface ICommandHandler<in TCmd, out TResponse>
    {
        TResponse Handle(TCmd cmd);
    }

    public interface ICommandWithResponse<TResponse>
    {
    }
}
