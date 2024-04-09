from .base import BaseRepository
from models import Client
from django.http import Http404
from typing import List
STATIC_ATTR: List[str] = ['client_id', 'first_name', 'last_name', 'registration_date']

class ClientRepository(BaseRepository):
    def add(self, data: Client) -> None:
        Client.objects.create(**data)
    

    def remove(self, data: Client) -> None:
        try:
            client: Client = Client.objects.get(pk=data.client_id)
            client.delete()
        except Client.DoesNotExist:
            raise Http404(f'Client with id {data.client_id} does not exist')
        
    def update(self, data: Client) -> None:
        try:
            client: Client = Client.objects.get(pk=data.client_id)
            
            for attr, value in data.__dict__.items():
                if attr not in STATIC_ATTR and value is not None:
                    setattr(client, attr, value)

            client.save()
        except Client.DoesNotExist:
            raise Http404(f'Client with id {data.client_id} does not exist')


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


