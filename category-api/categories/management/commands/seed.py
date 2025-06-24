from django.core.management.base import BaseCommand
from categories.models import Category
from django.db import transaction

class Command(BaseCommand):
    def handle(self, *args, **kwargs):
        try:
            with transaction.atomic():
                self.stdout.write("Deleting existing categories...")
                Category.objects.all().delete()

                # Step 2: Create new entries
                new_categories = [
                    Category(id='809e7a3c-2895-41ea-9961-f375b12a1d41', name='Electronics', description='Gadgets and devices'),
                    Category(id='f64fdea0-fcc4-4383-b9bd-747096c8e079', name='Books', description='Printed and eBooks'),
                    Category(id='3ae9cf40-03a8-44e0-a49c-fea236ded24f', name='Clothing', description='Wearable items'),
                    Category(id='9e45b88a-6811-4d94-b00f-57e74d5a15ba', name='Phones', description='Portable devices')
                ]

                Category.objects.bulk_create(new_categories)
                self.stdout.write(self.style.SUCCESS(f' Seeded {len(new_categories)} categories.'))

        except Exception as e:
            self.stderr.write(self.style.ERROR(f" Error while seeding: {str(e)}"))
