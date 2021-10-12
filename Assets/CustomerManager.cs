using LitJson;
using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerInfo : InfoBase
{
    public KeyValueBase[] favoriteItems;
}
public class AllCustomersInfo
{
    public List<CustomerInfo> generalCustomer;
}
public class CustomerManager : Singleton<CustomerManager>
{
    

    public Dictionary<string, CustomerInfo> customerInfoDict = new Dictionary<string, CustomerInfo>();
    string[] nameList = new string[]
    {"Liam","Olivia","Noah","Emma","Oliver","Ava","Elijah","Charlotte","William",
        "Sophia","James","Amelia","Benjamin","Isabella","Lucas","Mia","Henry","Evelyn",
        "Alexander","Harper"
    };
    private void Awake()
    {
        string text = Resources.Load<TextAsset>("json/Customer").text;
        var allNPCs = JsonMapper.ToObject<AllCustomersInfo>(text);
        foreach (CustomerInfo info in allNPCs.generalCustomer)
        {
            customerInfoDict[info.name] = info;
        }
    }
    public Customer currentSelectedCustomer;

    public List<GameObject> customerList = new List<GameObject>();

    string generateCustoemrType()
    {
        var rand =Random.Range(0, customerInfoDict.Count);
        return customerInfoDict.Values.ToList()[rand].name;
    }
    public void selectCustomerInfo(Customer go)
    {
        currentSelectedCustomer = go;
        go.selectCustomerInfo();
    }

    public void deselectCustomerInfo()
    {
        if (currentSelectedCustomer)
        {
            currentSelectedCustomer.deselectCustomerInfo();
            currentSelectedCustomer = null;
        }
    }

    string getCustomerName()
    {
        return nameList[Random.Range(0, nameList.Length)];
    }
    public GameObject createCustomer(Vector3 position, DraggableRoom selectedRoom)
    {
        string customerType = generateCustoemrType();
        GameObject prefab = Resources.Load<GameObject>("NPC/"+ customerType);
        GameObject go = Instantiate<GameObject>(prefab, position, Quaternion.identity, transform);
        Customer customer = go.GetComponent<Customer>();
        string customerName = getCustomerName();
        customer.Init(selectedRoom, customerName, customerType);
        customerList.Add(go);
        EventPool.Trigger("updateCustomer");
        return go;
    }
    public void removeCustomer(GameObject go)
    {
        customerList.Remove(go);
        Destroy(go);
        EventPool.Trigger("updateCustomer");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
