from django.urls import path
from .views import new_parkinglot, delete_parkinglot, update_parkinglot, get_vehicles_parkinglot


urlpatterns = [
    path('new-parking-lot/', new_parkinglot, name='new-parking-lot'),
    path('delete-parking-lot/', delete_parkinglot, name='delete-parking-lot'),
    path('update-parking-lot/', update_parkinglot, name='update-parking-lot'),
    path('vehicles-parkinglot/', get_vehicles_parkinglot, name='vehicles-parkinglot'),
]