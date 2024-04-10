from django.db import models
from clients.models import Client

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