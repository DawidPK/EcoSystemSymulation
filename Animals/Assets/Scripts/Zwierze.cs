using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zwierze : Organizm
{

    private void Start()
    {
        rysowanie();

    }
    public override void akcja(Swiat swiat)
    {
        bool hasFoundPlace = false;
        while (hasFoundPlace == false)
        {
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
        }else
        {
            fight(collisonWith);
        }
    }
    private void fight(Organizm enemy)
    {
        
        if(enemy == null || enemy is Kapybara){return;}
        if(enemy.sila > this.sila)
        {
            //Debug.Log(enemy.transform.name + " Wygrał walkę");
            Destroy(this.gameObject);
        }else
        {
            //Debug.Log(transform.name + " Wygrał walkę");
            transform.position = new Vector3(enemy.transform.position.x, 1 , enemy.transform.position.z);
            if(enemy.nazwaGatunku == "guarana")
            {
                Debug.Log("Nom nom guarana");
                this.sila += 3;
            }
            Destroy(enemy.gameObject);
        }
    }
    private void spawnOffspring(Vector2 pos)
    {
        Vector3 nPos = new Vector3(pos.x, 1, pos.y);
        var offspring = Instantiate(this.gameObject, nPos, Quaternion.identity);
        swiat.animalsSpawned.Add(offspring.GetComponent<Organizm>());
    }
    public override void rysowanie()
    {
        polozenie = new Vector2(transform.position.x, transform.position.z);
        swiat = GameObject.Find("Board").GetComponent<Swiat>();
    }
}
