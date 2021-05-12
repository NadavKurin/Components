#pragma once
#include "FlowersBouquet.h"
#include "Person.h"

class Grower;

class Wholesaler : public Person
{
private:
	Grower* grower;
public:
	Wholesaler(Grower* g);
	FlowersBouquet* acceptOrder(std::vector<std::string> order);
	std::string getName();
};

