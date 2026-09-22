namespace Ofz.Mmf.Core.Exceptions;

/// <summary>
/// Modül bağımlılık grafiğinde döngüsel bir bağımlılık tespit edildiğinde fırlatılır.
/// </summary>
/// <remarks>
/// Örnek: Modül A, Modül B'ye bağlı; Modül B de Modül A'ya bağlı.
/// Bu durumda modül yükleme sırası belirlenemez, bu yüzden framework hata verir.
/// </remarks>
public sealed class MmfCircularDependencyException : MmfException
{
    /// <summary>
    /// Döngüyü oluşturan modül tipleri, sırayla.
    /// Örneğin <c>[A, B, C, A]</c> — yani A → B → C → A döngüsü.
    /// </summary>
    public IReadOnlyList<Type> CyclePath { get; }

    /// <summary>
    /// Belirtilen döngü yolu ile yeni bir <see cref="MmfCircularDependencyException"/> oluşturur.
    /// </summary>
    /// <param name="cyclePath">Döngüyü oluşturan modül tipleri. Son eleman, ilk elemanla aynı olmalıdır.</param>
    public MmfCircularDependencyException(IReadOnlyList<Type> cyclePath)
        : base(BuildMessage(cyclePath))
    {
        CyclePath = cyclePath;
    }

    private static string BuildMessage(IReadOnlyList<Type> cyclePath)
    {
        ArgumentNullException.ThrowIfNull(cyclePath);

        string path = string.Join(" -> ", cyclePath.Select(t => t.Name));
        return $"Circular dependency detected: {path}";
    }
}
