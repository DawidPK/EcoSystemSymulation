using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lis : Zwierze
{
    public override void akcja(Swiat swiat)
    {
        int i = 0;
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
                    if(swiat.objectOnCords(cords).sila > sila && i <= 8)
                    {
                        continue;
                    }
                    Organizm newOrg = swiat.objectOnCords(cords);
                    if(newOrg == this)
                    {
                        return;
                    }else if( i >= 8)
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
}
