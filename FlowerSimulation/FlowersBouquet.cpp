#include "FlowersBouquet.h"

FlowersBouquet::FlowersBouquet(std::vector<std::string> b)
{
	bouquet = b;
	is_arranged = false;
}

void FlowersBouquet::arrange()
{
	is_arranged = true;
}
