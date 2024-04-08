using ASPNETCoreBackend.Entities;

namespace ASPNETCoreBackend.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle GetVehicle(string plateNumber);
        List<Vehicle> GetVehiclesByClient(int clientId);
        void AddVehicle (Vehicle vehicle);
        void RemoveVehicle (Vehicle vehicle);
    }
}
