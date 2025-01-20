using api.Controllers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.UnitTests.Controllers;

public class MakeControllerTests
{
    private Mock<IMakeRepository> _makeRepository;
    private MakeController _makeController;

    [SetUp]
    public void Setup()
    {
        _makeRepository = new Mock<IMakeRepository>();
        _makeRepository.Setup(mr => mr.GetMakesAsync(true)).ReturnsAsync(new List<Make>());

        _makeController = new MakeController(_makeRepository.Object);
    }

    [Test]
    public void GetMakes_WhenCalled_GetMakesFromDb()
    {
        _makeController.GetMakes().Wait();

        _makeRepository.Verify(mr => mr.GetMakesAsync(true));
    }

    [Test]
    public async Task GetMakes_WhenCalled_ReturnsOkObjectResult()
    {
        var result = await _makeController.GetMakes();

        Assert.That(result, Is.TypeOf<OkObjectResult>());
    }
}
