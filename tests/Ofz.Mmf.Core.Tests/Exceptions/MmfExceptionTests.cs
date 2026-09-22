// =============================================================================
// MmfExceptionTests
//
// Amaç: MmfException sınıfının constructor'larının ve kalıtım ilişkisinin
//       doğru çalıştığını doğrulamak.
//
// Yaklaşım: Arrange-Act-Assert deseniyle, her constructor için ayrı test.
//           Inner exception'ın korunup korunmadığı da ayrıca test edilir.
// =============================================================================
using AwesomeAssertions;
using Ofz.Mmf.Core.Exceptions;

namespace Ofz.Mmf.Core.Tests.Exceptions;

public class MmfExceptionTests
{
    [Fact]
    public void ParameterlessConstructor_CreatesInstance()
    {
        // Act
        var exception = new MmfException();

        // Assert
        exception.Should().NotBeNull();
        exception.Message.Should().NotBeNullOrEmpty(); // .NET varsayılan mesaj verir
    }

    [Fact]
    public void MessageConstructor_SetsMessage()
    {
        // Arrange
        const string message = "Bir şeyler ters gitti";

        // Act
        var exception = new MmfException(message);

        // Assert
        exception.Message.Should().Be(message);
    }

    [Fact]
    public void InnerExceptionConstructor_PreservesBothMessageAndInner()
    {
        // Arrange
        const string message = "Üst seviye hata";
        var inner = new InvalidOperationException("Alt seviye hata");

        // Act
        var exception = new MmfException(message, inner);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().BeSameAs(inner);
    }

    [Fact]
    public void MmfException_IsCatchableAsBaseException()
    {
        // Arrange & Act
        var exception = new MmfException("test");

        // Assert — MmfException, System.Exception'dan türer
        exception.Should().BeAssignableTo<Exception>();
    }
}
