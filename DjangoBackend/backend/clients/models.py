from django.db import models


class Client(models.Model):
    client_id = models.AutoField(primary_key=True)
    first_name = models.CharField(max_length=50)
    last_name = models.CharField(max_length=100)
    phone_number = models.CharField(max_length=20, unique=True)
    email = models.EmailField(blank=True, null=True)
    registration_date = models.DateTimeField(auto_now_add=True)

    class Meta:
        db_table = 'clients'

    def __str__(self) -> str:
        return f'{self.first_name} {self.last_name}'
    

class ClientModel():
    def __init__(self, **kwargs):
        self.first_name = kwargs.get('firstName')
        self.last_name = kwargs.get('lastName')
        self.phone_number = kwargs.get('phone')
        self.email = kwargs.get('email')

    def to_dict(self):
        return {
            'first_name': self.first_name,
            'last_name': self.last_name,
            'phone_number': self.phone,
            'email': self.email
        }