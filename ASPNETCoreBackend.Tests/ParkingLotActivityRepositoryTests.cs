using ASPNETCoreBackend.Entities;
using ASPNETCoreBackend.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreBackend.Tests
{
    public class ParkingLotActivityRepositoryTests()
    {
        [Fact]
        public void AddParkingLotActivity_ShouldAddDatabaseRecord()
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

                // Act
                activityRepository.AddParkingLotActivity(activity);
                ParkingLotActivity addedActivity = context
                    .ParkingLotActivities
                    .FirstOrDefault(a => a.ParkingLotActivityId == 1);

                // Arrange
                Assert.NotNull(addedActivity);
                Assert.Equal(1, addedActivity.ParkingLotId);
                Assert.Equal(1, addedActivity.ClientId);
                Assert.Equal(1, addedActivity.VehicleId);
                Assert.Null(addedActivity.EndDate);
                Assert.Equal(0, addedActivity.ParkingValue);
            }
        }

        [Fact]
        public void GetByClientId_WithActivities_ShouldReturnListOfActivities()
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
                List<ParkingLotActivity> activities = activityRepository.GetByClientId(1);

                // Assert
                Assert.NotNull(activities);
                Assert.Equal(1, activities.Count);
            }
        }

        [Fact]
        public void GetByClientId_WithNoActivities_ShouldReturnEmptyList()
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

                Client client1 = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };

                Client client2 = new Client
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    PhoneNumber = "35479851",
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

                ParkingLotActivity activity = new ParkingLotActivity
                {
                    ParkingLotId = 1,
                    ClientId = 1,
                    VehicleId = 1,
                    StartDate = DateTime.Now,
                };
                activityRepository.AddParkingLotActivity(activity);

                // Act
                List<ParkingLotActivity> activities = activityRepository.GetByClientId(2);

                // Assert
                Assert.NotNull(activities);
                Assert.Equal(0, activities.Count);
            }
        }


        [Fact]
        public void GetByClientName_WithActivities_ShouldReturnListOfActivities()
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
                List<ParkingLotActivity> activities = activityRepository.GetByClientName("John", "Doe");

                // Assert
                Assert.NotNull(activities);
                Assert.Equal(1, activities.Count);
            }
        }


        [Fact]
        public void GetByClientName_WithNoActivities_ShouldReturnEmptyList()
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

                Client client1 = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                };

                Client client2 = new Client
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    PhoneNumber = "35479851",
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

                ParkingLotActivity activity = new ParkingLotActivity
                {
                    ParkingLotId = 1,
                    ClientId = 1,
                    VehicleId = 1,
                    StartDate = DateTime.Now,
                };
                activityRepository.AddParkingLotActivity(activity);

                // Act
                List<ParkingLotActivity> activities = activityRepository.GetByClientName("Jane", "Doe");

                // Assert
                Assert.NotNull(activities);
                Assert.Equal(0, activities.Count);
            }
        }

        [Fact]
        public void GetById_ValidId_ShouldReturnDatabaseRecord()
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
                ParkingLotActivity addedActivity = activityRepository.GetById(1);

                // Assert
                Assert.NotNull(addedActivity);
                Assert.Equal(1, addedActivity.ParkingLotId);
                Assert.Equal(1, addedActivity.ClientId);
                Assert.Equal(1, addedActivity.VehicleId);
            }
        }

        [Fact]
        public void GetById_NonValidId_ShouldReturnNull()
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
                ParkingLotActivity addedActivity = activityRepository.GetById(2);

                // Assert
                Assert.Null(addedActivity);
            }
        }

        [Fact]
        public void GetFromPlateNumber_ValidPlateNumber_ShouldReturnDatabaseRecord()
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
                ParkingLotActivity addedActivity = activityRepository.GetFromPlateNumber("ABC123", "TestPark");

                // Assert
                Assert.NotNull(addedActivity);
                Assert.Equal(1, addedActivity.ParkingLotId);
                Assert.Equal(1, addedActivity.ClientId);
                Assert.Equal(1, addedActivity.VehicleId);
            }
        }

        [Fact]
        public void GetFromPlateNumber_NonValidPlateNumber_ShouldReturnNull()
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
                    PlateNumber = "AAA222",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };
                vehicleRepository.AddVehicle(vehicle1);
                vehicleRepository.AddVehicle(vehicle2);

                ParkingLotActivity activity = new ParkingLotActivity
                {
                    ParkingLotId = 1,
                    ClientId = 1,
                    VehicleId = 1,
                    StartDate = DateTime.Now,
                };
                activityRepository.AddParkingLotActivity(activity);

                // Act
                ParkingLotActivity addedActivity = activityRepository.GetFromPlateNumber("AAA222", "TestPark");

                // Assert
                Assert.Null(addedActivity);
            }
        }

        [Fact]
        public void GetByParkingLotId_WithParkedVehicles_ShouldReturnListOfActivities()
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
                    PlateNumber = "AAA222",
                    Brand = "Renault",
                    Model = "Kwid",
                    Color = "Black",
                    Year = 2020,
                    Client = client
                };
                vehicleRepository.AddVehicle(vehicle1);
                vehicleRepository.AddVehicle(vehicle2);

                ParkingLotActivity activity1 = new ParkingLotActivity
                {
                    ParkingLotId = 1,
                    ClientId = 1,
                    VehicleId = 1,
                    StartDate = DateTime.Now,
                };
                ParkingLotActivity activity2 = new ParkingLotActivity
                {
                    ParkingLotId = 1,
                    ClientId = 1,
                    VehicleId = 2,
                    StartDate = DateTime.Now,
                };
                activityRepository.AddParkingLotActivity(activity1);
                activityRepository.AddParkingLotActivity(activity2);

                // Act
                List<ParkingLotActivity> addedActivities = activityRepository.GetByParkingLotId(1);

                // Assert
                Assert.NotNull(addedActivities);
                Assert.Equal(2, addedActivities.Count);
                Assert.Contains(addedActivities, a => a.VehicleId == 1);
                Assert.Contains(addedActivities, a => a.VehicleId == 2);
            }
        }

        [Fact]
        public void GetByParkingLotId_WithNoParkedVehicles_ShouldReturnEmptyList()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ParkingLotRepository parkingLotRepository = new ParkingLotRepository(context);
                ParkingLotActivityRepository activityRepository = new ParkingLotActivityRepository(context);

                ParkingLot parkingLot = new ParkingLot
                {
                    Name = "TestPark",
                    Address = "Streets with no name",
                    PricePerAdditionalHour = 10,
                    PriceFirstHour = 15
                };
                parkingLotRepository.AddParkingLot(parkingLot);

                // Act
                List<ParkingLotActivity> addedActivities = activityRepository.GetByParkingLotId(1);

                // Assert
                Assert.NotNull(addedActivities);
                Assert.Equal(0, addedActivities.Count);
            }
        }

        [Fact]
        public void RemoveParkingLotActivity_ShouldRemoveDatabaseRecord()
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
                activityRepository.RemoveParkingLotActivity(1);
                ParkingLotActivity removedActivity = context.ParkingLotActivities.FirstOrDefault(
                    a => a.ParkingLotActivityId == 1
                    );

                // Assert
                Assert.Null(removedActivity);
            }
        }

        [Fact]
        public void UpdateParkingLotActivity_ShouldUpdateDatabaseRecord()
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

                DateTime startDate = DateTime.Now;
                activity.StartDate = startDate;

                // Act
                activityRepository.UpdateParkingLotActivity(activity);
                ParkingLotActivity updatedActivity = context
                    .ParkingLotActivities
                    .FirstOrDefault(a => a.ParkingLotActivityId == 1);

                // Assert
                Assert.NotNull(updatedActivity);
                Assert.Equal(startDate, updatedActivity.StartDate);
            }
        }
    }
}
