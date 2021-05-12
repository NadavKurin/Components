#pragma once 
#include <string>
#include <vector>
#include "Person.h"

class Person;
class FlowersBouquet;

class FlowerArranger : public Person
{
private:

public:
    FlowerArranger(std::string);
    void arrangeFlowers(FlowersBouquet*);
    std::string getName();
};