namespace Xandudex.LifeGame
{
    internal record AnimalEat(Animal Animal);
    internal record AnimalStateChanged(Animal Animal);
    internal record AnimalSpawned(Animal Animal);
    internal record FoodSpawned(Food Food, Animal Animal);
}
