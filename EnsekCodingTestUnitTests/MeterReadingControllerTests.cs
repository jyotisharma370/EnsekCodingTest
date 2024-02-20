using EnsekCodingTest.Models;
using EnsekCodingTest.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using EnsekCodingTest.Interface;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace EnsekCodingTestUnitTests
{
    [TestFixture]
    public class MeterReadingControllerTestsS
    {
        private MeterReadingController _controller;
        private Mock<ILogger<MeterReadingController>> _loggerMock;
        private Mock<IMeterReadingRepository> _repoMock;
        private EnsekDbContext _dbContextMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<MeterReadingController>>();
            _repoMock = new Mock<IMeterReadingRepository>();
            var options = new DbContextOptionsBuilder<EnsekDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

            _dbContextMock = new EnsekDbContext(options);
            _mapperMock = new Mock<IMapper>();

            _controller = new MeterReadingController(_loggerMock.Object, _repoMock.Object, _dbContextMock, _mapperMock.Object);          
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the _dbContextMock after each test
            _dbContextMock.Dispose();
        }

        [Test]
        public void AddCsvFileToDb_ValidModelState_CallsRepoCheckMeterReadings()
        {
            // Arrange
            var formFileMock = new Mock<IFormFile>();
            _repoMock.Setup(repo => repo.CheckMeterReadings(It.IsAny<IFormFile>())).Returns(new string[] { "2", "1" });

            // Act
            var result = _controller.AddCsvFileToDb(formFileMock.Object);

            // Assert
            _repoMock.Verify(repo => repo.CheckMeterReadings(It.IsAny<IFormFile>()), Times.Once);
            Assert.IsInstanceOf<CreatedResult>(result);
        }

        [Test]
        public void AddCsvFileToDb_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("SomeKey", "Invalid model state");

            // Act
            var result = _controller.AddCsvFileToDb(null);

            // Assert
            _repoMock.Verify(repo => repo.CheckMeterReadings(It.IsAny<IFormFile>()), Times.Never);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        private List<MeterReading> GetMeterReadings()
        {
            return new List<MeterReading>
            {
                new MeterReading
                {
                   AccountId=2199,
                   TestAccount=new TestAccount{AccountId=2199,FirstName="John",LastName="Smith"},
                   MeterReadingDateTime=System.DateTime.Now,
                   MeterReadValue="19035"
                },
                 new MeterReading
                {
                   AccountId=1489,
                   TestAccount=new TestAccount{AccountId=2200,FirstName="Jack",LastName="Wills"},
                   MeterReadingDateTime=System.DateTime.Now,
                   MeterReadValue="50108"
                },
            };
        }

        private List<TestAccount> GetTestAccounts()
        {
            return new List<TestAccount>
            {
                new TestAccount() {AccountId=4321,FirstName="Tommy",LastName="Test" },
                new TestAccount() {AccountId=3128,FirstName="Barry",LastName="Test" },
                new TestAccount() {AccountId=1789,FirstName="Sally",LastName="Test" }
            };
        }
    }
}