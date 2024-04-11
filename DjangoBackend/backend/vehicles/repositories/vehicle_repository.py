from ..models import Vehicle, VehicleModel
from clients.models import Client
from clients.repositories.client_repository import ClientRepository
from django.http import Http404
from typing import List
STATIC_ATTR: List[str] = ['vehicle_id', 
                          'plate_number', 
                          'brand', 
                          'model', 
                          'year', 
                          'client_firstname', 
                          'client_lastname']

class VehicleRepository:
    def add(self, data: dict) -> None:
        Vehicle.objects.create(**data)

    def remove(self, data: Vehicle) -> None:
        try:
            vehicle: Vehicle = Vehicle.objects.get(pk=data.vehicle_id)
            vehicle.delete()
        except Vehicle.DoesNotExist:
            raise Http404(f'Vehicle with id {data.vehicle_id} does not exist')
        
    def update(self, id: int, data: VehicleModel) -> None:
        try:
            vehicle: Vehicle = Vehicle.objects.get(pk=id)
            
            for attr, value in data.__dict__.items():
                if attr not in STATIC_ATTR and value is not None:
                    setattr(vehicle, attr, value)

            if any('client' in attr for attr in data.__dict__.keys()):
                client: Client = ClientRepository().get_by_fullname(
                    data.client_firstname, data.client_lastname
                    )
                
                if not client:
                    raise Http404('client not found')
                
                vehicle.client = client

            vehicle.save()
        except Vehicle.DoesNotExist:
            raise Http404(f'Vehicle with id {id} does not exist')
        
    def get_vehicle(self, plate_number: str) -> Vehicle:
        try:
            vehicle: Vehicle = Vehicle.objects.get(plate_number=plate_number)
            return vehicle
        except Vehicle.DoesNotExist:
            raise Http404(f'Vehicle with plate number {plate_number} does not exist')
