from django.db import models

class ParkingLot(models.Model):
    parking_lot_id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=35, unique=True)
    address = models.CharField(max_length=150)
    price_first_hour = models.DecimalField(max_digits=10, decimal_places=2)
    price_additional_hour = models.DecimalField(max_digits=10, decimal_places=2)
    

    class Meta:
        db_table = 'parking_lots'

    def __str__(self) -> str:
        return f'{self.name}'
    

class ParkingLotModel:
    def __init__(self, **kwargs):
        self.name = kwargs.get('name')
        self.address = kwargs.get('address')
        self.price_first_hour = kwargs.get('price_first_hour')
        self.price_additional_hour = kwargs.get('price_additional_hour')

    def to_dict(self):
        return {
            'name': self.name,
            'address': self.address,
            'price_first_hour': self.price_first_hour,
            'price_additional_hour': self.price_additional_hour
        }