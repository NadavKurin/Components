#include "Florist.h"

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
}

std::string Florist::getName() {
	return "Florist " + Person::getName();
}