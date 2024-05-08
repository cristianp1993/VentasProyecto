using MD5Hash;

namespace VentasProyect.Repository
{
    public class EncryptRepository
    {

        public string EncryptPassword(string password)
        {
            string passwordHash = password.GetMD5();

            return passwordHash;
        }      

    }
}