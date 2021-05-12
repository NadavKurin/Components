#include "Gardener.h"

#include "Person.h"

Gardener::Gardener() : Person(name)
{
}


FlowersBouquet Gardener::prepareBouquet(std::vector<std::string> order)
{
	std::cout << getName() << " Prepares flowers " << std::endl;
	

}

std::string Gardener::getName()
{
	return "Gardener " + Person::getName();
}