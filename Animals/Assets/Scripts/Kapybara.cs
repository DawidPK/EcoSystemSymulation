using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Kapybara : Zwierze
{
    public override void akcja(Swiat swiat)
    {
        bool hasFoundPlace = false;
        while (hasFoundPlace == false)
        {
            foreach (var item in swiat.animalsSpawned)
            {
                if(item.polozenie == polozenie && item is Roslina)
                {
                    Destroy(item);
                }
            }
            Vector3 dir = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
            Vector3 nPos = transform.position + dir;
            Vector2 cords = new Vector2(nPos.x, nPos.z);
            //Czy pole istnieje + czy się w ogóle rusza
            
            if(swiat.isOutsideMap(cords) == false && dir != Vector3.zero)
            {
                //Czy ktoś jest na tym polu
                if(swiat.isOnCords(cords) == true){
                    Organizm newOrg = swiat.objectOnCords(cords);
                    if(newOrg == this)
                    {
                        return;
                    }
                    kolizja(newOrg, swiat);
                    return;
                }
                transform.position = nPos;
                hasFoundPlace = true;
            }
        }
        polozenie = new Vector2(transform.position.x, transform.position.z);
        if(transform.position.y == 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }
    public override void kolizja(Organizm collisonWith, Swiat swiat)
    {
        if(collisonWith == null){return;}
        if(collisonWith.nazwaGatunku == this.nazwaGatunku)
        {
            List<Vector2> freeSpaces = swiat.GenFreeSpaces(polozenie);
            if(freeSpaces.Count == 0){ return;}
            spawnOffspring(freeSpaces[Random.Range(0, freeSpaces.Count - 1)]);
        }
    }
    private void spawnOffspring(Vector2 pos)
    {
        Vector3 nPos = new Vector3(pos.x, 1, pos.y);
        var offspring = Instantiate(this.gameObject, nPos, Quaternion.identity);
        swiat.animalsSpawned.Add(offspring.GetComponent<Organizm>());
    }
}
