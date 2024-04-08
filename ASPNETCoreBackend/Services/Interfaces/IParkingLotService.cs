using ASPNETCoreBackend.Entities;

namespace ASPNETCoreBackend.Services.Interfaces
{
    public interface IParkingLotService
    {
        void AddVehicleToParkingLot(int vehicleId, int parkingLotId);
        void RemoveVehicleToParkingLot(int vehicleId, int parkingLotId);
        List<Vehicle> ListVehiclesOnParkingLot(int parkingLotId);
    }
}
