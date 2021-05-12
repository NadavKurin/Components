#pragma once 
#include <string>
#include <vector>
#include "FlowersBouquet.h"
#include "Person.h"

class DeliveryPerson : public Person
{
private:

public:
    DeliveryPerson(std::string);
    void deliver(Person*, FlowersBouquet*);
    std::string getName();
};