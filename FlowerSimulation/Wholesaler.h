#pragma once
#include "Person.h"

class Grower;

class Wholesaler : public Person
{
private:
	Grower* grower;
public:
	Wholesaler(Grower* g);
	void acceptOrder(std::vector<std::string> order);
	std::string getName();
};

