using System.Security;

namespace ViewModel.Base
{
    /// <summary>
    /// an interface for a class that can provide a secure
    /// </summary>
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
