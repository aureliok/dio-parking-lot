from django.db import models
from abc import ABC, abstractmethod

class Client(models.Model):
    client_id = models.AutoField(primary_key=True)
    first_name = models.CharField(max_length=50)
    last_name = models.CharField(max_length=100)
    phone_number = models.CharField(max_length=20)
    email = models.EmailField(blank=True, null=True)
    registration_date = models.DateTimeField(auto_now_add=True)

    class Meta:
        db_table = 'clients'

    def __str__(self) -> str:
        return f'{self.first_name} {self.last_name}'
    

class ClientModel(ABC):
    @property
    @abstractmethod
    def first_name(self):
        pass

    @property
    @abstractmethod
    def last_name(self):
        pass

    @property
    @abstractmethod
    def phone(self):
        pass

    @property
    @abstractmethod
    def email(self):
        pass
