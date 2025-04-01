namespace systemeGAB.DataClass.Tools
{
    class CodePinTool
    {
        public static string HashCodePin(string codePin)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: codePin,
                salt: Encoding.UTF8.GetBytes("saltForPasswordHashing"),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

    }
}
