from abc import ABC, abstractmethod
from models import Client

class BaseRepository(ABC):
    @abstractmethod
    def add(self, data: Client) -> None:
        pass

    def remove(self, data: Client) -> None:
        pass

    def update(self, data: Client) -> None:
        pass

    def get_by_id(self, id: int) -> Client:
        pass

    def get_by_fullname(self, firstname: str, lastname: str) -> Client:
        pass