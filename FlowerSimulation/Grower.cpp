#include "Grower.h"

#include "Gardener.h"
#include "Person.h"

Grower::Grower(std::string name, Gardener* g): Person(name)
{
	gardener = g;
}

FlowersBouquet* Grower::prepareOrder(std::vector<std::string> order)
{
	std::cout << getName() << " forwards the request to " << gardener->getName() << std::endl;
	FlowersBouquet* flowers =  gardener->prepareBouquet(order);
	std::cout << gardener->getName() << " returns flowers to " << getName() << std::endl;
	return flowers;
}

std::string Grower::getName() {
	return "Grower " + Person::getName();
}