using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Organizm : MonoBehaviour
{
    public string nazwaGatunku;
    public int sila;
    public int inicjatywa;
    public Vector2 polozenie;
    public Swiat swiat;

    public abstract void akcja(Swiat swiat);
    public abstract void kolizja(Organizm collisonWith, Swiat swiat);
    public abstract void rysowanie();
    
}
