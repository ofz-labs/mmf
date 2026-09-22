namespace Ofz.Mmf.Core.Exceptions;

/// <summary>
/// MMF framework'ü tarafından fırlatılan tüm exception'ların temel sınıfı.
/// Bu sınıfı miras alan exception'lar, framework'e özgü hataları temsil eder.
/// </summary>
/// <remarks>
/// Kullanıcılar <c>catch (MmfException ex)</c> yazarak framework'ten gelen
/// hataları, diğer .NET exception'larından ayırt edebilir.
/// </remarks>
public class MmfException : Exception
{
    /// <summary>
    /// Yeni bir <see cref="MmfException"/> oluşturur.
    /// </summary>
    public MmfException()
    {
    }

    /// <summary>
    /// Belirtilen hata mesajıyla yeni bir <see cref="MmfException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    public MmfException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Belirtilen hata mesajı ve iç exception ile yeni bir <see cref="MmfException"/> oluşturur.
    /// </summary>
    /// <param name="message">Hata mesajı.</param>
    /// <param name="innerException">Hataya neden olan iç exception.</param>
    public MmfException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
