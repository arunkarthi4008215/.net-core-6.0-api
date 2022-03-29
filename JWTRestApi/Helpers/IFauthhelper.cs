namespace JWTRestApi.Helpers
{
    public interface IFauthhelper
    {
        public String CreateToken(User user);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    }
}
