using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Domain.Entities;
using Domain.Interfaces.Validators;
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
    private Mock<IPersonValidator> _personValidatorMock;
    private NotificationPattern.Controllers.PersonController _personControllerNotification;
    private ExceptionProject.Controllers.PersonController _personControllerException;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personValidatorMock = new Mock<IPersonValidator>();
        _personControllerNotification = new NotificationPattern.Controllers.PersonController(_notificationHandlerMock.Object, _personRepositoryMock.Object, 
            _personValidatorMock.Object);
        _personControllerException = new ExceptionProject.Controllers.PersonController(_personRepositoryMock.Object, 
            _personValidatorMock.Object);
    }

    [Benchmark]
    public async Task<bool> AddPersonAsync_Notification_Success()
    {
        var person = new Person()
        {
            Name = "random"
        };

        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(true);
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

        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(false);

        return await _personControllerNotification.AddPersonAsync(person);
    }

    [Benchmark]
    public async Task<bool> AddPersonAsync_Exception_Success()
    {
        var person = new Person()
        {
            Name = "random"
        };

        _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(true);
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

            _personValidatorMock.Setup(p => p.IsPersonValid(It.IsAny<Person>())).Returns(false);

            await _personControllerException.AddPersonAsync(person);
        }
        catch 
        {
        }
    }
}
