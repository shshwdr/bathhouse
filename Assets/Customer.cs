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
    DraggableItem nextItem;
    DraggableItem liveBed;
    int maxBehavior = 5;
    float currentStayTime = 0;
    List<CustomerBehavior> behaviors = new List<CustomerBehavior>();
    Vector3 startPosition;
    public float arriveDistance = 2f;
    public Transform popupParent;

    public GameObject customerCamera;
    public string currentDoing;
    public List<string> logs;

    public string customerName;
    public string customerType;
    public CustomerInfo customerInfo;

    HashSet<DraggableItem> visitedItem = new HashSet<DraggableItem>();

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


    public void Init(DraggableItem d, string n, string t)
    {
        customerName = n;
        customerType = t;
        startPosition = transform.position;
        liveBed = d;
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

    public void showLikeness(DraggableItem item)
    {
        if (!item)
        {
            return;
        }
        int tempPoint = 0;
        var items = new List<DraggableItem>(item.affectedItems);
        items.Add(item);
        foreach (var ite in items)
        {
            foreach (var fav in customerInfo.favoriteItems)
            {
                if (ite.info.name == fav.key)
                {
                    tempPoint += fav.amount;
                    break;
                }
            }
        }
        logBehavior("happy +" + tempPoint);
        //Popup.createPopup("happy +" + tempPoint, popupParent);
    }
    public void showTips(DraggableItem item)
    {
        if (!item)
        {
            return;
        }
        int tempPoint = 0;
        var items = new List<DraggableItem>(item.affectedItems);
        items.Add(item);
        foreach (var ite in items)
        {
                    tempPoint += ((RoomItemInfo) ite.info).earning;
        }
        logBehavior("tips +" + tempPoint);
        //Popup.createPopup("tips +" + tempPoint, popupParent);
    }

    DraggableItem pickTarget(string targetName)
    {

        int favoritePoint = 0;
        DraggableItem res = null;
        if (!RoomItemManager.Instance.items.ContainsKey(behaviors[0].target))
        {
            return res;
        }
        for(int i = 0;i< RoomItemManager.Instance.items[behaviors[0].target].Count; i++)
        {
            var item = RoomItemManager.Instance.items[behaviors[0].target][i];
            if (visitedItem.Contains(item))
            {
                continue;
            }
            int tempPoint = 0;

            foreach (var ite in item.affectedItems)
            {
                foreach (var fav in customerInfo.favoriteItems)
                {
                    if (item.info.name == fav.key)
                    {
                        tempPoint += fav.amount;
                    }
                }
            }

            if (tempPoint >= favoritePoint)
            {
                res = item;
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
                if (behaviors[0].target == "bed")
                {
                    nextItem = liveBed;
                    changeCurrentDoing("Go check in");
                    targetPosition = liveBed.transform.position;
                    agent.SetDestination(targetPosition);
                    debugBehavior("will go to bedroom");
                }
                else if (behaviors[0].target == "home")
                {
                    nextItem = null;
                    changeCurrentDoing("Go back home");
                    targetPosition = startPosition;
                    agent.SetDestination(startPosition);
                    debugBehavior("will go to home");
                    liveBed.customerRelease();
                }
                else
                {
                    nextItem = pickTarget(behaviors[0].target);
                    if (nextItem)
                    {
                        changeCurrentDoing("Go to " + nextItem.info.displayName);
                        Transform selectedRoom = nextItem.transform;
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
                showLikeness(nextItem);
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

                    showTips(nextItem);
                    logBehavior("enjoyed staying in " + behaviors[0].target);
                    skipNextLog = false;
                }
                if (behaviors[0].returnRoom)
                {

                    changeCurrentDoing("go back to bedroom");
                    targetPosition = liveBed.transform.position;
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
        behaviors.Add(new CustomerBehavior("bed", stayTime));
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
            if (dist < arriveDistance)
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
