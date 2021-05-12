#pragma once
#include <string>
#include <vector>

#include "FlowersBouquet.h"
#include "Person.h"

class Gardener : public Person
{
private:
public:
	Gardener();
	FlowersBouquet prepareBouquet(std::vector<std::string> order);
	std::string getName();
};

