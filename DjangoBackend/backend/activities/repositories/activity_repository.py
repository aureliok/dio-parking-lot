from .base_activity_repository import BaseRepository
from models import Activity
from vehicles.models import Vehicle
from parkinglots.models import ParkingLot
from clients.models import Client
from django.http import Http404
from typing import List
STATIC_ATTR: List[str] = ['activity_id', 'parking_lot', 'client', 'vehicle']

class BaseRepository(BaseRepository):
    def add(self, data: Activity) -> None:
        Activity.objects.create(**data)

    def remove(self, activity_id: int) -> None:
        try:
            activity: Activity = Activity.objects.get(pk=activity)
            activity.delete()
        except Activity.DoesNotExist:
            raise Http404(f'Activity with id {activity_id} does not exist')

    def update(self, data: Activity) -> None:
        try:
            activity: Activity = Activity.objects.get(pk=data.activity_id)

            for attr, value in data.__dict__.items():
                if attr not in STATIC_ATTR and value is not None:
                    setattr(activity, attr, value)

            activity.save()
        except Activity.DoesNotExist:
            raise Http404(f'Activity with id {data.activity_id} does not exist')

    def get_by_id(self, id: int) -> Activity:
        try:
            activity: Activity = Activity.objects.get(pk=id)
            return activity
        except Activity.DoesNotExist:
            raise Http404(f'Activity with id {id} does not exist')

    def get_by_platenumber(self, plate_number: str, parkinglot_name: str) -> Activity:
        try:
            vehicle: Vehicle = Vehicle.objects.get(plate_number=plate_number)
        except Vehicle.DoesNotExist:
            raise Http404(f'Vehicle with plate number {plate_number} does not exist')

        try:
            parking_lot: ParkingLot = ParkingLot.objects.get(name=parkinglot_name)
        except ParkingLot.DoesNotExist:
            raise Http404(f'Parking Lot {parkinglot_name} does not exist')
        
        activity: Activity = Activity.objects.filter(
            parking_lot_id=parking_lot,
            end_date__isnull=True,
            vehicle=vehicle
        ).select_related('client').order_by('-start_date').first()

        return activity


    def get_by_parkinglot(self, id: int) -> List[Activity]:
        try:
            parking_lot: ParkingLot = ParkingLot.objects.get(pk=id)
        except ParkingLot.DoesNotExist:
            raise Http404(f'Parking Lot with id {id} does not exist')
        
        try:
            activities: List[Activity] = Activity.objects.filter(parking_lot=parking_lot) \
                                            .select_related(
                                                'vehicle', 'parking_lot', 'client'
                                            ).order_by('-start_date')
            
            return activities
        except Activity.DoesNotExist:
            raise Http404(f'Activities on parking lot with id {id} does not exist')


    def get_by_client(self, id: int) -> List[Activity]:
        try:
            client: Client = Client.objects.get(pk=id)
        except Client.DoesNotExist:
            raise Http404(f'Client with id {id} does not exist')
        
        try:
            activities: List[Activity] = Activity.objects.filter(client=client)
            return activities
        except Activity.DoesNotExist:
            raise Http404(f'Activities of client with id {id} does not exist')

    def get_by_client_name(self, first_name: str, last_name: str) -> List[Activity]:
        try:
            client: Client = Client.objects.get(first_name=first_name, last_name=last_name)
        except Client.DoesNotExist:
            raise Http404(f'Client {first_name} {last_name} does not exist')
        

        try:
            activities: List[Activity] = Activity.objects.filter(client=client) \
                                        .select_related('vehicle', 'parking_lot', 'client')
            return activities
        except Activity.DoesNotExist:
            raise Http404(f'Activities of client with id {id} does not exist')