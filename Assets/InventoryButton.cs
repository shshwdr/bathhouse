using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryButton : BuildItemButton
{
    public TMP_Text amountText;
    public override bool CanPurchaseItem()
    {
        return false;
    }
    public override void init(InfoBase info)
    {
        base.init(info);
        amountText.text = ((ItemInfo)info).amount.ToString();
    }

    public override string itemType()
    {
        return "inventory";
    }

    public override void SpawnItem()
    {
        //
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
