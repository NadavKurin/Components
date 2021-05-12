// FlowerSimulation.cpp : This file contains the 'main' function. Program execution begins and ends there.
//


#include <iostream>
#include "Person.h"
#include "Florist.h"
#include "Gardener.h"
#include "Grower.h"
#include "Wholesaler.h"

int main()
{
    Gardener* garett = new Gardener("Garret");
    Grower* gray = new Grower("Gray", garett);
    Wholesaler* watson = new Wholesaler("Watson", gray);
    FlowerArranger* flora = new FlowerArranger("Flora");
    DeliveryPerson* dylan = new DeliveryPerson("Dylan");
    Florist* fred = new Florist("Fred",watson,flora,dylan);
    Person* chris = new Person("Chris");
    Person* robin = new Person("Robin");
    std::vector<std::string> order = { "Roses", "Violets", "Gladiolus" };

    chris->orderFlowers(fred, robin, order);

    delete fred;
    delete chris;
    delete robin;
    delete dylan;
    delete flora;
    delete watson;
    delete gray;
    delete garett;
}


// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
