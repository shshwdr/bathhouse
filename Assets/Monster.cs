using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterMoveMode { wander, followPlayer}
public enum MonsterAttackMode { noAttack,melee, ranged }
public class Monster : HPObjectController
{
    public MonsterMoveMode moveMode;
    public MonsterAttackMode attackMode;
    NavMeshAgent agent;
    Transform player;
    public float attackRange = 3;
    public BattleRegionController battleRegion;

    bool isAttacking = false;

    // Start is called before the first frame update


    protected override void Start()
    {
        base.Start();

        agent = GetComponentInChildren<NavMeshAgent>();
        player = GameObject.FindObjectOfType<Player>().transform;

        if (battleRegion.isActive)
        {
            activate();
        }
    }
    bool isStatic()
    {
        return isDead || !isActive;
    }
    protected override void Update()
    {
        base.Update();
        if (isDead)
        {
            return;
        }
        if (isAttacking)
        {
            return;
        }
        if (isInsideAttackRange())
        {
            startAttack();
            return;
        }
        if (isActive)
        {

            agent.SetDestination(player.position);

            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    bool isInsideAttackRange()
    {
        if(Utils.distanceWithoutY(player.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        return false;
    }

    void startAttack()
    {
        if (isStatic())
        {
            return;
        }
        animator.SetTrigger("attack");
        isAttacking = true;
    }

    public void finishAttack()
    {

        isAttacking = false;

        if (isStatic())
        {
            return;
        }
        if (isInsideAttackRange())
        {
            player.GetComponent<Player>().getDamage();
        }
    }

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("die");
        //Destroy(gameObject);
    }

    public void finishDie()
    {
        Destroy(gameObject);
    }
}
