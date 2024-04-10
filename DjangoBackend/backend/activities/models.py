from django.db import models
from clients.models import Client
from vehicles.models import Vehicle
from parkinglots.models import ParkingLot

class Activity(models.Model):
    activity_id = models.AutoField(primary_key=True)
    parking_lot = models.ForeignKey(ParkingLot, on_delete=models.CASCADE)
    client = models.ForeignKey(Client, on_delete=models.CASCADE)
    vehicle = models.ForeignKey(Vehicle, on_delete=models.CASCADE)
    start_date = models.DateTimeField(auto_now_add=True)
    end_date = models.DateTimeField(blank=True, null=True)
    parking_value = models.DecimalField(max_digits=10, decimal_places=2, null=True, default=None)


    class Meta:
        db_table = 'activities'


    def __str__(self) -> str:
        return f'{self.activity}: {self.parking_lot} | {self.client} | {self.vehicle}'
