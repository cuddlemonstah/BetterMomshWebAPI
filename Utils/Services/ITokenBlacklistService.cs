namespace BetterMomshWebAPI.Models.Database_Repository
{
    public interface ITokenBlacklistService
    {
        Task AddToBlacklistAsync(string token);
        Task<bool> IsTokenBlacklistedAsync(string token);
    }

}
