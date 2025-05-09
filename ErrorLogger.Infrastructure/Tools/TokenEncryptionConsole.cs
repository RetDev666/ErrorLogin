using Microsoft.AspNetCore.DataProtection;

namespace ErrorLogger.Infrastructure.Tools
{
    public static class TokenEncryptionHelper
    {
        public static string GenerateEncryptedToken(string plainToken)
        {
            var provider = DataProtectionProvider.Create("TelegramTokenProtection");
            var protector = provider.CreateProtector("TelegramTokenProtection");
            return protector.Protect(plainToken);
        }
    }
}