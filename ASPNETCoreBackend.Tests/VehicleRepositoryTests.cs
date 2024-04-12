using ASPNETCoreBackend.Entities;
using ASPNETCoreBackend.Models;
using ASPNETCoreBackend.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreBackend.Tests
{
    public class VehicleRepositoryTests
    {
        [Fact]
        public void AddVehicle_ShouldAddVehicleToDatabase()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };
                clientRepository.AddClient(client);

                Vehicle vehicle = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };

                // Act
                vehicleRepository.AddVehicle(vehicle);

                // Assert
                Vehicle addedVehicle = context.Vehicles.FirstOrDefault(v => v.PlateNumber == vehicle.PlateNumber);
                Assert.NotNull(addedVehicle);

                Assert.Equal("Renault", addedVehicle.Brand);
                Assert.Equal("Kwid", addedVehicle.Model);
                Assert.Equal("Black", addedVehicle.Color);
                Assert.Equal(2020, addedVehicle.Year);
                Assert.Equal(client.ClientId, addedVehicle.Client.ClientId);

            }
        }

        [Fact]
        public void GetVehicle_ValidId_ShouldReturnVehicle()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };
                clientRepository.AddClient(client);

                Vehicle vehicle = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };
                vehicleRepository.AddVehicle(vehicle);

                // Act
                Vehicle getVehicle = vehicleRepository.GetVehicle(1);

                // Assert
                Assert.NotNull(getVehicle);

                Assert.Equal("ABC123", getVehicle.PlateNumber);
                Assert.Equal("Renault", getVehicle.Brand);
                Assert.Equal("Kwid", getVehicle.Model);
                Assert.Equal("Black", getVehicle.Color);
                Assert.Equal(2020, getVehicle.Year);
                Assert.Equal(client.ClientId, getVehicle.Client.ClientId);
            }
        }

        [Fact]
        public void GetVehicle_InvalidId_ShouldReturnNull()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };
                clientRepository.AddClient(client);

                Vehicle vehicle = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };
                vehicleRepository.AddVehicle(vehicle);

                // Act
                Vehicle getVehicle = vehicleRepository.GetVehicle(2);

                // Assert
                Assert.Null(getVehicle);
            }
        }

        [Fact]
        public void GetVehiclie_ValidPlateNumber_ShouldReturnVehicle()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };
                clientRepository.AddClient(client);

                Vehicle vehicle = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };
                vehicleRepository.AddVehicle(vehicle);

                // Act
                Vehicle getVehicle = vehicleRepository.GetVehicle("ABC123");

                // Assert
                Assert.NotNull(getVehicle);

                Assert.Equal("Renault", getVehicle.Brand);
                Assert.Equal("Kwid", getVehicle.Model);
                Assert.Equal("Black", getVehicle.Color);
                Assert.Equal(2020, getVehicle.Year);
                Assert.Equal(client.ClientId, getVehicle.Client.ClientId);
            }
        }

        [Fact]
        public void GetVehicle_InvalidPlateNumber_ShouldReturnNull()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };
                clientRepository.AddClient(client);

                Vehicle vehicle = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };
                vehicleRepository.AddVehicle(vehicle);

                // Act
                Vehicle getVehicle = vehicleRepository.GetVehicle("XYZ987");

                // Assert
                Assert.Null(getVehicle);
            }
        }

        [Fact]
        public void GetVehiclesByClient_ValidIdWithVehicles_ShouldReturnListOfVehiclesViewModels()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };
                clientRepository.AddClient(client);

                Vehicle vehicle1 = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };

                Vehicle vehicle2 = new Vehicle
                {
                    PlateNumber = "QWE321",
                    Brand = "Nissan",
                    Model = "March",
                    Color = "Blue",
                    Year = 2022,
                    Client = client
                };

                vehicleRepository.AddVehicle(vehicle1);
                vehicleRepository.AddVehicle(vehicle2);

                // Act
                List<VehicleViewModel> vehicles = vehicleRepository.GetVehiclesByClient(1);

                // Assert
                Assert.NotNull(vehicles);
                Assert.Equal(2, vehicles.Count);
                Assert.Contains(vehicles, v => v.PlateNumber == "ABC123");
                Assert.Contains(vehicles, v => v.PlateNumber == "QWE321");
            }
        }

        [Fact]
        public void GetVehiclesByClient_ValidIdWithNoVehicles_ShouldReturnEmptyList()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client1 = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };

                Client client2 = new Client
                {
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "111111111",
                };
                clientRepository.AddClient(client1);
                clientRepository.AddClient(client2);

                Vehicle vehicle1 = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client1
                };

                vehicleRepository.AddVehicle(vehicle1);

                // Act
                List<VehicleViewModel> vehicles = vehicleRepository.GetVehiclesByClient(2);

                // Assert
                Assert.NotNull(vehicles);
                Assert.Equal(0, vehicles.Count);
                Assert.DoesNotContain(vehicles, v => v.PlateNumber == "ABC123");
            }
        }

        [Fact]
        public void UpdateVehicle_ShouldUpdateDatabaseRecord()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client1 = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };

                Client client2 = new Client
                {
                    FirstName = "John",
                    LastName = "Smith",
                    PhoneNumber = "145681",
                };

                clientRepository.AddClient(client1);
                clientRepository.AddClient(client2);

                Vehicle vehicle = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client1
                };
                vehicleRepository.AddVehicle(vehicle);
                vehicle.Color = "Blue";
                vehicle.Client = client2;

                // Act
                vehicleRepository.UpdateVehicle(vehicle);
                Vehicle updatedVehicle = context.Vehicles.FirstOrDefault(v => v.VehicleId == 1);

                // Assert
                Assert.NotNull(updatedVehicle);
                Assert.Equal("Blue", updatedVehicle.Color);
                Assert.Equal("Smith", updatedVehicle.Client.LastName);
                Assert.Equal("Kwid", updatedVehicle.Model);
            }
        }

        [Fact]
        public void RemoveVehicle_ShouldRemoveDatabaseRecord()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);

                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };

                clientRepository.AddClient(client);
                Vehicle vehicle = new Vehicle
                {
                    PlateNumber = "ABC123",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };

                vehicleRepository.AddVehicle(vehicle);

                // Act
                vehicleRepository.RemoveVehicle(vehicle);
                Vehicle removedVehicle = context.Vehicles.FirstOrDefault(v => v.VehicleId == 1);

                // Assert
                Assert.Null(removedVehicle);
            }
        }
    }
}
