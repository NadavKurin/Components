#pragma once
#include <string>
#include <vector>

#include "FlowersBouquet.h"
#include "Person.h"

class Gardener : public Person
{
private:
public:
	Gardener(std::string name);
	FlowersBouquet* prepareBouquet(std::vector<std::string> order);
	std::string getName();
};

