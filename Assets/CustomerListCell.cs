using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerListCell : MonoBehaviour
{
    Customer customer;

    public TMP_Text nameLabel;
    public TMP_Text typeLabel;
    public TMP_Text statusLabel;

    public void init(Customer cust)
    {
        customer = cust;
        nameLabel.text = customer.customerName;
        typeLabel.text = customer.customerInfo.displayName;
        statusLabel.text = customer.currentDoing;
    }
    public void select()
    {
        CustomerManager.Instance.selectCustomerInfo(customer);
        Doozy.Engine.GameEventMessage.SendEvent("customerInfo");
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
