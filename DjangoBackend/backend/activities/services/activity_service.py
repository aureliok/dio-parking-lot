from ..models import Activity, ActivityModel, ActivityViewModel
from clients.models import Client
from clients.repositories.client_repository import ClientRepository
from parkinglots.models import ParkingLot
from parkinglots.repositories.parkinglot_repository import ParkingLotRepository
from vehicles.models import Vehicle
from vehicles.repositories.vehicle_repository import VehicleRepository
from ..repositories.activity_repository import ActivityRepository
from django.http import Http404
from typing import List
from datetime import datetime
from django.utils import timezone
from decimal import Decimal

class ActivityService:
    def __init__(self):
        self.activity_repo = ActivityRepository()
        self.client_repo = ClientRepository()
        self.parkinglot_repo = ParkingLotRepository()
        self.vehicle_repo = VehicleRepository()


    def add_activity(self, activity_model: ActivityModel):
        client: Client = self.client_repo.get_by_fullname(activity_model.client_firstname, activity_model.client_lastname)
        if client is None:
            raise Http404('client not found')
        
        parking_lot: ParkingLot = self.parkinglot_repo.get_by_name(activity_model.parkinglot_name)
        if parking_lot is None:
            raise Http404('parking lot not found')
        
        vehicle: Vehicle = self.vehicle_repo.get_vehicle(activity_model.vehicle_platenumber)
        if vehicle is None:
            raise Http404('vehicle not found')
        
        activity: Activity = self.get_activity_platenumber(activity_model.vehicle_platenumber)
        if activity is not None:
            raise Exception('vehicle is already parked')
        
        activity: Activity = Activity(parking_lot=parking_lot,
                                      client=client,
                                      vehicle=vehicle,
                                      )
        
        self.activity_repo.add(activity)

        

    def get_activity_platenumber(self, plate_number: str) -> Activity:
        vehicle: Vehicle = self.vehicle_repo.get_vehicle(plate_number)

        activity: Activity = Activity.objects.filter(
            vehicle=vehicle,
            end_date__isnull=True
        ).first()

        return activity


    def get_parkinglot_activities(self, parkinglot_name: str) -> List[Activity]:
        parking_lot: ParkingLot = self.parkinglot_repo.get_by_name(parkinglot_name)

        if parking_lot is None:
            raise Http404('parking lot not found')
        
        return self.activity_repo.get_by_parkinglot(parking_lot.parking_lot_id)
    

    def get_activity_view_model(self, data: Activity) -> ActivityViewModel:
        view_model: ActivityViewModel = ActivityViewModel(
            parkinglot_activity_id = data.activity_id,
            parkinglot_id = data.parking_lot.parking_lot_id,
            parkinglot_name = data.parking_lot.name,
            price_additional_hour = data.parking_lot.price_additional_hour,
            price_first_hour = data.parking_lot.price_first_hour,
            plate_number = data.vehicle.plate_number,
            client_firstname = data.client.first_name,
            client_lastname = data.client.last_name,
            start_date = data.start_date,
            end_date = data.end_date,
            parking_value = data.parking_value
        )

        return view_model
    

    def end_activity(self, activity_model: ActivityModel) -> ActivityViewModel:
        parking_lot: ParkingLot = self.parkinglot_repo.get_by_name(activity_model.parkinglot_name)

        if parking_lot is None:
            raise Http404('parking lot not found')
        
        if activity_model.parkinglot_activity_id is None or activity_model.parkinglot_activity_id == 0:
            vehicle: Vehicle = self.vehicle_repo.get_vehicle(activity_model.vehicle_platenumber)
            if vehicle is None:
                raise Http404('vehicle not found')
            
            activity: Activity = self.activity_repo.get_by_platenumber(vehicle.plate_number, parking_lot.name)

        else:
            activity: Activity = self.activity_repo.get_by_id(activity_model.parkinglot_activity_id)

        if activity_model.end_date is None:
            activity.end_date = datetime.now()
        else:
            activity.end_date = activity_model.end_date

        
        if not activity.end_date.tzinfo:
            activity.end_date = timezone.make_aware(activity.end_date, timezone=activity.start_date.tzinfo)

        duration = activity.end_date - activity.start_date
        additional_hours = Decimal(max((duration.total_seconds() / 3600) - 1, 0))

        activity.parking_value = parking_lot.price_first_hour + parking_lot.price_additional_hour * additional_hours

        self.activity_repo.update(activity)

        return self.get_activity_view_model(activity)

         
