#pragma once
#include <string>
#include <vector>

#include "FlowersBouquet.h"
#include "Person.h"
class Gardener;

class Grower : public Person
{
private:
	Gardener* gardener;
public:
	Grower(Gardener* g);
	FlowersBouquet prepareOrder(std::vector< std::string>);
	std::string getName();
};

