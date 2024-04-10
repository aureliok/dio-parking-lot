from abc import ABC, abstractmethod
from models import ParkingLot
from vehicles.models import Vehicle
from typing import List

class BaseRepository(ABC):
    @abstractmethod
    def add(self, data: ParkingLot) -> None:
        pass

    def remove(self, parking_lot_id: int) -> None:
        pass

    def update(self, data: ParkingLot) -> None:
        pass

    def get_by_id(self, id: int) -> ParkingLot:
        pass

    def get_by_name(self, name: str) -> ParkingLot:
        pass

    def get_vehicles_on_parkinglot(self, id: int) -> List[Vehicle]:
        pass