from ..models import Client, ClientModel
from vehicles.models import Vehicle
from ..repositories.client_repository import ClientRepository
from typing import List
from django.http import Http404

class ClientService:
    def __init__(self):
        self.client_repo = ClientRepository()

    def add_client(self, client_model: ClientModel) -> None:
        client = { 
                "first_name":client_model.first_name,
                "last_name":client_model.last_name,
                "phone_number":client_model.phone_number,
                "email":client_model.email 
                }
        
        self.client_repo.add(client)


    def remove_client(self, client_model: ClientModel) -> None:
        client: Client = self.client_repo.get_by_fullname(firstname=client_model.first_name,
                                                  lastname=client_model.last_name)
        
        self.client_repo.remove(client)


    def update_client(self, client_model: ClientModel) -> None:
        client: Client = self.client_repo.get_by_fullname(firstname=client_model.first_name,
                                                  lastname=client_model.last_name)

        self.client_repo.update(client.client_id, client_model)


    def get_vehicles_client(self, first_name: str, last_name: str) -> List[Vehicle]:
        client: Client = self.client_repo.get_by_fullname(firstname=first_name,
                                                          lastname=last_name)
        if client is None:
            raise Http404('client not found')
        
        return self.client_repo.get_vehicles_of_client(client.client_id)

    