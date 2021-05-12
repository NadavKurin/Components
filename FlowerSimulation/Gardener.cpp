#include "Gardener.h"

#include "Person.h"

Gardener::Gardener(std::string name) : Person(name)
{
}


FlowersBouquet* Gardener::prepareBouquet(std::vector<std::string> order)
{
	std::cout << getName() << " Prepares flowers" << "." << std::endl;
	return new FlowersBouquet(order);

}

std::string Gardener::getName()
{
	return "Gardener " + Person::getName();
}