using api.Controllers;
using api.Interfaces;
using api.Models;

namespace api.UnitTests;

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
}
