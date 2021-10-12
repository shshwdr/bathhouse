using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class CustomerInfoController : MonoBehaviour
{

    public TMP_Text nameLabel;
    public TMP_Text typeLabel;
    public TMP_Text statusLabel;
    public Transform favoriteListParent;
    public Transform logParent;
    Customer selectedCustomer;
    public void showView()
    {
        selectedCustomer = CustomerManager.Instance.currentSelectedCustomer;
        init();
    }

    public void hideView()
    {
        selectedCustomer = null;
    }
    public void init()
    {
        nameLabel.text = selectedCustomer.customerName;
        typeLabel.text = selectedCustomer.customerInfo.displayName;
        statusLabel.text = selectedCustomer.currentDoing;

        int i = 0;
        Assert.IsTrue(selectedCustomer.customerInfo.favoriteItems.Length <= favoriteListParent.childCount);
        for (; i < selectedCustomer.customerInfo.favoriteItems.Length; i++)
        {
            favoriteListParent.GetChild(i).gameObject.SetActive(true);
            var favorateItem = selectedCustomer.customerInfo.favoriteItems[i];
            //todo get display name
            favoriteListParent.GetChild(i).GetComponent<KeyValuePairCell>().init(favorateItem.key, favorateItem.amount);
        }
        for (; i < favoriteListParent.childCount; i++)
        {

            favoriteListParent.GetChild(i).gameObject.SetActive(false);
        }



        i = 0;
        Assert.IsTrue(selectedCustomer.logs.Count <= logParent.childCount);
        //only need to update the latest log
        for (; i < selectedCustomer.logs.Count; i++)
        {
            logParent.GetChild(i).gameObject.SetActive(true);
            //todo get display name
            logParent.GetChild(i).GetComponentInChildren<TMP_Text>().text = selectedCustomer.logs[i];
        }
        for (; i < logParent.childCount; i++)
        {

            logParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    void updateCustomerInfo(string customerName)
    {
        if (selectedCustomer&&selectedCustomer.customerName == customerName)
        {
            init();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn<string>("updateOneCustomer", updateCustomerInfo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
