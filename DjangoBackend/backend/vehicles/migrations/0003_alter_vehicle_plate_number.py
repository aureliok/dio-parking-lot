# Generated by Django 5.0.4 on 2024-04-10 20:46

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('vehicles', '0002_alter_vehicle_table'),
    ]

    operations = [
        migrations.AlterField(
            model_name='vehicle',
            name='plate_number',
            field=models.CharField(max_length=14, unique=True),
        ),
    ]
