using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlecz : Roslina
{
    public override void akcja(Swiat swiat)
    {
        for (int i = 0; i < 3; i++)
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
    }
}
