using ASPNETCoreBackend.Entities;
using ASPNETCoreBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreBackend.Repositories.Implementations
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ParkingLotDbContext _context;

        public VehicleRepository(ParkingLotDbContext context)
        {
            _context = context;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public Vehicle GetVehicle(int id)
        {
            Vehicle vehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleId == id);

            return vehicle;
        }

        public Vehicle GetVehicle(string plateNumber)
        {
            Vehicle vehicle = _context.Vehicles.FirstOrDefault(v => v.PlateNumber == plateNumber);

            return vehicle;
        }


        public List<Vehicle> GetVehiclesByClient(int clientId)
        {
            List<Vehicle> vehicles = _context.Vehicles.Where(v => v.ClientId == clientId).ToList();

            return vehicles;
        }


        public void RemoveVehicle(Vehicle vehicle)
        {
            Vehicle checkVehicle = _context.Vehicles.FirstOrDefault(v => v.Equals(vehicle));

            if (checkVehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }
    }
}
