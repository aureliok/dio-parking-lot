from abc import ABC, abstractmethod
from models import Activity
from typing import List

class BaseRepository(ABC):
    @abstractmethod
    def add(self, data: Activity) -> None:
        pass

    def remove(self, activity_id: int) -> None:
        pass

    def update(self, data: Activity) -> None:
        pass

    def get_by_id(self, id: int) -> Activity:
        pass

    def get_by_platenumber(self, plate_number: str, parkinglot_name: str) -> Activity:
        pass

    def get_by_parkinglot(self, id: int) -> List[Activity]:
        pass

    def get_by_client(self, id: int) -> List[Activity]:
        pass

    def get_by_client_name(self, first_name: str, last_name: str) -> List[Activity]:
        pass