from models import ParkingLot
from vehicles.models import Vehicle
from activities.models import Activity
from typing import List
from django.http import Http404
STATIC_ATTR: List[str] = ['parking_lot_id', 'name']

class ParkingLotRepository:
    def add(self, data: ParkingLot) -> None:
        ParkingLot.objects.create(**data)

    def remove(self, parking_lot_id: int) -> None:
        try:
            parking_lot: ParkingLot = ParkingLot.objects.get(pk=parking_lot_id)
            parking_lot.delete()
        except ParkingLot.DoesNotExist:
            raise Http404(f'Parking Lot with id {parking_lot} does not exist')
        
    def update(self, data: ParkingLot) -> None:
        try:
            parking_lot: ParkingLot = ParkingLot.object.get(pk=data.parking_lot_id)

            for attr, value in data.__dict__.items():
                if attr not in STATIC_ATTR and value is not None:
                    setattr(parking_lot, attr, value)

            parking_lot.save()
        except ParkingLot.DoesNotExist:
            raise Http404(f'Parking Lot with id {data.parking_lot_id} does not exist')
        
    def get_by_id(self, id: int) -> ParkingLot:
        try:
            return ParkingLot.objects.get(pk=id)
        except ParkingLot.DoesNotExist:
            raise Http404(f'Parking Lot with id {id} does not exist')
        
    def get_by_name(self, name: str) -> ParkingLot:
        try:
            parking_lot: ParkingLot = ParkingLot.objects.get(name=name)
            return parking_lot
        except:
            raise Http404(f'Parking Lot {name} does not exist')
        
    def get_vehicles_on_parkinglot(self, id: int) -> List[Vehicle]:
        vehicles_ids: List[int] = Activity.objects.filter(parking_lot_id=id, end_date__isnull=True) \
                                                    .values_list('vehicle_id', flat=True)

        return Vehicle.objects.filter(vehicle_id__in=vehicles_ids)
            