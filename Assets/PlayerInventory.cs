using UnityEngine;

public class PlayerInventory {

    private int wood;


    public int Wood { get { return wood; } set { wood = value; } }
    public void AddWood(int wood)
    {
        Wood += wood;
    }
    public void ExtractWood(int wood)
    {
        Wood -= wood;
    }

    public PlayerInventory()
    {
        AddWood(5); // la nimereala , nush inca cate o sa ai la inceput, nici macar nu stiu ce faci cu ele :)) 
        // initializare cate lemne ai la inceput 
        // initializare restul de resurse 
    }
}
