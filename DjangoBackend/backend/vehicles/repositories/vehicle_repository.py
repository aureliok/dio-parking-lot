from models import Vehicle
from django.http import Http404
from typing import List
STATIC_ATTR: List[str] = ['vehicle_id', 'plate_number', 'brand', 'model', 'year']

class VehicleRepository:
    def add(self, data: Vehicle) -> None:
        Vehicle.objects.create(**data)

    def remove(self, data: Vehicle) -> None:
        try:
            vehicle: Vehicle = Vehicle.objects.get(pk=data.vehicle_id)
            vehicle.delete()
        except Vehicle.DoesNotExist:
            raise Http404(f'Vehicle with id {data.vehicle_id} does not exist')
        
    def update(self, id: int, data: Vehicle) -> None:
        try:
            vehicle: Vehicle = Vehicle.objects.get(pk=id)
            
            for attr, value in data.__dict__.items():
                if attr not in STATIC_ATTR and value is not None:
                    setattr(vehicle, attr, value)

            vehicle.save()
        except Vehicle.DoesNotExist:
            raise Http404(f'Vehicle with id {id} does not exist')
        
    def get_vehicle(self, plate_number: str) -> Vehicle:
        try:
            vehicle: Vehicle = Vehicle.objects.get(plate_number=plate_number)
            return Vehicle
        except Vehicle.DoesNotExist:
            raise Http404(f'Vehicle with plate number {plate_number} does not exist')
        

    def get_by_client(self, client_id: int) -> List[Vehicle]:
        try:
            vehicles: List[Vehicle] = Vehicle.objects.filter(client_id=client_id)
            return vehicles
        except Vehicle.DoesNotExist:
            raise Http404(f'Client with id {client_id} does not have any vehicles registered')
