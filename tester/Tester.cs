using MatCom.Examen;

namespace MatCom.Tester;

//quitar citynode

public class Tester : TesterBase<int, CityNode, long, long>
{
    private const int maxChildren = 5; //variable
    private const int maxDepth = 20; //variable
    private const int teleportRatio = 10; //variable
    private const int maxRoadDanger = 100; //variable

    public override CityNode InputGenerator(int seed, int arg)
    {
        Random random = new Random();
        List<CityNode> ExistingCityNodes = new List<CityNode>();
        CityNode RootCity = TreeGenerator(0, random.Next(1, maxDepth), new List<CityNode>(), random);
        TeleportGenerator(RootCity, ExistingCityNodes, random); 
        return  RootCity;
    }

    public override long OutputGenerator(CityNode input)
    {
        return Solution.MinDanger(input);
    }

    public override bool OutputChecker(CityNode input, long output, long expectedOutput)
    {
        return output == expectedOutput;
    }

    private CityNode TreeGenerator(int currDepth, int depth, List<CityNode> ExistingCityNodes, Random randInt){
        if(currDepth == depth){ //si esta en el fondo del arbol no puede tener hijos es hoja por default
            CityNode newCityNode = new CityNode();
            ExistingCityNodes.Add(newCityNode); //agrega el nodo a la lista de nodos existentes par aposible teleport
            
            return newCityNode;
        }
        else{
            List<(int, CityNode)> roads = new(); //lista de caminos del nodo
            
            int childsAmount = randInt.Next(0, maxChildren); //genera la cantidad de hijos que tendra el nodo
            
            for(int i = 0; i < childsAmount; i++){ //genera los hijos del nodo
                int roadDanger = randInt.Next(1, maxRoadDanger); //genera el peligro del camino
                CityNode newNode = TreeGenerator(currDepth+1, depth, ExistingCityNodes, randInt); //genera el nodo hijo
                roads.Add((roadDanger, newNode)); //agrega el camino a la lista de caminos del nodo
            }

            CityNode newCityNode = new CityNode(roads: roads); //genera el nodo con los caminos y el teleport
            ExistingCityNodes.Add(newCityNode); //agrega el nodo a la lista de nodos existentes par aposible teleport

            return newCityNode;
        }
    }

    private void TeleportGenerator(CityNode RootCity, List<CityNode> ExistingCityNodes, Random randInt){
        int teleportChance = randInt.Next(0, teleportRatio); //genera la probabilidad de que el nodo tenga un teleport
        int teleportIndex = randInt.Next(0, ExistingCityNodes.Count); //genera el indice del nodo al que se teleportara

        CityNode? teleport = (teleportChance % teleportRatio == 0 && RootCity.Roads().Length > 0) ? ExistingCityNodes[teleportIndex] : null; //genera el nodo al que se teleportara con un change 1/teleportRatio    
        if(RootCity != teleport){
            RootCity.AddTeleport(teleport);
        }
        
        foreach((int, CityNode) road in RootCity.Roads()){
            TeleportGenerator(road.Item2, ExistingCityNodes, randInt);
        }
    }
}   
