namespace systemeGAB.DataClass.Tools
{
    public static class TokenTool
    {
        public static string GenerateJwt(CarteBancaire carteBancaire, JwtSettings jwtSettings)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, carteBancaire.idCarte.ToString()),
            new Claim(ClaimTypes.Name, (carteBancaire.numeroCarte!))
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.ExpirationInMinutes));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
