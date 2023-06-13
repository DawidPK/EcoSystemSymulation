using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leniwiec : Zwierze
{
    int lastMoved = 1;

    public override void akcja(Swiat swiat)
    {
        if(lastMoved == 0)
        {
            lastMoved++;
            return;
        }
        bool hasFoundPlace = false;
        while (hasFoundPlace == false)
        {
            Vector3 dir = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
            Vector3 nPos = transform.position + dir;
            Vector2 cords = new Vector2(nPos.x, nPos.z);
            //Czy pole istnieje + czy się w ogóle rusza
            if(swiat.isOutsideMap(cords) == false && dir != Vector3.zero )
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
        lastMoved = 0;

    }
}
