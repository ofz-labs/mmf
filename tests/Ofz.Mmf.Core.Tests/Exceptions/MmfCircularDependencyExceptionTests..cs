// =============================================================================
// MmfCircularDependencyExceptionTests
//
// Amaç: MmfCircularDependencyException'ın CyclePath property'sini doğru
//       sakladığını ve okunabilir hata mesajı ürettiğini doğrulamak.
//
// Yaklaşım: Arrange-Act-Assert deseniyle, CyclePath ataması, mesaj formatı,
//           null kontrolü ve kalıtım ilişkisi test edilir.
// =============================================================================
using AwesomeAssertions;
using Ofz.Mmf.Core.Exceptions;

namespace Ofz.Mmf.Core.Tests.Exceptions;

public class MmfCircularDependencyExceptionTests
{
    [Fact]
    public void Constructor_StoresCyclePath()
    {
        // Arrange
        var cyclePath = new List<Type> { typeof(string), typeof(int), typeof(string) };

        // Act
        var exception = new MmfCircularDependencyException(cyclePath);

        // Assert
        exception.CyclePath.Should().BeEquivalentTo(cyclePath);
    }

    [Fact]
    public void Constructor_BuildsReadableMessage()
    {
        // Arrange
        var cyclePath = new List<Type> { typeof(string), typeof(int), typeof(string) };

        // Act
        var exception = new MmfCircularDependencyException(cyclePath);

        // Assert — "String -> Int32 -> String" formatı beklenir
        exception.Message.Should().Be("Circular dependency detected: String -> Int32 -> String");
    }

    [Fact]
    public void Constructor_WithNullPath_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => new MmfCircularDependencyException(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void MmfCircularDependencyException_IsMmfException()
    {
        // Arrange
        var cyclePath = new List<Type> { typeof(string), typeof(int) };

        // Act
        var exception = new MmfCircularDependencyException(cyclePath);

        // Assert — MmfException'dan türer, dolayısıyla catch (MmfException) ile yakalanabilir
        exception.Should().BeAssignableTo<MmfException>();
    }
}
