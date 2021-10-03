using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public List<PlayerAttack> playerAttacks;
    int currentAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        deselectAllAttack();
        playerAttacks[0].gameObject.SetActive(true);

    }

    void deselectAllAttack()
    {
        foreach(var attack in playerAttacks)
        {
            attack.gameObject.SetActive(false);
        }
    }
    public void selectNextAttack()
    {
        deselectAllAttack();
        currentAttack++;
        if (currentAttack >= playerAttacks.Count)
        {
            currentAttack = 0;
        }
        playerAttacks[currentAttack].gameObject.SetActive(true);
    }

    public void selectPreviousAttack()
    {
        deselectAllAttack();
        currentAttack--;
        if (currentAttack < 0)
        {
            currentAttack = playerAttacks.Count-1;
        }
        playerAttacks[currentAttack].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectNextAttack();
        }
    }
}
