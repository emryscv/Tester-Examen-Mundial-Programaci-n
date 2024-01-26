using MatCom.Examen;

namespace MatCom.Solver;

public static class Solution {
    public static long AuxMinDanger(CityNode CurrentCity, int amountRoads)
    {
        long currentDanger = int.MaxValue;
        if(CurrentCity.Roads().Count() == 0)
        {
            return 0;
        }

        if(CurrentCity.HasTeleport().Item1){
            currentDanger = Math.Min(AuxMinDanger(CurrentCity.HasTeleport().Item2, amountRoads), currentDanger);
        }
        else{
            foreach ((int, CityNode)road in CurrentCity.Roads())
            {
                long _aux = AuxMinDanger(road.Item2, amountRoads+1);
                currentDanger = Math.Min(road.Item1 * amountRoads + _aux, currentDanger);
            }
        }

        return currentDanger;
    }

    public static long MinDanger(CityNode RootCity)
    {
        return AuxMinDanger(RootCity, 1);
    }
} 