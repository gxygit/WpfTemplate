using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Fasetto.Word.Core
{
    /// <summary>
    /// an interface for a class that can provide a secure
    /// </summary>
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
