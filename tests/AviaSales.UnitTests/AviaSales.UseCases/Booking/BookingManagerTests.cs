using AviaSales.Core.Enums;
using AviaSales.External.Services.Interfaces;
using AviaSales.Persistence;
using AviaSales.UseCases.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AviaSales.UnitTests.AviaSales.UseCases.Booking;


public class BookingManagerTests
{
    [Fact]
    public async Task CreateBooking_ValidData_ReturnsSuccessResult()
    {
        // Arrange
        var dbMock = new Mock<AviaSalesDb>();
        var loggerMock = new Mock<ILogger<BookingManager>>();
        var fakeServiceMock = new Mock<IFakeService>();
        fakeServiceMock.Setup(s => s.CheckFlight(It.IsAny<long>())).ReturnsAsync(true);
        fakeServiceMock.Setup(s => s.BookFlight(It.IsAny<long>())).ReturnsAsync(true);

        var manager = new BookingManager(dbMock.Object, loggerMock.Object, fakeServiceMock.Object);
        var dto = new CreateBookingDto (1, 1, 100, EBookingStatus.Pending);

        // Act
        var result = await manager.CreateBooking(dto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(dto.FlightId, result.Data.FlightId);
        Assert.Equal(dto.PassengerId, result.Data.PassengerId);
        Assert.Equal(dto.TotalPrice, result.Data.TotalPrice);
        Assert.Equal(dto.Status, result.Data.Status);
    }

    [Fact]
    public async Task UpdateBooking_ExistingId_ReturnsSuccessResult()
    {
        // Arrange
        var dbMock = new Mock<AviaSalesDb>();
        var loggerMock = new Mock<ILogger<BookingManager>>();
        var fakeServiceMock = new Mock<IFakeService>();
        fakeServiceMock.Setup(s => s.UpdateBooking(It.IsAny<long>())).ReturnsAsync(true);

        var manager = new BookingManager(dbMock.Object, loggerMock.Object, fakeServiceMock.Object);
        var id = 1;
        var dto = new UpdateBookingDto (150,EBookingStatus.Confirmed);
    
        // Use the Create method to create an instance of Booking
        var booking = Core.Entities.Booking.Create(1, 1, 100, EBookingStatus.Pending);
        booking.Id = id;

        var data = new[] { booking }.AsQueryable();
        var dbSetMock = new Mock<DbSet<Core.Entities.Booking>>();
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.Provider).Returns(data.Provider);
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.Expression).Returns(data.Expression);
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.ElementType).Returns(data.ElementType);
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        dbMock.Setup(m => m.Bookings).Returns(dbSetMock.Object);

        // Act
        var result = await manager.UpdateBooking(id, dto);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(dto.TotalPrice, result.Data.TotalPrice);
        Assert.Equal(dto.Status, result.Data.Status);
    }

    [Fact]
    public async Task Delete_ExistingId_ReturnsSuccessResult()
    {
        // Arrange
        var dbMock = new Mock<AviaSalesDb>();
        var loggerMock = new Mock<ILogger<BookingManager>>();
        var fakeServiceMock = new Mock<IFakeService>();
        fakeServiceMock.Setup(s => s.DeleteBooking(It.IsAny<long>())).ReturnsAsync(true);

        var manager = new BookingManager(dbMock.Object, loggerMock.Object, fakeServiceMock.Object);
        var id = 1;
        var booking = Core.Entities.Booking.Create(1, 1, 100, EBookingStatus.Pending);
        var data = new[] { booking }.AsQueryable();
        var dbSetMock = new Mock<DbSet<Core.Entities.Booking>>();
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.Provider).Returns(data.Provider);
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.Expression).Returns(data.Expression);
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.ElementType).Returns(data.ElementType);
        dbSetMock.As<IQueryable<Core.Entities.Booking>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        dbMock.Setup(m => m.Bookings).Returns(dbSetMock.Object);

        // Act
        var result = await manager.Delete(id);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.True(result.Data);
    }
}