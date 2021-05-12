#include "Florist.h"
#include "Wholesaler.h"
#include "Person.h"

Florist::Florist(std::string name, Wholesaler* w, FlowerArranger* f, DeliveryPerson* d) : Person(name)
{
	wholesaler = w;
	flowerArranger = f;
	deliveryPerson = d;
}

void Florist::acceptOrder(Person* person, std::vector<std::string> order)
{
	// TODO
	std::cout << getName() << " forwards request to Wholesaler " << wholesaler->getName() << std::endl;
	FlowersBouquet* flowersBouquet = wholesaler->acceptOrder(order);
	flowerArranger->arrangeFlowers(flowersBouquet);
	std::cout << flowerArranger->getName() << " returns arranged flowers to " << getName() << std::endl;
	std::cout << getName() << " forwards flowers to " << deliveryPerson->getName() << std::endl;
	deliveryPerson->deliver(person, flowersBouquet);
}

std::string Florist::getName() {
	return "Florist " + Person::getName();
}