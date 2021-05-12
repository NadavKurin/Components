#pragma once 
#include <string>
#include <vector>

#include "Person.h"

class Person;

class Florist : public Person
{
private:

public:
    Florist(std::string);
    void acceptOrder(Person*, std::vector<std::string>);
    std::string getName();
};
