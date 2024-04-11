from django.urls import path
from .views import new_vehicle, delete_vehicle, update_vehicle


urlpatterns = [
    path('new-vehicle/', new_vehicle, name='new-vehicle'),
    path('delete-vehicle/', delete_vehicle, name='delete-vehicle'),
    path('update-vehicle/', update_vehicle, name='update-vehicle'),
]