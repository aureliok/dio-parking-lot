using ASPNETCoreBackend.Controllers;
using ASPNETCoreBackend.Entities;
using ASPNETCoreBackend.Models;
using ASPNETCoreBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace ASPNETCoreBackend.Tests.Controllers
{
    public class ParkingLotControllerTests
    {
        private readonly Mock<IParkingLotManager> _manager;
        private readonly ParkingLotController _controller;

        public ParkingLotControllerTests()
        {
            _manager = new Mock<IParkingLotManager>();
            _controller = new ParkingLotController(_manager.Object);
        }

        [Fact]
        public void NewParkingLot_ReturnsOk()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark"
            };
            _manager.Setup(m => m.AddParkingLot(model));

            // Act
            IActionResult result = _controller.NewParkingLot(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void NewParkingLot_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark"
            };
            _manager.Setup(m => m.AddParkingLot(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.NewParkingLot(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void NewClient_ReturnsOk()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe"
            };
            _manager.Setup(m => m.AddClient(model));

            // Act
            IActionResult result = _controller.NewClient(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void NewClient_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe"
            };
            _manager.Setup(m => m.AddClient(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.NewClient(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void NewVehicle_ReturnsOk()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123"
            };
            _manager.Setup(m => m.AddVehicle(model));

            // Act
            IActionResult result = _controller.NewVehicle(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void NewVehicle_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123"
            };
            _manager.Setup(m => m.AddVehicle(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.NewVehicle(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void NewActivity_ReturnsOk()
        {
            // Arrange
            ParkingLotActivityModel model = new ParkingLotActivityModel
            {
                ParkingLotActivityId = 1
            };
            _manager.Setup(m => m.AddParkingLotActivity(model));

            // Act
            IActionResult result = _controller.NewActivity(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void NewActivity_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ParkingLotActivityModel model = new ParkingLotActivityModel
            {
                ParkingLotActivityId = 1
            };
            _manager.Setup(m => m.AddParkingLotActivity(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.NewActivity(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void EndActivity_ReturnsOk()
        {
            // Arrange
            ParkingLotActivityModel model = new ParkingLotActivityModel
            {
                ParkingLotActivityId = 1
            };

            ParkingLotActivityViewModel viewModel = new ParkingLotActivityViewModel
            {
                ParkingLotActivityId = 1,
                ParkingLotId = 1,
                PlateNumber = "ABC123",
                ParkingValue = 20
            };


            _manager.Setup(m => m.EndParkingLotActivity(model)).Returns(viewModel);

            // Act
            IActionResult result = _controller.EndActivity(model);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            var returnedViewModel = Assert.IsAssignableFrom<ParkingLotActivityViewModel>(okResult.Value);
            Assert.Equal(viewModel, returnedViewModel); 
        }

        [Fact]
        public void EndActivity_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ParkingLotActivityModel model = new ParkingLotActivityModel
            {
                ParkingLotActivityId = 1
            };
            _manager.Setup(m => m.EndParkingLotActivity(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.EndActivity(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void DeleteParkingLot_ReturnsOk()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark"
            };
            _manager.Setup(m => m.RemoveParkingLot(model));

            // Act
            IActionResult result = _controller.DeleteParkingLot(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteParkingLot_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark"
            };
            _manager.Setup(m => m.RemoveParkingLot(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.DeleteParkingLot(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void DeleteClient_ReturnsOk()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe"
            };
            _manager.Setup(m => m.RemoveClient(model));

            // Act
            IActionResult result = _controller.DeleteClient(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteClient_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe"
            };
            _manager.Setup(m => m.RemoveClient(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.DeleteClient(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void DeleteVehicle_ReturnsOk()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123"
            };
            _manager.Setup(m => m.RemoveVehicle(model));

            // Act
            IActionResult result = _controller.DeleteVehicle(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteVehicle_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123"
            };
            _manager.Setup(m => m.RemoveVehicle(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.DeleteVehicle(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }


        [Fact]
        public void UpdateeParkingLot_ReturnsOk()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark"
            };
            _manager.Setup(m => m.UpdateParkingLot(model));

            // Act
            IActionResult result = _controller.UpdateParkingLot(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateParkingLot_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark"
            };
            _manager.Setup(m => m.UpdateParkingLot(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.UpdateParkingLot(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }


        [Fact]
        public void UpdateClient_ReturnsOk()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe"
            };
            _manager.Setup(m => m.UpdateClient(model));

            // Act
            IActionResult result = _controller.UpdateClient(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateClient_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe"
            };
            _manager.Setup(m => m.UpdateClient(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.UpdateClient(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void UpdateVehicle_ReturnsOk()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123"
            };
            _manager.Setup(m => m.UpdateVehicle(model));

            // Act
            IActionResult result = _controller.UpdateVehicle(model);

            // Assert
            OkResult okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateVehicle_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123"
            };
            _manager.Setup(m => m.UpdateVehicle(model)).Throws(new Exception());

            // Act
            IActionResult result = _controller.UpdateVehicle(model);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void GetListOfVehiclesOnParkingLot_ReturnsOk()
        {
            // Arrange
            string parkingLotName = "TestPark";
            List<Vehicle> vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    VehicleId = 1
                },
                new Vehicle
                {
                    VehicleId = 2
                }
            };
            _manager.Setup(m => m.GetVehiclesAtParkingLot(parkingLotName)).Returns(vehicles);

            // Act
            IActionResult result = _controller.GetListOfVehiclesOnParkingLot(parkingLotName);

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            var returnedList = Assert.IsAssignableFrom<List<Vehicle>>(okResult.Value);
            Assert.Equal(vehicles, returnedList);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetListOfVehiclesOnParkingLot_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            string parkingLotName = "TestPark";
            _manager.Setup(m => m.GetVehiclesAtParkingLot(parkingLotName)).Throws(new Exception());

            // Act
            IActionResult result = _controller.GetListOfVehiclesOnParkingLot(parkingLotName);

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void GetListOfVehiclesOfClient_ReturnsOk()
        {
            // Arrange
            string parkingLotName = "TestPark";
            List<VehicleViewModel> vehicles = new List<VehicleViewModel>
            {
                new VehicleViewModel
                {
                    PlateNumber = "ABC123"
                },
                new VehicleViewModel
                {
                    PlateNumber = "ABC456"
                }
            };
            _manager.Setup(m => m.GetVehiclesOfClient("John", "Doe")).Returns(vehicles);

            // Act
            IActionResult result = _controller.GetListOfVehiclesOfClient("John", "Doe");

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            var returnedList = Assert.IsAssignableFrom<List<VehicleViewModel>>(okResult.Value);
            Assert.Equal(vehicles, returnedList);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetListOfVehiclesOfClient_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            _manager.Setup(m => m.GetVehiclesOfClient("John", "Doe")).Throws(new Exception());

            // Act
            IActionResult result = _controller.GetListOfVehiclesOfClient("John", "Doe");

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void GetListOfActivitiesOfParkingLot_ReturnsOk()
        {
            // Arrange
            string parkingLotName = "TestPark";
            List<ParkingLotActivity> activities = new List<ParkingLotActivity>
            {
                new ParkingLotActivity
                {
                    ParkingLotActivityId = 1
                },
                new ParkingLotActivity
                {
                    ParkingLotActivityId = 2
                }
            };

            _manager.Setup(m => m.GetParkingLotActivities("TestPark")).Returns(activities);

            // Act
            IActionResult result = _controller.GetListOfActivitiesOfParkingLot("TestPark");

            // Assert
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
            var returnedList = Assert.IsAssignableFrom<List<ParkingLotActivityViewModel>>(okResult.Value);
            Assert.Equal(activities.Count, returnedList.Count);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetListOfActivitiesOfParkingLot_ReturnsNoContentWhenActivitiesIsNull()
        {
            // Arrange
            _manager.Setup(m => m.GetParkingLotActivities("TestPark")).Returns((List<ParkingLotActivity>)null);

            // Act
            IActionResult result = _controller.GetListOfActivitiesOfParkingLot("TestPark");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public void GetListOfActivitiesOfParkingLot_ExceptionsOccurs_ReturnsBadRequest()
        {
            // Arrange
            _manager.Setup(m => m.GetParkingLotActivities("TestPark")).Throws(new Exception());

            // Act
            IActionResult result = _controller.GetListOfActivitiesOfParkingLot("TestPark");

            // Assert
            BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }
    }
}