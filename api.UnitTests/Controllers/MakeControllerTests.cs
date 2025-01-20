using api.Controllers;
using api.Interfaces;
using api.Models;

namespace api.UnitTests;

public class MakeControllerTests
{
    private Mock<IFeatureRepository> _featureRepository;
    private FeatureController _featureController;

    [SetUp]
    public void Setup()
    {
        _featureRepository = new Mock<IFeatureRepository>();
        _featureRepository.Setup(fr => fr.GetFeaturesAsync()).ReturnsAsync(new List<Feature>());

        _featureController = new FeatureController(_featureRepository.Object);
    }
}
