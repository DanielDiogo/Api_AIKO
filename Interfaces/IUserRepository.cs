namespace Api_AIKO.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> DoesUserIdExist(string userId);
    }
}
