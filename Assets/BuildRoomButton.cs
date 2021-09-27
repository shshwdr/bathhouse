using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomButton : BuildItemButton
{
    public override string itemType()
    {
        return "room";
    }
    public override void SpawnItem()
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //PlantsManager.Instance.shadowCollider.gameObject.SetActive(true);
        GameObject spawnInstance = Instantiate(prefab);
        MouseManager.Instance.startDragItem(spawnInstance);
    }
    public override bool CanPurchaseItem()
    {
        return true;
    }
    
}
