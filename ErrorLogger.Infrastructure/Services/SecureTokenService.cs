using Microsoft.AspNetCore.DataProtection;

namespace ErrorLogger.Infrastructure.Services
{
    public class SecureTokenService
    {
        private readonly IDataProtector protector;

        public SecureTokenService(IDataProtectionProvider provider)
        {
            protector = provider.CreateProtector("TelegramTokenProtection");
        }

        public string Encrypt(string plainText)
        {
            return protector.Protect(plainText);
        }

        public string Decrypt(string encryptedText)
        {
            return protector.Unprotect(encryptedText);
        }
    }
}