using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector2 minmaxSpeed = new Vector2(0.5f, 1.5f);
    Animator anim;
    List<Transform> wayPoints = new List<Transform>();
    DraggableRoom liveRoom;
    public void Init(DraggableRoom d)
    {
        liveRoom = d;
        wayPoints.Add(d.transform);
        agent.SetDestination(wayPoints[0].position);
    }
    // Start is called before the first frame update
    void Awake()
    {

        anim = GetComponentInChildren<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    void Update()
    {
        anim.SetFloat("Walk", agent.velocity.magnitude);
    }


    public float RandomSpeed()
    {
        return Random.Range(minmaxSpeed.x, minmaxSpeed.y);
    }
}
