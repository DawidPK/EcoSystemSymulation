using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Swiat : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] GameObject[] animalsToSpawn = new GameObject[8];
    [SerializeField] Vector2 size;
    [SerializeField] int numOfCopies;
    public List<Organizm> animalsSpawned = new List<Organizm>();
    string[] names = { "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Henry", "Isabella", "Jack", "Katherine", "Liam", "Mia", "Noah", "Olivia", "Peter", "Quinn", "Ryan", "Sophia", "Thomas" };


    public void wykonajTure()
    {
        StartCoroutine(Tura());
    }

    IEnumerator Tura()
    {
        List<Organizm> animalsCopy = new List<Organizm>();
        foreach (var animal in animalsSpawned)
        {
            animalsCopy.Add(animal);
        }
        animalsCopy.OrderByDescending(animal=> animal.inicjatywa).ToList();
        foreach (var Or in animalsCopy)
        {
            if(Or == null){continue;}
            if (Or.TryGetComponent<Organizm>(out Organizm zw))
            {
                zw.akcja(this);
                //yield return new WaitForSeconds(0.25f);
            }
        }
        animalsSpawned.RemoveAll(obj => obj == null);
        RemoveDuplicates();
        yield return null;
    }

    private void RemoveDuplicates()
    {
        Dictionary<Organizm, int> counts = new Dictionary<Organizm, int>();
        foreach (Organizm item in animalsSpawned)
        {
            if (counts.ContainsKey(item))
            {
                counts[item]++;
            }
            else
            {
                counts[item] = 1;
            }
        }
        foreach (var item in animalsSpawned)
        {
            if(counts[item] > 1)
            {
                deleteCopies(item, counts[item]);
            }
        }
    }
    private void deleteCopies(Organizm O, int numOfCopies)
    {
        if(O is Zwierze){return;}
        string name = O.transform.name;
        Debug.Log(name);
        for (int i = 0; i < numOfCopies; i++)
        {
            Destroy(GameObject.Find(name));
        }
    }
    private void rysujSwiat()
    {
        for(int j = 0; j < (int)size.y; j++)
        {
            for(int i = 0; i < (int)size.x; i++)
            {
                var cell = Instantiate(cellPrefab, new Vector3(i, -0.15f, j), Quaternion.identity);
                string name = $"Cell{i}:{j}";
                cell.transform.name = name;
            }
        }
        for (int g = 0; g < numOfCopies; g++)
        {
            spawnObjects();
        }
        
    }
    private void spawnObjects()
    {
        for (int i = 0; i < animalsToSpawn.Length; i++)
        {
            int high = 1;
            int x = (int)Random.Range(0, size.x);
            int y =  (int)Random.Range(0, size.y);
            Vector2 cords = new Vector2(x, y);
            if(isOutsideMap(cords) || isOnCords(new Vector2(x, y)))
            {
                i -= 1;
            }else
            {
                if(animalsToSpawn[i] == null){return;}
                if(animalsToSpawn[i].GetComponent<Organizm>() is Roslina)
                {
                    high = 0;
                }
                var organizmToSpawn = Instantiate(animalsToSpawn[i], new Vector3(x, high, y), Quaternion.identity);
                string name = organizmToSpawn.GetComponent<Organizm>().nazwaGatunku + " " + names[Random.Range(0, names.Length)];
                organizmToSpawn.transform.name = name;
                animalsSpawned.Add(organizmToSpawn.GetComponent<Organizm>());
            }
            

        }
    }
    private void Start()
    {
        cellPrefab = Resources.Load("Prefabs/Cell") as GameObject;
        //animalsToSpawn[0] = Resources.Load("Prefabs/Wilk") as GameObject;
        rysujSwiat();
    }
    public bool isOnCords(Vector2 cords)
    {
        foreach (var animal in animalsSpawned)
        {
            if(animal == null){continue;}
            if (animal.polozenie == cords)
            {
                return true;
            }
        }
        return false;
    }
    public bool isPlanted(Vector2 cords)
    {
        string name = $"Cell{cords.x}:{cords.y}";
        var cell = GameObject.Find(name);
        if(cell.GetComponent<CellScript>().hasPlantOnIt == true)
        {
            return true;
        }else{
            return false;
        }
    }
    public Organizm objectOnCords(Vector2 cords)
    {
        foreach (var animal in animalsSpawned)
        {
            if (animal.polozenie == cords)
            {
                return animal;
            }
        }
        return null;
    }
    public bool isOutsideMap(Vector2 cords)
    {
        string name = $"Cell{cords.x}:{cords.y}";
        var cell = GameObject.Find(name);
        if(cell == null)
        {
            return true;
        }else
        {
            return false;
        }
    }
    public List<Vector2> GenFreeSpaces(Vector2 pos)
    {
        List<Vector2> freeSpaces = new List<Vector2>();
        
            for (int i = -1; i <= 1; i++)
            {
                for (int j= -1; j <= 1; j++)
                {
                    if(i == 0 && j == 0){continue;}
                    var Places = animalsSpawned.Where(animal => animal.polozenie == new Vector2(pos.x +i, pos.y + j));
                    
                    if(!(Places.Any()) && !isOutsideMap(new Vector2(pos.x +i, pos.y + j)))
                    {
                        freeSpaces.Add(new Vector2(pos.x + i, pos.y + j));
                    }
                }
            }
            return freeSpaces;
    }


}
