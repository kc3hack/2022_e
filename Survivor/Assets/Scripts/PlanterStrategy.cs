using UnityEngine;
public interface PlanterStrategy
{
    void Initialize();
    void Planting();
    Vector2 DefinePosition();
    void SetPlanter(ElectricPlanter planter);
}