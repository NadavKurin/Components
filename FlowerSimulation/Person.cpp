#include "Person.h"
#include "Florist.h"


Person::Person(std::string name) : name(name)
{}

std::string Person::getName()
{
	return name;
}

void Person::orderFlowers(Florist* florist, Person* person, std::vector<std::string> order)
{
	std::string flowers = " ";
	for(auto& elem : order)
	{
		flowers = flowers + elem + ", ";
	}
	std::cout << getName() << " orders flowers to " << person->getName() << " from " << florist->getName() <<":" << flowers << std::endl;
	florist->acceptOrder(person, order);
}

void Person::acceptFlower(FlowersBouquet* flowersBouquet)
{ 
	std::string output ="";
	for (auto& elem : flowersBouquet->getBouquet())
	{
		output = output + elem + ", ";
	}
	std::cout << getName() <<" accepts the flowers: " << output << std::endl;
}