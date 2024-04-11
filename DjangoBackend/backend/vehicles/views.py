from django.http import JsonResponse
from django.views.decorators.http import require_POST, require_http_methods
from django.views.decorators.csrf import csrf_exempt
from .models import VehicleModel
from .services.vehicle_service import VehicleService

import json

@csrf_exempt
@require_POST
def new_vehicle(request):
    data = json.loads(request.body.decode('utf-8'))

    model = VehicleModel(**data)

    service = VehicleService()
    service.add_vehicle(model)

    return JsonResponse({'message': 'Vehicle has been added succesfully'}, status=201)


@csrf_exempt
@require_POST
def delete_vehicle(request):
    data = json.loads(request.body.decode('utf-8'))
    model = VehicleModel(**data)
    service = VehicleService()
    service.remove_vehicle(model)

    return JsonResponse({'message': 'Vehicle has been removed succesfully'}, status=201)


@csrf_exempt
@require_http_methods(['PUT'])
def update_vehicle(request):
    data = json.loads(request.body.decode('utf-8'))
    model = VehicleModel(**data)
    service = VehicleService()
    service.update_vehicle(model)

    return JsonResponse({'message': 'Vehicle has been updated succesfully'}, status=201)
