from models import ParkingLot, ParkingLotModel
from vehicles.models import Vehicle
from activities.models import Activity
from repositories.parkinglot_repository import ParkingLotRepository
from typing import List
from django.http import Http404

class ParkingLotService:
    def __init__(self):
        self.parkinglot_repo = ParkingLotRepository()

    def add_parkinglot(self, parkinglot_model: ParkingLotModel) -> None:
        parking_lot: ParkingLot = ParkingLot(name=parkinglot_model.name,
                                             address=parkinglot_model.address,
                                             price_first_hour=parkinglot_model.price_first_hour,
                                             price_additional_hour=parkinglot_model.price_additional_hour)
        
        self.parkinglot_repo.add(parking_lot)


    def remove_parkinglot(self, parkinglot_model: ParkingLotModel) -> None:
        parking_lot: ParkingLot = self.parkinglot_repo.get_by_name(parkinglot_model.name)

        self.parkinglot_repo.remove(parking_lot.parking_lot_id)


    def update_parkinglot(self, parkinglot_model: ParkingLotModel) -> None:
        parking_lot: ParkingLot = self.parkinglot_repo.get_by_name(parkinglot_model.name) 

        self.parkinglot_repo.update(parking_lot.parking_lot_id, parkinglot_model)


    def get_parkinglot_vehicles(self, parkinglot_name: str) -> List[Vehicle]:
        parking_lot: ParkingLot = self.parkinglot_repo.get_by_name(parkinglot_name)

        if parking_lot is None:
            raise Http404('parking lot not found')

        return self.parkinglot_repo.get_vehicles_on_parkinglot(parking_lot.parking_lot_id)


    
