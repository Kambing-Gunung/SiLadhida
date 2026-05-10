using FluentAssertions;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;
using SiLadhida.Core.Services;

namespace SiLadhida.Tests.Services
{
    public class PesananServiceTests
    {
        private readonly PesananService _service;

        public PesananServiceTests()
        {
            _service = new PesananService();
        }

        [Fact]
        public void GetInitialStatus_ShouldReturnPesananTelahDibayar()
        {
            // Act
            var result = _service.GetInitialStatus();

            // Assert
            result.Should().Be(Status.PesananTelahDibayar);
        }

        [Fact]
        public void IsValidTransition_ShouldReturnTrue_WhenTransitionValid()
        {
            // Act
            var result = _service.IsValidTransition(
                Status.PesananTelahDibayar,
                Status.PesananDisiapkan
            );

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidTransition_ShouldReturnFalse_WhenTransitionInvalid()
        {
            // Act
            var result = _service.IsValidTransition(
                Status.PesananTelahDibayar,
                Status.PesananSelesai
            );

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void HitungTotal_ShouldReturnCorrectTotal()
        {
            // Arrange
            var items = new List<OrderItem>
            {
                new OrderItem
                {
                    Quantity = 2,
                    Harga = 10000
                },
                new OrderItem
                {
                    Quantity = 1,
                    Harga = 5000
                }
            };

            // Act
            var result = _service.HitungTotal(items);

            // Assert
            result.Should().Be(25000);
        }
    }
}