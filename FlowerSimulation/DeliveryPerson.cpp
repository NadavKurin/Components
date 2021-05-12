#include "DeliveryPerson.h"

#include "Person.h"

DeliveryPerson::DeliveryPerson(std::string name) : Person(name)
{
}

void DeliveryPerson::deliver(Person* p, FlowersBouquet* f)
{
	// TODO
	std::cout << getName() << " delivers flowers " << p->getName() << std::endl;
}

std::string DeliveryPerson::getName() {
	return "Delivery Person " + Person::getName();
}