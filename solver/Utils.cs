namespace MatCom.Examen;

public class CityNode{
    public List<(int, CityNode)> roads {get; set;}
    public CityNode? teleport {get; set;}
    
    public CityNode(CityNode? teleport = null, List<(int, CityNode)>? roads = null){
        this.roads = (roads is null) ? new() : roads;
        this.teleport = teleport;
    }

    public (int, CityNode)[] Roads(){
        return roads.ToArray();
    }

    public (bool, CityNode?) HasTeleport(){
        return (teleport != null, teleport);
    }

    public void AddTeleport(CityNode? target){
        this.teleport = target;
    }

    public void DropTeleport(){
        this.teleport = null;
    }
}