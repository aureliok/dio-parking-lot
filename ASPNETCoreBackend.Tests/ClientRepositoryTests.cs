using ASPNETCoreBackend.Entities;
using ASPNETCoreBackend.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ASPNETCoreBackend.Tests
{
    public class ClientRepositoryTests
    {
        [Fact]
        public void AddClient_ShouldAddClientToDatabase()
        {
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                // Arrange
                ClientRepository clientRepository = new ClientRepository(context);

                Client clientToAdd = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890",
                    Email = "test@email.com",
                };

                // Act
                clientRepository.AddClient(clientToAdd);

                // Assert
                Client clientAdded = context.Clients.FirstOrDefault(c => (c.FirstName == "John" && c.LastName == "Doe"));
                Assert.NotNull(clientAdded);

                Assert.Equal("1234567890", clientAdded.PhoneNumber);
                Assert.Equal("test@email.com", clientAdded.Email);

            }
        }

        [Fact]
        public void GetByFullName_ValidName_ReturnsClient()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                context.Clients.Add(new Client { 
                    FirstName = "John", 
                    LastName = "Doe",
                    PhoneNumber = "1234567890"
                });
                context.SaveChanges();

                ClientRepository clientRepository = new ClientRepository(context);

                // Act
                Client result = clientRepository.GetByFullName("John", "Doe");

                // Assert
                Assert.NotNull(result);
                Assert.Equal("John", result.FirstName);
                Assert.Equal("Doe", result.LastName);
            }
        }

        [Fact]
        public void GetByFullName_InvalidName_ReturnsNull()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ClientRepository clientRepository = new ClientRepository(context);

                // Act
                Client result = clientRepository.GetByFullName("John", "Doe");

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void GetById_ValidId_ReturnsClient()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                context.Clients.Add(new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890"
                });
                context.SaveChanges();

                ClientRepository clientRepository = new ClientRepository(context);

                // Act
                Client result = clientRepository.GetById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("John", result.FirstName);
                Assert.Equal("Doe", result.LastName);
            }

        }

        [Fact]
        public void GetById_InvalidId_ReturnsNull()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                ClientRepository clientRepository = new ClientRepository(context);

                // Act
                Client result = clientRepository.GetById(1);

                // Assert
                Assert.Null(result);
            }
        }


        [Fact]
        public void RemoveClient_ShouldRemoveFromDatabase()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890"
                };

                context.Clients.Add(client);
                context.SaveChanges();

                ClientRepository clientRepository = new ClientRepository(context);

                // Act
                clientRepository.RemoveClient(client);

                Client getClient = clientRepository.GetByFullName("John", "Doe");

                // Assert
                Assert.Null(getClient);
            }
        }


        [Fact]
        public void UpdateClient_ValidName_ShouldUpdateClient()
        {
            // Arrange
            string databaseName = "TestDatabase_" + Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ParkingLotDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            using (ParkingLotDbContext context = new ParkingLotDbContext(options))
            {
                Client client = new Client
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890"
                };

                context.Clients.Add(client);
                context.SaveChanges();

                ClientRepository clientRepository = new ClientRepository(context);
                client.PhoneNumber = "0987654321";
                client.Email = "test@email.com";

                // Act
                clientRepository.UpdateClient(client);
                Client updatedClient = clientRepository.GetByFullName("John", "Doe");

                // Arrange
                Assert.Equal("0987654321", updatedClient.PhoneNumber);
                Assert.Equal("test@email.com", updatedClient.Email);
            }
        }
    }
}
