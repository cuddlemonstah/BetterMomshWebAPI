using BetterMomshWebAPI.Models;
using BetterMomshWebAPI.Models.JWT_Models;
using BetterMomshWebAPI.Models.Responses;
using BetterMomshWebAPI.Utils.Services;

namespace BetterMomshWebAPI.Utils
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly DbHelper _db;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, DbHelper db)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _db = db;
        }

        public async Task<AuthenticatedUserResponses> Authenticate(UserModel value)
        {
            var accesstoken = _accessTokenGenerator.Generate(value);
            var refreshtoken = _refreshTokenGenerator.GenerateToken();

            RefreshToken token = new()
            {
                Token = refreshtoken,
                Created = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(1880)
            };

            var id = await _db.getUserID(value.user_id);
            if (id != null)
            {
                bool updateResult = await _db.UpdateRefreshToken(id.user_id, token);

                if (updateResult)
                {
                    return new AuthenticatedUserResponses()
                    {
                        AccessToken = accesstoken,
                        RefreshToken = refreshtoken
                    };
                }
                else
                {
                    return null;
                }
            }

            // Handle scenarios where user ID retrieval fails
            // return appropriate response indicating the failure
            return null;
        }
    }
}
