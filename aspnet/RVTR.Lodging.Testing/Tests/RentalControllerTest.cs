using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Service.Controllers;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class CampsiteControllerTest
  {
    private readonly CampsiteController _controller;
    private readonly ILogger<CampsiteController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CampsiteControllerTest()
    {
      var loggerMock = new Mock<ILogger<CampsiteController>>();
      var repositoryMock = new Mock<IRepository<CampsiteModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<CampsiteModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<CampsiteModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(-1)).Throws(new KeyNotFoundException());
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((CampsiteModel)null);
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new CampsiteModel() { Id = 2, LotNumber = "2", Status = "Available", Price = 1.00 });
      repositoryMock.Setup(m => m.Update(It.IsAny<CampsiteModel>()));
      unitOfWorkMock.Setup(m => m.Campsite).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new CampsiteController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(-1);
      var resultPass = await _controller.Delete(2);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(-1);
      var resultOne = await _controller.Get(2);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new CampsiteModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      CampsiteModel campsitemodel = await _unitOfWork.Campsite.SelectAsync(2);

      var resultPass = await _controller.Put(campsitemodel);
      var resultFail = await _controller.Put(null);

      Assert.NotNull(resultPass);
      Assert.NotNull(resultFail);

      CampsiteModel campsiteModelBadId = await _unitOfWork.Campsite.SelectAsync(2);
      campsiteModelBadId.Id = -1;

      var resultFail2 = await _controller.Put(campsiteModelBadId);

      Assert.NotNull(resultFail2);
    }
  }
}
