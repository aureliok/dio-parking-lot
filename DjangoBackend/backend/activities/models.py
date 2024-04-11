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
    

class ActivityModel:
    def __init__(self, **kwargs):
        self.parkinglot_activity_id = kwargs.get('parkinglot_activity_id')
        self.parkinglot_name = kwargs.get('parkinglot_name')
        self.client_firstname = kwargs.get('client_firstname')
        self.client_lastname = kwargs.get('client_lastname')
        self.vehicle_platenumber = kwargs.get('vehicle_platenumber')
        self.start_date = kwargs.get('start_date')
        self.end_date = kwargs.get('end_date')

    def to_dict(self):
        return {
            'parkinglot_activity_id': self.parkinglot_activity_id,
            'parkinglot_name': self.parkinglot_name,
            'client_firstName': self.client_firstname,
            'client_lastname': self.client_lastname,
            'vehicle_platenumber': self.vehicle_platenumber,
            'start_date': self.start_date,
            'end_date': self.end_date
        }


class ActivityViewModel:
    def __init__(self, **kwargs):
        self.parkinglot_activity_id = kwargs.get('parkinglot_activity_id')
        self.parkinglot_id = kwargs.get('parkinglot_id')
        self.parkinglot_name = kwargs.get('parkinglot_name')
        self.price_additional_hour = kwargs.get('price_additional_hour')
        self.price_first_hour = kwargs.get('price_first_hour')
        self.plate_number = kwargs.get('plate_number')
        self.client_firstname = kwargs.get('client_firstname')
        self.client_lastname = kwargs.get('client_lastname')
        self.start_date = kwargs.get('start_date')
        self.end_date = kwargs.get('end_date')
        self.parking_value = kwargs.get('parking_value')


    def to_dict(self):
        return {
            'parkingLotActivityId': self.parkinglot_activity_id,
            'parkingLotId': self.parkinglot_id,
            'parkingLotName': self.parkinglot_name,
            'pricePerAdditionalHour': self.price_additional_hour,
            'priceFirstHour': self.price_first_hour,
            'plateNumber': self.plate_number,
            'clientFirstName': self.client_firstname,
            'clientLastName': self.client_lastname,
            'startDate': self.start_date,
            'endDate': self.end_date,
            'parkingValue': self.parking_value
        }