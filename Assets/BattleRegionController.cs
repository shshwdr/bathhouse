using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRegionController : MonoBehaviour
{
    List<Monster> enemies = new List<Monster>();
    public bool isActive = false;
    public void addEnemy(GameObject go)
    {
        var monster = go.GetComponent<Monster>();
        monster.battleRegion = this;
        enemies.Add(monster);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {

        isActive = true;
        foreach(var enemy in enemies)
        {
            enemy.activate();
        }
    }

    public void deactivate()
    {

        isActive = false;
        foreach (var enemy in enemies)
        {
            enemy.deactivate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<Player>())
        {
            deactivate();
        }
    }
}
