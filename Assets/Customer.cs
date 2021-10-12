using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum BehaviorStatus { none, go, stay, back}
public class CustomerBehavior {
    public string target;
    public float stayTime;
    public bool returnRoom;
    public BehaviorStatus status;
    public CustomerBehavior(string t, float s, bool r = true)
    {
        target = t;
        stayTime = s;
        returnRoom = r;
    }
}


public class Customer : MonoBehaviour
{
    NavMeshAgent agent;
    float stayTime = 2;
    public Vector2 minmaxSpeed = new Vector2(0.5f, 1.5f);
    Animator anim;
    //Transform wayPoint;
    Vector3 targetPosition;
    DraggableRoom liveRoom;
    int maxBehavior = 5;
    float currentStayTime = 0;
    List<CustomerBehavior> behaviors = new List<CustomerBehavior>();
    Vector3 startPosition;

    public GameObject customerCamera;
    public string currentDoing;
    public List<string> logs;

    public string customerName;
    public string customerType;
    public CustomerInfo customerInfo;

    HashSet<DraggableRoom> visitedRoom = new HashSet<DraggableRoom>();

    public void selectCustomerInfo()
    {
        customerCamera.SetActive(true);
    }

    public void deselectCustomerInfo()
    {
        customerCamera.SetActive(false);
    }

    void debugBehavior(string t)
    {
        if (GameManager.Instance.debugCustomerBehavior)
        {
            Debug.Log(t);
        }
    }


    public void Init(DraggableRoom d, string n, string t)
    {
        customerName = n;
        customerType = t;
        startPosition = transform.position;
        liveRoom = d;
        customerInfo = CustomerManager.Instance.customerInfoDict[customerType];
        //wayPoint = d.transform;
        //agent.SetDestination(wayPoint.position);
        decideBehavior();
        doBehavior();
        agent.speed = RandomSpeed();
        logBehavior(customerName+" shows up");
    }

    bool isStaying()
    {
        return behaviors[0].status == BehaviorStatus.stay;
    }

    void changeCurrentDoing(string current)
    {

        if (current != null)
        {

            currentDoing = current;
        }
        EventPool.Trigger<string>("updateOneCustomer", customerName);
    }
    void logBehavior(string logText)
    {
        if (logText!=null)
        {

            logs.Add(logText);
        }
        EventPool.Trigger<string>("updateOneCustomer", customerName);
    }


    DraggableRoom pickTarget(string targetName)
    {

        int favoritePoint = 0;
        DraggableRoom res = null;
        if (!RoomManager.Instance.rooms.ContainsKey(behaviors[0].target))
        {
            return res;
        }
        for(int i = 0;i< RoomManager.Instance.rooms[behaviors[0].target].Count; i++)
        {
            var room = RoomManager.Instance.rooms[behaviors[0].target][i];
            if (visitedRoom.Contains(room))
            {
                continue;
            }
            int tempPoint = 0;

            foreach(var item in room.allLikeableItems())
            {
                foreach (var fav in customerInfo.favoriteItems)
                {
                    if(item == fav.key)
                    {
                        tempPoint += fav.amount;
                    }
                }
            }

            if (tempPoint > favoritePoint)
            {
                res = room;
                favoritePoint = tempPoint;
            }
        }

        debugBehavior(customerName + " " + customerType + " picked " + res + " with score " + favoritePoint);

        return res;
    }

    public void doBehavior()
    {
        bool skipNextLog = false;
        switch (behaviors[0].status)
        {
            case BehaviorStatus.none:
                debugBehavior("start a new behavior");
                if (behaviors[0].target == "bedroom")
                {
                    changeCurrentDoing("Go check in");
                    targetPosition = liveRoom.transform.position;
                    agent.SetDestination(targetPosition);
                    debugBehavior("will go to bedroom");
                }
                else if (behaviors[0].target == "home")
                {
                    changeCurrentDoing("Go back home");
                    targetPosition = startPosition;
                    agent.SetDestination(startPosition);
                    debugBehavior("will go to home");
                    liveRoom.customerRelease();
                }
                else
                {
                    var selectTarget = pickTarget(behaviors[0].target);
                    if (selectTarget)
                    {
                        changeCurrentDoing("Go to " + selectTarget.info.displayName);
                        Transform selectedRoom = selectTarget.transform;
                        targetPosition = selectedRoom.transform.position;
                        agent.SetDestination(targetPosition);

                        debugBehavior("will go to "+ behaviors[0].target);
                    }
                    else
                    {
                        logBehavior("Wants to " + behaviors[0].target + " but failed to find one");
                        debugBehavior("no room found as "+ behaviors[0].target);
                        skipNextLog = true;
                        goto case BehaviorStatus.stay;
                    }
                }
                behaviors[0].status = BehaviorStatus.go;
                break;
            case BehaviorStatus.go:
                changeCurrentDoing("stays in " + behaviors[0].target);
                logBehavior("arrives " + behaviors[0].target);
                currentStayTime = 0;
                if(behaviors[0].target == "home")
                {
                    goto case BehaviorStatus.back;
                }

                debugBehavior("start stay");
                behaviors[0].status = BehaviorStatus.stay;
                break;
            case BehaviorStatus.stay:
                if (!skipNextLog)
                {

                    logBehavior("enjoyed staying in " + behaviors[0].target);
                    skipNextLog = false;
                }
                if (behaviors[0].returnRoom)
                {

                    changeCurrentDoing("go back to bedroom");
                    targetPosition = liveRoom.transform.position;
                    agent.SetDestination(targetPosition);
                    behaviors[0].status = BehaviorStatus.back;
                    debugBehavior("stop stay, start moving back bedroom");
                    break;

                }
                else
                {
                    debugBehavior("stop stay");
                    goto case BehaviorStatus.back;
                }
            case BehaviorStatus.back:
                behaviors.RemoveAt(0);

                debugBehavior("finished a behavior");
                if (behaviors.Count == 0)
                {

                    debugBehavior("finished all behaviors");
                    changeCurrentDoing("Left");
                    //logBehavior(customerName + " get back home");
                    CustomerManager.Instance.removeCustomer(gameObject);
                }
                else
                {
                    doBehavior();
                }
                break;

        }
    }

    public void decideBehavior()
    {
        behaviors.Add(new CustomerBehavior("bedroom", stayTime));
        behaviors.Add(new CustomerBehavior("shop", stayTime));
        behaviors.Add(new CustomerBehavior("spring", stayTime, false));
        behaviors.Add(new CustomerBehavior("restaurant", stayTime));
        behaviors.Add(new CustomerBehavior("home", 0));

        bool willShop = Random.Range(0, 0.7f) <= 0.7f;
        bool willSpring = Random.Range(0, 0.7f) <= 0.7f;
        bool willEat = Random.Range(0, 0.7f) <= 0.7f;
        if (willShop)
        {

        }
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
        if (isStaying())
        {
            currentStayTime += Time.deltaTime;
            if (currentStayTime >= behaviors[0].stayTime)
            {
                doBehavior();
            }
        }
        else
        {

            float dist = Utils.distanceWithoutY(targetPosition, transform.position);
            if (dist < 0.35f)
            {
                //arrived
                doBehavior();
            }
        }
    }


    public float RandomSpeed()
    {
        return Random.Range(minmaxSpeed.x, minmaxSpeed.y);
    }
}
