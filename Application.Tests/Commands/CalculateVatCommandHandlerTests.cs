using Application.Commands;

namespace Application.Tests.Commands;

[TestClass]
public class CalculateVatCommandHandlerTests
{
    private readonly CalculateVatCommandHandler _handler = new();

    [TestMethod]
    public async Task Handle_WhenNetAmountIsProvided_ShouldCalculateCorrectly()
    {
        // Arrange
        var command = new CalculateVatCommand { NetAmount = 100, VatRate = 20 };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual(100, result.NetAmount);
        Assert.AreEqual(120, result.GrossAmount);
        Assert.AreEqual(20, result.VatAmount);
    }

    [TestMethod]
    public async Task Handle_WhenGrossAmountIsProvided_ShouldCalculateCorrectly()
    {
        // Arrange
        var command = new CalculateVatCommand { GrossAmount = 120, VatRate = 20 };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual(100, result.NetAmount);
        Assert.AreEqual(120, result.GrossAmount);
        Assert.AreEqual(20, result.VatAmount);
    }

    [TestMethod]
    public async Task Handle_WhenVatAmountIsProvided_ShouldCalculateCorrectly()
    {
        // Arrange
        var command = new CalculateVatCommand { VatAmount = 20, VatRate = 20 };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual(100, result.NetAmount);
        Assert.AreEqual(120, result.GrossAmount);
        Assert.AreEqual(20, result.VatAmount);
    }

    [TestMethod]
    public async Task Handle_WhenNoAmountIsProvided_ShouldReturnZeroValues()
    {
        // Arrange
        var command = new CalculateVatCommand { VatRate = 20 };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.AreEqual(0, result.NetAmount);
        Assert.AreEqual(0, result.GrossAmount);
        Assert.AreEqual(0, result.VatAmount);
    }
}