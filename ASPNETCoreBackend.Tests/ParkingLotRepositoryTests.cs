using ASPNETCoreBackend.Entities;
using ASPNETCoreBackend.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreBackend.Tests
{
    public class ParkingLotRepositoryTests()
    {
        [Fact]
        public void AddParkingLot_ShouldAddToDatabase()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                // Act
                parkingLotRepository.AddParkingLot(parkingLot);

                // Assert
                ParkingLot addedParkingLot = context.ParkingLots.FirstOrDefault(p => p.Name == "TestPark");
                Assert.NotNull(addedParkingLot);

                Assert.Equal("Streets with no name", addedParkingLot.Address);
                Assert.Equal(10, addedParkingLot.PricePerAdditionalHour);
                Assert.Equal(15, addedParkingLot.PriceFirstHour);
            }
        }

        [Fact]
        public void RemoveParkingLot_ShouldRemoveFromDatabase()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                parkingLotRepository.AddParkingLot(parkingLot);

                // Act
                parkingLotRepository.RemoveParkingLot(1);
                ParkingLot addedParkingLot = context.ParkingLots.FirstOrDefault(p => p.Name == "TestPark");

                // Assert
                Assert.Null(addedParkingLot);
            }
        }

        [Fact]
        public void GetParkingLot_ValidId_ShouldReturnParkingLot()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                parkingLotRepository.AddParkingLot(parkingLot);

                // Act
                ParkingLot addedParkingLot = parkingLotRepository.GetParkingLot(1);

                // Assert
                Assert.NotNull(addedParkingLot);
                Assert.Equal("TestPark", addedParkingLot.Name);
            }
        }

        [Fact]
        public void GetParkingLot_InvalidId_ShouldReturnNull()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                parkingLotRepository.AddParkingLot(parkingLot);

                // Act
                ParkingLot addedParkingLot = parkingLotRepository.GetParkingLot(10);

                // Assert
                Assert.Null(addedParkingLot);
            }
        }

        [Fact]
        public void GetParkingLot_ValidName_ShouldReturnParkingLot()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                parkingLotRepository.AddParkingLot(parkingLot);

                // Act
                ParkingLot addedParkingLot = parkingLotRepository.GetParkingLot("TestPark");

                // Assert
                Assert.NotNull(addedParkingLot);
                Assert.Equal(1, addedParkingLot.ParkingLotId);
            }
        }

        [Fact]
        public void GetParkingLot_InvalidName_ShouldReturnNull()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                parkingLotRepository.AddParkingLot(parkingLot);

                // Act
                ParkingLot addedParkingLot = parkingLotRepository.GetParkingLot("SomethingPark");

                // Assert
                Assert.Null(addedParkingLot);
            }
        }

        [Fact]
        public void UpdateParkingLot_ShouldUpdateDatabaseRecord()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                parkingLotRepository.AddParkingLot(parkingLot);
                parkingLot.Address = "New street";
                parkingLot.PricePerAdditionalHour = 15;
                parkingLot.PriceFirstHour = 20;

                // Act
                parkingLotRepository.UpdateParkingLot(parkingLot);
                ParkingLot updatedParkingLot = context.ParkingLots.FirstOrDefault(p => p.Name == "TestPark");
                Assert.NotNull(updatedParkingLot);

                // Assert
                Assert.Equal("New street", updatedParkingLot.Address);
                Assert.Equal(15, updatedParkingLot.PricePerAdditionalHour);
                Assert.Equal(20, updatedParkingLot.PriceFirstHour);
            }
        }

        [Fact]
        public void GetVehiclesByParkingLotId_WithParkedVehicles_ShouldReturnListOfVehicles()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ClientRepository clientRepository = new ClientRepository(context);
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);
                ParkingLotActivityRepository activityRepository = new ParkingLotActivityRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };
                parkingLotRepository.AddParkingLot(parkingLot);

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

                ParkingLotActivity activity = new ParkingLotActivity
                {
                    ParkingLotId = 1,
                    ClientId = 1,
                    VehicleId = 1,
                    StartDate = DateTime.Now,
                };

                activityRepository.AddParkingLotActivity(activity);

                // Act
                List<Vehicle> vehicles = parkingLotRepository.GetVehiclesByParkingLotId(1);
                
                // Assert 
                Assert.NotNull(vehicles);
                Assert.Equal(1, vehicles.Count);
                Assert.Contains(vehicles, v => v.PlateNumber == "ABC123");
            }
        }

        [Fact]
        public void GetVehiclesByParkingLotId_WithNoParkedVehicles_ShouldReturnEmptyList()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ClientRepository clientRepository = new ClientRepository(context);
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);
                VehicleRepository vehicleRepository = new VehicleRepository(context);
                ParkingLotActivityRepository activityRepository = new ParkingLotActivityRepository(context);

                ParkingLot parkingLot1 = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };

                ParkingLot parkingLot2 = new ParkingLot
                {
                    Name = "SomePark",
                    Address = "Streets with a name",
                    PricePerAdditionalHour = 15,
                    PriceFirstHour = 20
                };
                parkingLotRepository.AddParkingLot(parkingLot1);
                parkingLotRepository.AddParkingLot(parkingLot2);

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

                ParkingLotActivity activity = new ParkingLotActivity
                {
                    ParkingLotId = 1,
                    ClientId = 1,
                    VehicleId = 1,
                    StartDate = DateTime.Now,
                };

                activityRepository.AddParkingLotActivity(activity);

                // Act
                List<Vehicle> vehicles = parkingLotRepository.GetVehiclesByParkingLotId(2);

                // Assert
                Assert.NotNull(vehicles);
                Assert.Equal(0, vehicles.Count);
            }
        }
    }
}