namespace Common.FrameWork.Domain.ApplicationServices
{
    public interface ICommandHandler<in TCommand>
    {
        void Handle(TCommand command);
    }
}
