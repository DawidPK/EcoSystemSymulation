using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roslina : Organizm
{
    private void Start()
    {
        rysowanie();
    }
    public override void akcja(Swiat swiat)
    {
        int chance = Random.Range(0,100);
        if(chance > 70)
        {
            List<Vector2> freeSpaces = swiat.GenFreeSpaces(polozenie);
            if(freeSpaces.Count == 0){return;}
            spawnOffspring(freeSpaces[Random.Range(0, freeSpaces.Count)]);
        }else{
            return;
        }
    }
    public override void kolizja(Organizm collisonWith, Swiat swiat)
    {
        return;
    }
    public override void rysowanie()
    {
        polozenie = new Vector2(transform.position.x, transform.position.z);
        string gName = $"Plant{polozenie.x}:{polozenie.y}";
        transform.name = gName;
        swiat = GameObject.Find("Board").GetComponent<Swiat>();
        string name = $"Cell{transform.position.x}:{transform.position.z}";
    }
    public void spawnOffspring(Vector2 pos)
    {
        Vector3 nPos = new Vector3(pos.x, 0, pos.y);
        string gName = $"Plant{pos.x}:{pos.y}";
        var offspring = Instantiate(this.gameObject, nPos, Quaternion.identity);
        swiat.animalsSpawned.Add(offspring.GetComponent<Organizm>());
    }
}
