#include "Wholesaler.h"

#include "Grower.h"


Wholesaler::Wholesaler(Grower* g) : Person(name)
{
	grower = g;
}
void Wholesaler::acceptOrder(std::vector<std::string> order)
{
	std::cout << getName() << " forwards the request to " << grower->getName() << std::endl;
	grower->prepareOrder(order);
}
std::string Wholesaler::getName() {
	return "Wholesaler " + Person::getName();
}