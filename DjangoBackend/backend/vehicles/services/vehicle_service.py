from ..models import Vehicle, VehicleModel
from clients.models import Client
from clients.repositories.client_repository import ClientRepository
from ..repositories.vehicle_repository import VehicleRepository
from typing import List

class VehicleService:
    def __init__(self):
        self.vehicle_repo = VehicleRepository()
        self.client_repo = ClientRepository()


    def add_vehicle(self, vehicle_model: VehicleModel) -> None:
        client: Client = self.client_repo.get_by_fullname(vehicle_model.client_firstname, vehicle_model.client_lastname)

        if client is None:
            raise ValueError("client not found")

        vehicle = {
            "plate_number":vehicle_model.plate_number,
            "brand":vehicle_model.brand,
            "model":vehicle_model.model,
            "color":vehicle_model.color,
            "year":vehicle_model.year,
            "client":client
        }
        
        self.vehicle_repo.add(vehicle)


    def remove_vehicle(self, vehicle_model: VehicleModel) -> None:
        vehicle: Vehicle = self.vehicle_repo.get_vehicle(vehicle_model.plate_number)

        self.vehicle_repo.remove(vehicle)


    def update_vehicle(self, vehicle_model: VehicleModel) -> None:
        vehicle: Vehicle = self.vehicle_repo.get_vehicle(vehicle_model.plate_number)

        self.vehicle_repo.update(vehicle.vehicle_id, vehicle_model)
        

        

    


    