#pragma once 
#include <string>
#include <vector>

#include "Person.h"
#include "Wholesaler.h"
#include "FlowerArranger.h"
#include "DeliveryPerson.h"

class Person;

class Florist : public Person
{
private:
    Wholesaler* wholesaler;
    FlowerArranger* flowerArranger;
    DeliveryPerson* deliveryPerson;

public:
    Florist(std::string, Wholesaler* w, FlowerArranger* f, DeliveryPerson* d);
    void acceptOrder(Person*, std::vector<std::string>);
    std::string getName();
};
