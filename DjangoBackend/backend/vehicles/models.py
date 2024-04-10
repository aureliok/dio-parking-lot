from django.db import models
from clients.models import Client
from abc import ABC, abstractmethod

class Vehicle(models.Model):
    vehicle_id = models.AutoField(primary_key=True)
    plate_number = models.CharField(max_length=14)
    brand = models.CharField(max_length=20)
    model = models.CharField(max_length=20)
    color = models.CharField(max_length=15)
    year = models.IntegerField(null=True, blank=True)
    client = models.ForeignKey(Client, on_delete=models.CASCADE)

    class Meta:
        db_table = 'vehicles'


    def __str__(self) -> str:
        return f'{self.brand} {self.model} - {self.plate_number}'
    

class VehicleModel:
    def __init__(self, **kwargs):
        self.plate_number = kwargs.get('plate_number')
        self.brand = kwargs.get('brand')
        self.model = kwargs.get('model')
        self.color = kwargs.get('color')
        self.year = kwargs.get('year')
        self.client_firstname = kwargs.get('client_firstname')
        self.client_lastname = kwargs.get('client_lastname')

    def to_dict(self):
        return {
            'plate_number': self.plate_number,
            'brand': self.brand,
            'model': self.model,
            'color': self.color,
            'year': self.year,
            'client_firstname': self.client_firstname,
            'client_lastname': self.client_lastname
        }