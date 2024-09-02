namespace Task2Core.DTOfolder
{
    public class PasswordHasher
    {
        //function Register
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // The Key property provides a randomly generated salt.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        // function Login
        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHash1 = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash1.SequenceEqual(passwordHash);
            }
        }


    }
}
