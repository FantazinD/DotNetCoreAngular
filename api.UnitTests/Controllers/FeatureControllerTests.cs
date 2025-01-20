using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Controllers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.UnitTests.Controllers
{
    public class FeatureControllerTests
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

        [Test]
        public void GetFeatures_WhenCalled_GetsFeaturesFromDatabase()
        {
            _featureController.GetFeatures().Wait();

            _featureRepository.Verify(fr => fr.GetFeaturesAsync());
        }

        [Test]
        public async Task GetFeatures_WhenCalled_ReturnsOkObjectResult()
        {
            var result = await _featureController.GetFeatures();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
