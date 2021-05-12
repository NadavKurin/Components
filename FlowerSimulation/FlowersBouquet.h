#pragma once 
#include <string>
#include <vector>



class FlowersBouquet
{
private:
    bool is_arranged;
    std::vector<std::string> bouquet;
public:
    FlowersBouquet(std::vector<std::string>);
    void arrange();
    std::vector<std::string> getBouquet();
};