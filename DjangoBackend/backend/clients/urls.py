from django.urls import path
from .views import new_client, delete_client, update_client, get_vehicles_client


urlpatterns = [
    path('new-client', new_client, name='new-client'),
    path('delete-client', delete_client, name='delete-client'),
    path('update-client', update_client, name='update-client'),
    path('vehicles-client', get_vehicles_client, name='vehicles-client'),
]