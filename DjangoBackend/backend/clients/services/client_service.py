from models import Client, ClientModel
from repositories.client_repository import ClientRepository


class ClientService:
    def __init__(self):
        self.client_repo = ClientRepository()

    def add_client(self, client_model: ClientModel) -> None:
        client: Client = Client(first_name=client_model.first_name,
                        last_name=client_model.last_name,
                        phone=client_model.phone,
                        email=client_model.email)
        
        self.client_repo.add(client)


    def remove_client(self, client_model: ClientModel) -> None:
        client: Client = self.client_repo.get_by_fullname(firstname=client_model.first_name,
                                                  lastname=client_model.last_name)
        
        self.client_repo.remove(client)


    def update_client(self, client_model: ClientModel) -> None:
        client: Client = self.client_repo.get_by_fullname(firstname=client_model.first_name,
                                                  lastname=client_model.last_name)

        self.client_repo.update(client.client_id, client_model)

    