using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Domain.Entities;
using Infra.Interfaces;
using Moq;
using NotificationPattern.Interfaces;

namespace Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
[IterationCount(10)]
public class NotificationExceptionBenchmark
{
    private Mock<INotificationHandler> _notificationHandlerMock;
    private Mock<IPersonRepository> _personRepositoryMock;
    private NotificationPattern.Controllers.PersonController _personControllerNotification;
    private ExceptionProject.Controllers.PersonController _personControllerException;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personControllerNotification = new NotificationPattern.Controllers.PersonController(_notificationHandlerMock.Object, _personRepositoryMock.Object);
        _personControllerException = new ExceptionProject.Controllers.PersonController(_personRepositoryMock.Object);
    }

    [Benchmark]
    public async Task<bool> AddPersonAsync_Notification_Success()
    {
        var person = new Person()
        {
            Name = "random"
        };

        _notificationHandlerMock.Setup(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
        _personRepositoryMock.Setup(p => p.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(true);

        return await _personControllerNotification.AddPersonAsync(person);
    }

    [Benchmark]
    public async Task<bool> AddPersonAsync_Notification_AddNotification()
    {
        var person = new Person()
        {
            Name = new string('a', 60)
        };

        return await _personControllerNotification.AddPersonAsync(person);
    }

    [Benchmark]
    public async Task<bool> AddPersonAsync_Exception_Success()
    {
        var person = new Person()
        {
            Name = "random"
        };

        _personRepositoryMock.Setup(p => p.AddPersonAsync(It.IsAny<Person>())).ReturnsAsync(true);

        return await _personControllerException.AddPersonAsync(person);
    }

    [Benchmark]
    public async Task AddPersonAsync_Exception_ThrowException()
    {
        try
        {
            var person = new Person()
            {
                Name = new string('a', 60)
            };

            await _personControllerException.AddPersonAsync(person);
        }
        catch 
        {
        }
    }
}
