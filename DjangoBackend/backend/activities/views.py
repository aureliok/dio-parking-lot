from django.http import JsonResponse
from django.views.decorators.http import require_POST, require_http_methods
from django.views.decorators.csrf import csrf_exempt
from .models import ActivityModel
from parkinglots.models import ParkingLot
from .services.activity_service import ActivityService

import json

@csrf_exempt
@require_POST
def new_activity(request):
    data = json.loads(request.body.decode('utf-8'))
    model = ActivityModel(**data)
    service = ActivityService()
    service.add_activity(model)

    return JsonResponse({'message': 'Activity has been added succesfully'}, status=201)


@csrf_exempt
@require_http_methods(['PUT'])
def end_activity(request):
    data = json.loads(request.body.decode('utf-8'))
    model = ActivityModel(**data)
    service = ActivityService()
    activity = service.end_activity(model)

    return JsonResponse(activity.to_dict(), status=201)


@csrf_exempt
@require_http_methods(['GET'])
def get_list_activities_parkinglot(request):
    parkinglot_name = request.GET.get('parkingLotName')
    service = ActivityService()
    list_activities = service.get_parkinglot_activities(parkinglot_name)

    view_activities = [service.get_activity_view_model(act).to_dict() for act in list_activities]

    return JsonResponse(view_activities, safe=False)