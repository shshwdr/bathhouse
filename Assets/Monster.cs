using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterMoveMode { wander, followPlayer}
public enum MonsterAttackMode { noAttack,melee, ranged }
public class Monster : MonoBehaviour
{
    public MonsterMoveMode moveMode;
    public MonsterAttackMode attackMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
