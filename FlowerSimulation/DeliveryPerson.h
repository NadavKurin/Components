#pragma once 
#include <string>
#include <vector>

#include "Person.h"
#include "FlowersBouquet.h"

class Person;

class DeliveryPerson : public Person
{
private:

public:
    DeliveryPerson(std::string);
    void deliver(Person*, FlowersBouquet*);
    std::string getName();
};