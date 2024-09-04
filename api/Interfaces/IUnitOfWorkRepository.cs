namespace api.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        Task CompleteAsync();
    }
}