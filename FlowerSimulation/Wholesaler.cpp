#include "Wholesaler.h"

#include "Grower.h"


Wholesaler::Wholesaler(Grower* g) : Person(name)
{
	grower = g;
}
FlowersBouquet* Wholesaler::acceptOrder(std::vector<std::string> order)
{
	std::cout << getName() << " forwards the request to " << grower->getName() << std::endl;
	FlowersBouquet* flowers = grower->prepareOrder(order);
	std::cout << grower->getName() << " returns flowers to " << getName() << std::endl;
	return flowers;
}
std::string Wholesaler::getName() {
	return "Wholesaler " + Person::getName();
}