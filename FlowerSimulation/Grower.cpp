#include "Grower.h"

#include "Gardener.h"
#include "Person.h"

Grower::Grower(Gardener* g): Person(name)
{
	gardener = g;
}

FlowersBouquet Grower::prepareOrder(std::vector<std::string> order)
{
	std::cout << getName() << " forwards the request to Gardener " << gardener->getName() << std::endl;
	return gardener->prepareBouquet(order);
}

std::string Grower::getName() {
	return "Grower " + Person::getName();
}