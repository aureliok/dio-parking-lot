from django.http import JsonResponse
from django.views.decorators.http import require_POST, require_http_methods
from django.views.decorators.csrf import csrf_exempt
from django.core.serializers import serialize
from .models import ParkingLotModel
from .services.parkinglot_service import ParkingLotService

import json

@csrf_exempt
@require_POST
def new_parkinglot(request):
    data = json.loads(request.body.decode('utf-8'))
    model = ParkingLotModel(**data)
    service = ParkingLotService()
    service.add_parkinglot(model)

    return JsonResponse({'message': 'Parking lot has been added succesfully'}, status=201)


@csrf_exempt
@require_POST
def delete_parkinglot(request):
    data = json.loads(request.body.decode('utf-8'))
    model = ParkingLotModel(**data)
    service = ParkingLotService()
    service.remove_parkinglot(model)

    return JsonResponse({'message': 'Parking lot has been removed succesfully'}, status=201)


@csrf_exempt
@require_http_methods(['PUT'])
def update_parkinglot(request):
    data = json.loads(request.body.decode('utf-8'))
    model = ParkingLotModel(**data)
    service = ParkingLotService()
    service.update_parkinglot(model)

    return JsonResponse({'message': 'Parking lot has been updated succesfully'}, status=201)


@csrf_exempt
@require_http_methods(['GET'])
def get_vehicles_parkinglot(request):
    parkinglot_name = request.GET.get('parkingLotName')
    service = ParkingLotService()
    list_vehicles =  service.get_parkinglot_vehicles(parkinglot_name)

    vehicles = [
        {
            'vehicleId': vehicle.pk,
            'plateNumber': vehicle.plate_number,
            'brand': vehicle.brand,
            'model': vehicle.model,
            'color': vehicle.color,
            'year': vehicle.year,
            'clientId': vehicle.client.client_id
        }
        for vehicle in list_vehicles
    ]

    return JsonResponse(vehicles, safe=False)

    