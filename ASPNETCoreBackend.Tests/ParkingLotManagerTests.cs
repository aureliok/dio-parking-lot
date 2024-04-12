using ASPNETCoreBackend.Entities;
using ASPNETCoreBackend.Models;
using ASPNETCoreBackend.Repositories.Interfaces;
using ASPNETCoreBackend.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ASPNETCoreBackend.Tests
{
    public class ParkingLotManagerTests
    {
        private Mock<IClientRepository> _mockClientRepo;
        private Mock<IVehicleRepository> _mockVehicleRepo;
        private Mock<IParkingLotRepository> _mockParkingLotRepo;
        private Mock<IParkingLotActivityRepository> _mockActivityRepo;
        private ParkingLotManager _manager;

        public ParkingLotManagerTests()
        {
            _mockClientRepo = new Mock<IClientRepository>();
            _mockVehicleRepo = new Mock<IVehicleRepository>();
            _mockParkingLotRepo = new Mock<IParkingLotRepository>();
            _mockActivityRepo = new Mock<IParkingLotActivityRepository>();

            _manager = new ParkingLotManager(
                _mockClientRepo.Object,
                _mockVehicleRepo.Object,
                _mockParkingLotRepo.Object,
                _mockActivityRepo.Object
            );
        }

        [Fact]
        public void AddClient_ShouldAddClientToRepository()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "test@email.com"
            };

            // Act
            _manager.AddClient(model);

            // Assert
            _mockClientRepo.Verify(repo => repo.AddClient(It.Is<Client>(client =>
                client.FirstName == "John" &&
                client.LastName == "Doe" &&
                client.PhoneNumber == "1234567890" &&
                client.Email == "test@email.com"
            )), Times.Once());
        }

        [Fact]
        public void AddParkingLot_ShouldAddParkingLotToRepository()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark",
                Address = "End of the world",
                PricePerAdditionalHour = 10,
                PriceFirstHour = 15,
            };

            // Act
            _manager.AddParkingLot(model);

            // Assert
            _mockParkingLotRepo.Verify(repo => repo.AddParkingLot(It.Is<ParkingLot>(p =>
                p.Name == "TestPark" &&
                p.Address == "End of the world" &&
                p.PricePerAdditionalHour == 10 &&
                p.PriceFirstHour == 15
            )), Times.Once());
        }

        [Fact]
        public void AddVehicle_ShouldAddVehicleToRepository()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123",
                Brand = "Renault",
                Model = "Kwid",
                Color = "Black",
                Year = 2020,
                ClientFirstName = "John",
                ClientLastName = "Doe"
            };

            Client client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                ClientId = 1
            };

            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);

            // Act
            _manager.AddVehicle(model);

            // Assert
            _mockVehicleRepo.Verify(repo => repo.AddVehicle(It.Is<Vehicle>(v =>
                v.PlateNumber == "ABC123" &&
                v.Brand == "Renault" &&
                v.Model == "Kwid" &&
                v.Color == "Black" &&
                v.Year == 2020 &&
                v.ClientId == 1
            )), Times.Once());
        }

        [Fact]
        public void RemoveClient_ShouldRemoveClientFromRepository()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890"
            };

            Client client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                ClientId = 1
            };

            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);

            // Act
            _manager.RemoveClient(model);

            // Assert
            _mockClientRepo.Verify(repo => repo.RemoveClient(It.Is<Client>(c =>
                c.FirstName == "John" &&
                c.LastName == "Doe" &&
                c.PhoneNumber == "1234567890"
            )), Times.Once());
        }

        [Fact]
        public void RemoveClient_ClientNotFound_ShouldNotDoAnything()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890"
            };

            Client client = null;

            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);

            // Act
            _manager.RemoveClient(model);

            // Assert
            _mockClientRepo.Verify(repo => repo.RemoveClient(It.Is<Client>(c =>
                c.FirstName == "John" &&
                c.LastName == "Doe" &&
                c.PhoneNumber == "1234567890"
            )), Times.Never());
        }

        [Fact]
        public void RemoveParkingLot_ShouldRemoveParkingLotFromRepository()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark",
            };

            ParkingLot parkingLot = new ParkingLot
            {
                Name = "TestPark",
                Address = "Default",
                PriceFirstHour = 15,
                PricePerAdditionalHour = 10,
                ParkingLotId = 1
            };

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);

            // Act
            _manager.RemoveParkingLot(model);

            // Assert
            _mockParkingLotRepo.Verify(repo => repo.RemoveParkingLot(It.Is<int>(p =>
                p == parkingLot.ParkingLotId
            )), Times.Once());
        }

        [Fact]
        public void RemoveParkingLot_ParkingLotNotFound_ShouldNotDoAnything()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark",
            };

            ParkingLot parkingLot = null;

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);

            // Act
            _manager.RemoveParkingLot(model);

            // Assert
            _mockParkingLotRepo.Verify(repo => repo.RemoveParkingLot(It.Is<int>(p =>
                p == parkingLot.ParkingLotId
            )), Times.Never());
        }

        [Fact]
        public void RemoveVehicle_ShouldVehicleFromRepository()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123",
            };

            Vehicle vehicle = new Vehicle
            {
                PlateNumber = "ABC123",
                VehicleId = 1
            };

            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);

            // Act
            _manager.RemoveVehicle(model);

            // Assert
            _mockVehicleRepo.Verify(repo => repo.RemoveVehicle(It.Is<Vehicle>(v =>
                v.PlateNumber == "ABC123" &&
                v.VehicleId == 1
            )), Times.Once());
        }

        [Fact]
        public void RemoveVehicle_VehicleNotFound_ShouldNotDoAnything()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123",
            };

            Vehicle vehicle = null;

            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);

            // Act
            _manager.RemoveVehicle(model);

            // Assert
            _mockVehicleRepo.Verify(repo => repo.RemoveVehicle(It.Is<Vehicle>(v =>
                v.PlateNumber == "ABC123" &&
                v.VehicleId == 1
            )), Times.Never());
        }

        [Fact]
        public void UpdateClient_ShouldUpdateClientOnRepository()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "123456",
                Email = "test@email.com",
            };

            Client client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "oldPhone",
                Email = "oldEmail",
                ClientId = 1,
            };

            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);

            // Act
            _manager.UpdateClient(model);

            // Assert
            _mockClientRepo.Verify(repo => repo.UpdateClient(It.Is<Client>(c =>
                c.FirstName == "John" &&
                c.LastName == "Doe" &&
                c.PhoneNumber == "123456" &&
                c.Email == "test@email.com"
            )), Times.Once());

            Assert.Equal("123456", client.PhoneNumber);
            Assert.Equal("test@email.com", client.Email);
        }

        [Fact]
        public void UpdateClient_ClientNotFound_ShouldThrowKeyNotException()
        {
            // Arrange
            ClientModel model = new ClientModel
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "123456",
                Email = "test@email.com",
            };

            Client client = null;

            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);

            // Act and Assert
            Assert.Throws<KeyNotFoundException>(() => _manager.UpdateClient(model));
        }

        [Fact]
        public void UpdateParkingLot_ShouldUpdateParkingLotOnRepository()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark",
                Address = "newAddress",
                PriceFirstHour = 10,
                PricePerAdditionalHour = 5
            };

            ParkingLot parkingLot = new ParkingLot
            {
                Name = "TestPark",
                Address = "oldAddress",
                PriceFirstHour = 8,
                PricePerAdditionalHour = 4,
                ParkingLotId = 1
            };

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);

            // Act
            _manager.UpdateParkingLot(model);

            // Assert
            _mockParkingLotRepo.Verify(repo => repo.UpdateParkingLot(It.Is<ParkingLot>(p => 
                p.Name == "TestPark" &&
                p.Address == "newAddress" &&
                p.PriceFirstHour == 10 &&
                p.PricePerAdditionalHour == 5 &&
                p.ParkingLotId == 1
            )), Times.Once());

            Assert.Equal("newAddress", parkingLot.Address);
            Assert.Equal(10, parkingLot.PriceFirstHour);
            Assert.Equal(5, parkingLot.PricePerAdditionalHour);
        }

        [Fact]
        public void UpdateParkingLot_ParkingLotNotFound_ShouldNotDoAnything()
        {
            // Arrange
            ParkingLotModel model = new ParkingLotModel
            {
                Name = "TestPark",
                Address = "newAddress",
                PriceFirstHour = 10,
                PricePerAdditionalHour = 5
            };

            ParkingLot parkingLot = null;

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);

            // Act
            _manager.UpdateParkingLot(model);

            // Assert
            _mockParkingLotRepo.Verify(repo => repo.UpdateParkingLot(It.Is<ParkingLot>(p =>
                p.Name == "TestPark" &&
                p.Address == "newAddress" &&
                p.PriceFirstHour == 10 &&
                p.PricePerAdditionalHour == 5 &&
                p.ParkingLotId == 1
            )), Times.Never());
        }

        [Fact]
        public void UpdateVehicle_ShouldUpdateVehicleOnRepository()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123",
                Color = "Blue",
                ClientFirstName = "Jane",
                ClientLastName = "Doe"
            };

            Vehicle vehicle = new Vehicle
            {
                PlateNumber = "ABC123",
                Color = "Black",
                ClientId = 1
            };

            Client newClient = new Client
            {
                ClientId = 2,
                FirstName = "Jane",
                LastName = "Doe"
            };

            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);
            _mockClientRepo.Setup(repo => repo.GetByFullName("Jane", "Doe")).Returns(newClient);

            // Act
            _manager.UpdateVehicle(model);

            // Assert
            _mockVehicleRepo.Verify(repo => repo.UpdateVehicle(It.Is<Vehicle>(v =>
                v.PlateNumber == "ABC123" &&
                v.Color == "Blue" &&
                v.ClientId == 2
            )), Times.Once());

            Assert.Equal("Blue", vehicle.Color);
            Assert.Equal(2, vehicle.ClientId);
        }

        [Fact]
        public void UpdateVehicle_NewOwnerNotFound_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123",
                Color = "Blue",
                ClientFirstName = "Jane",
                ClientLastName = "Doe"
            };

            Vehicle vehicle = new Vehicle
            {
                PlateNumber = "ABC123",
                Color = "Black",
                ClientId = 1
            };

            Client newClient = null;

            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);
            _mockClientRepo.Setup(repo => repo.GetByFullName("Jane", "Doe")).Returns(newClient);

            // Act and Assert
            Assert.Throws<KeyNotFoundException>(() => _manager.UpdateVehicle(model));
        }

        [Fact]
        public void UpdateVehicle_VehicleNotFound_ShouldNotDoAnything()
        {
            // Arrange
            VehicleModel model = new VehicleModel
            {
                PlateNumber = "ABC123",
                Color = "Blue",
                ClientFirstName = "Jane",
                ClientLastName = "Doe"
            };

            Vehicle vehicle = null;

            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);

            // Act
            _manager.UpdateVehicle(model);

            // Assert
            _mockVehicleRepo.Verify(repo => repo.UpdateVehicle(It.Is<Vehicle>(v => 
                v.PlateNumber == "ABC123" &&
                v.Color == "Blue" &&
                v.Client.FirstName == "Jane" &&
                v.Client.LastName == "Doe"
            )), Times.Never());
        }

        [Fact]
        public void AddParkingLotActivity_ShouldAddActivityOnRepository()
        {
            // Arrange
            ParkingLotActivityModel model = new ParkingLotActivityModel
            {
                ParkingLotName = "TestPark",
                ClientFirstName = "John",
                ClientLastName = "Doe",
                VehiclePlateNumber = "ABC123",
            };

            ParkingLot parkingLot = new ParkingLot
            {
                ParkingLotId = 1,
            };

            Client client = new Client
            {
                ClientId = 1
            };

            Vehicle vehicle = new Vehicle
            {
                VehicleId = 1
            };

            ParkingLotActivity parkingLotActivity = null;

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);
            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);
            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);
            _mockActivityRepo.Setup(repo => repo.GetFromPlateNumber("ABC123", "TestPark")).Returns(parkingLotActivity);

            // Act
            _manager.AddParkingLotActivity(model);

            // Assert
            _mockActivityRepo.Verify(repo => repo.AddParkingLotActivity(It.Is<ParkingLotActivity>(a =>
                a.ParkingLotId == 1 &&
                a.ClientId == 1 &&
                a.VehicleId == 1 &&
                a.ParkingValue == -1
            )), Times.Once());
        }

        [Fact]
        public void AddParkingLotActivity_ActivityExists_ShouldThrowException()
        {
            // Arrange
            ParkingLotActivityModel model = new ParkingLotActivityModel
            {
                ParkingLotName = "TestPark",
                ClientFirstName = "John",
                ClientLastName = "Doe",
                VehiclePlateNumber = "ABC123",
            };

            ParkingLot parkingLot = new ParkingLot
            {
                ParkingLotId = 1,
            };

            Client client = new Client
            {
                ClientId = 1
            };

            Vehicle vehicle = new Vehicle
            {
                VehicleId = 1
            };

            ParkingLotActivity parkingLotActivity = new ParkingLotActivity
            {
                ParkingLotId = 1
            };

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);
            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);
            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);
            _mockActivityRepo.Setup(repo => repo.GetFromPlateNumber("ABC123", "TestPark")).Returns(parkingLotActivity);

            // Act
            Assert.Throws<Exception>(() => _manager.AddParkingLotActivity(model));
        }

        [Fact]
        public void GetParkingLotActivityViewModel_ShouldReturnActivityViewModel()
        {
            // Assert
            ParkingLot parkingLot = new ParkingLot
            {
                ParkingLotId = 1,
                Name = "TestPark",
                PricePerAdditionalHour = 5,
                PriceFirstHour = 10
            };

            Client client = new Client
            {
                FirstName = "John",
                LastName = "Doe",
                ClientId = 1
            };

            Vehicle vehicle = new Vehicle
            {
                PlateNumber = "ABC123"
            };

            ParkingLotActivity activity = new ParkingLotActivity
            {
                ParkingLotActivityId = 1,
                ParkingLotId = 1,
                ParkingLot = parkingLot,
                Client = client,
                Vehicle = vehicle,
                StartDate = DateTime.Now,
            };

            // Act
            ParkingLotActivityViewModel viewModel = _manager.GetParkingLotActivityViewModel(activity);

            // Assert
            Assert.Equal(activity.ParkingLotActivityId, viewModel.ParkingLotActivityId);
            Assert.Equal(activity.ParkingLotId, viewModel.ParkingLotId);
            Assert.Equal(activity.ParkingLot.Name, viewModel.ParkingLotName);
            Assert.Equal(activity.ParkingLot.PricePerAdditionalHour, viewModel.PricePerAdditionalHour);
            Assert.Equal(activity.ParkingLot.PriceFirstHour, viewModel.PriceFirstHour);
            Assert.Equal(activity.Vehicle.PlateNumber, viewModel.PlateNumber);
            Assert.Equal(activity.Client.FirstName, viewModel.ClientFirstName);
            Assert.Equal(activity.Client.LastName, viewModel.ClientLastName);
            Assert.Equal(activity.StartDate, viewModel.StartDate);
            Assert.Equal(activity.EndDate, viewModel.EndDate);
            Assert.Equal(activity.ParkingValue, viewModel.ParkingValue);
        }

        [Fact]
        public void EndParkingLotActivity_ShouldEndActivityAndCalculateParkingValue()
        {
            // Arrange
            ParkingLot parkingLot = new ParkingLot
            {
                Name = "TestPark",
                PricePerAdditionalHour = 5,
                PriceFirstHour = 10
            };

            Vehicle vehicle = new Vehicle
            {
                PlateNumber = "ABC123",
                ClientId = 1
            };

            Client client = new Client
            {
                ClientId = 1
            };

            ParkingLotActivityModel model = new ParkingLotActivityModel
            {
                ParkingLotName = "TestPark",
                VehiclePlateNumber = "ABC123",
                EndDate = new DateTime(2024, 4, 10, 14, 0, 0)
            };

            ParkingLotActivity activity = new ParkingLotActivity
            {
                ParkingLotId = 1,
                ParkingLot = parkingLot,
                VehicleId = 1,
                Vehicle = vehicle,
                ClientId = 1,
                Client = client,
                StartDate = new DateTime(2024, 4, 10, 12, 0, 0)
        };

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);
            _mockVehicleRepo.Setup(repo => repo.GetVehicle("ABC123")).Returns(vehicle);
            _mockActivityRepo.Setup(repo => repo.GetFromPlateNumber("ABC123", "TestPark")).Returns(activity);

            // Act
            _manager.EndParkingLotActivity(model);

            // Arrange
            _mockActivityRepo.Verify(repo => repo.UpdateParkingLotActivity(It.Is<ParkingLotActivity>(a =>
                a.ParkingLotId == 1 &&
                a.VehicleId == 1 &&
                a.ClientId == 1
            )), Times.Once());

            Assert.Equal(15, activity.ParkingValue);       
        }

        [Fact]
        public void GetVehiclesAtParkingLot_ShouldReturnListOfVehicles()
        {
            // Arrange
            ParkingLot parkingLot = new ParkingLot
            {
                ParkingLotId = 1,
                Name = "TestPark"
            };

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);

            // Act
            _manager.GetVehiclesAtParkingLot("TestPark");

            // Arrange
            _mockParkingLotRepo.Verify(repo => repo.GetVehiclesByParkingLotId(It.Is<int>(p =>
                p == parkingLot.ParkingLotId
            )), Times.Once());
        }

        [Fact]
        public void GetVehiclesOfClient_ShouldReturnListOfVehiclesViewModel()
        {
            // Arrange
            Client client = new Client
            {
                ClientId = 1
            };

            _mockClientRepo.Setup(repo => repo.GetByFullName("John", "Doe")).Returns(client);

            // Act
            _manager.GetVehiclesOfClient("John", "Doe");

            // Assert
            _mockVehicleRepo.Verify(repo => repo.GetVehiclesByClient(It.Is<int>(p =>
                p == client.ClientId
            )), Times.Once());
        }

        [Fact]
        public void GetParkingLotActivites_ShouldReturnListOfActivities()
        {
            // Arrange
            ParkingLot parkingLot = new ParkingLot
            {
                ParkingLotId = 1
            };

            _mockParkingLotRepo.Setup(repo => repo.GetParkingLot("TestPark")).Returns(parkingLot);

            // Act
            _manager.GetParkingLotActivities("TestPark");

            // Assert
            _mockActivityRepo.Verify(repo => repo.GetByParkingLotId(It.Is<int>(p =>
                p == parkingLot.ParkingLotId
            )), Times.Once());
        }
    }
}
