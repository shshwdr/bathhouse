using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerListController : MonoBehaviour
{
    public Transform listParent;
    bool isActive;
    private void Awake()
    {
        if (!listParent)
        {
            listParent = transform;
        }

        EventPool.OptIn("updateCustomer", updateList);

        EventPool.OptIn<string>("updateOneCustomer", updateList);
    }

    public void showView()
    {
        isActive = true;
        CustomerManager.Instance.deselectCustomerInfo();
        updateList();
    }

    public void hideView()
    {
        isActive = false;
    }
    void updateList(string t)
    {
        updateList();
    }
    void updateList()
    {
        if (!isActive)
        {
            return;
        }
        var allCustomers = CustomerManager.Instance.customerList;
        if (allCustomers.Count > listParent.childCount)
        {
            Debug.LogError("listParent does not have enough child.. probably need to extend automatically?");
        }
        int i = 0;
        for (;i< allCustomers.Count; i++)
        {
            listParent.GetChild(i).gameObject.SetActive(true);
            listParent.GetChild(i).GetComponent<CustomerListCell>().init(allCustomers[i].GetComponent<Customer>());
        }
        for (; i < listParent.childCount; i++)
        {

            listParent.GetChild(i).gameObject.SetActive(false);
        }
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
