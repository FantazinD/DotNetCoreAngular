using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Controllers;
using api.Interfaces;

namespace api.UnitTests.Controllers
{
    public class VehicleControllerTests
    {
        private Mock<IVehicleRepository> _vehicleRepository;
        private Mock<IUnitOfWorkRepository> _unitOfWorkRepository;
        private VehicleController _vehicleController;

        [SetUp]
        public void SetUp()
        {
            _vehicleRepository = new Mock<IVehicleRepository>();
            _unitOfWorkRepository = new Mock<IUnitOfWorkRepository>();

            _vehicleController = new VehicleController(_vehicleRepository.Object, _unitOfWorkRepository.Object);
        }
    }
}
