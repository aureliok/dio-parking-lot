from abc import ABC, abstractmethod
from models import Vehicle
from typing import List

class BaseRepository(ABC):
    @abstractmethod
    def add(self, data: Vehicle) -> None:
        pass

    def remove(self, data: Vehicle) -> None:
        pass

    def update(self, data: Vehicle) -> None:
        pass

    def get_vehicle(self, plate_number: str) -> Vehicle:
        pass

    def get_by_client(self, client_id: int) -> List[Vehicle]:
        pass