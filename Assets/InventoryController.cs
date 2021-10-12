using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : SelectionController
{
    protected override List<InfoBase> allItems()
    {
        var res = new List<InfoBase>();
        foreach (var info in Inventory.Instance.itemDict.Values)
        {
            if (((ItemInfo)info).amount>0)
            {
                res.Add(info);
            }
        }
        return res;
    }
    protected override void Start()
    {
        base.Start();
        EventPool.OptIn("inventoryChanged", updateUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
