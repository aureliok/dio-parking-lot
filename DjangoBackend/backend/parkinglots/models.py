from django.db import models

class ParkingLot(models.Model):
    parking_lot_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=35)
    address = models.CharField(max_length=150)
    price_first_hour = models.DecimalField(max_digits=10, decimal_places=2)
    price_additional_hour = models.DecimalField(max_digits=10, decimal_places=2)
    

    def __str__(self) -> str:
        return f'{self.name}'
