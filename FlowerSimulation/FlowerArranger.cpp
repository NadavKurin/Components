#include "FlowerArranger.h"
#include "Person.h"
#include "FlowersBouquet.h"

FlowerArranger::FlowerArranger(std::string name) : Person(name)
{
}

void FlowerArranger::arrangeFlowers(FlowersBouquet* flowersBouquet)
{
	// TODO
	std::cout << getName() << " arranges flowers" << "." << std::endl;
	flowersBouquet->arrange();
}

std::string FlowerArranger::getName() {
	return "Flower Arranger " + Person::getName();
}