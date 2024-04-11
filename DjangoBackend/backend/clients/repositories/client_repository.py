from ..models import Client, ClientModel
from vehicles.models import Vehicle
from django.http import Http404
from typing import List
STATIC_ATTR: List[str] = ['client_id', 'first_name', 'last_name', 'registration_date']

class ClientRepository:
    def add(self, data: dict) -> None:
        Client.objects.create(**data)
    

    def remove(self, data: Client) -> None:
        try:
            client: Client = Client.objects.get(pk=data.client_id)
            client.delete()
        except Client.DoesNotExist:
            raise Http404(f'Client with id {data.client_id} does not exist')
        
    def update(self, id: int, data: ClientModel) -> None:
        try:
            client: Client = Client.objects.get(pk=id)
            
            for attr, value in data.__dict__.items():
                if attr not in STATIC_ATTR and value is not None:
                    setattr(client, attr, value)

            client.save()
        except Client.DoesNotExist:
            raise Http404(f'Client with id {id} does not exist')


    def get_by_id(self, id: int) -> Client:
        try:
            client: Client = Client.objects.get(pk=id)
            return Client
        except Client.DoesNotExist:
            raise Http404(f'Client with id {id} does not exist')
        
    
    def get_by_fullname(self, firstname: str, lastname: str) -> Client:
        try:
            client: Client = Client.objects.get(first_name=firstname, last_name=lastname)
            return client
        except Client.DoesNotExist:
            raise Http404(f'Client {firstname} {lastname} does not exist')
        

    def get_vehicles_of_client(self, client_id: int) -> List[Vehicle]:
        try:
            vehicles: List[Vehicle] = Vehicle.objects.filter(client_id=client_id)
            return vehicles
        except Vehicle.DoesNotExist:
            raise Http404(f'Client with id {client_id} does not have any vehicles registered')


