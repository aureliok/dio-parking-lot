from django.urls import path
from .views import new_activity, end_activity, get_list_activities_parkinglot


urlpatterns = [
    path('new-activity', new_activity, name='new-activity'),
    path('end-activity', end_activity, name='end-activity'),
    path('activities-parking-lot', get_list_activities_parkinglot, name='activities-parking-lot'),
]