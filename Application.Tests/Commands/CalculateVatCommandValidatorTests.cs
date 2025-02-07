using Application.Commands;
using FluentValidation.TestHelper;

namespace Application.Tests.Commands;

[TestClass]
public class CalculateVatCommandValidatorTests
{
    private readonly CalculateVatCommandValidator _validator = new();

    [TestMethod]
    public void Validate_WhenVatRateIsInvalid_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CalculateVatCommand { VatRate = 15, NetAmount = 100 };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.VatRate)
            .WithErrorMessage("VAT rate must be a valid Austrian rate (10%, 13%, or 20%).");
    }

    [TestMethod]
    public void Validate_WhenNetAmountIsZero_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CalculateVatCommand { VatRate = 20, NetAmount = 0 };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.NetAmount)
            .WithErrorMessage("Net amount must be greater than 0.");
    }

    [TestMethod]
    public void Validate_WhenNoAmountIsProvided_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CalculateVatCommand { VatRate = 20 };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage("Exactly one amount (Net, Gross, or VAT) must be provided.");
    }

    [TestMethod]
    public void Validate_WhenValidRequest_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new CalculateVatCommand { VatRate = 20, NetAmount = 100 };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}