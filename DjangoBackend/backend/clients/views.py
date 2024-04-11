from django.http import JsonResponse
from django.views.decorators.http import require_POST, require_http_methods
from django.views.decorators.csrf import csrf_exempt
from .models import ClientModel
from .services.client_service import ClientService

import json

@csrf_exempt
@require_POST
def new_client(request):
    data = json.loads(request.body.decode('utf-8'))

    model = ClientModel(**data)

    service = ClientService()
    service.add_client(model)

    return JsonResponse({'message': 'Client has been added succesfully'}, status=201)


@csrf_exempt
@require_http_methods(['DELETE'])
def delete_client(request):
    data = json.loads(request.body.decode('utf-8'))
    model = ClientModel(**data)
    service = ClientService()
    service.remove_client(model)

    return JsonResponse({'message': 'Client lot has been removed succesfully'}, status=201)


@csrf_exempt
@require_http_methods(['PUT'])
def update_client(request):
    data = json.loads(request.body.decode('utf-8'))
    model = ClientModel(**data)
    service = ClientService()
    service.update_client(model)

    return JsonResponse({'message': 'Client has been updated succesfully'}, status=201)


@csrf_exempt
@require_http_methods(['GET'])
def get_vehicles_client(request):
    firstname = request.GET.get('firstName')
    lastname = request.GET.get('lastName')
    service = ClientService()
    list_vehicles =  service.get_vehicles_client(firstname, lastname)

    vehicles = [
        {
            'fullName': f'{firstname} {lastname}',
            'plateNumber': vehicle.plate_number,
            'brand': vehicle.brand,
            'model': vehicle.model,
            'color': vehicle.color,
            'year': vehicle.year
        }
        for vehicle in list_vehicles
    ]

    return JsonResponse(vehicles, safe=False)