using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoomButton : BuildItemButton
{
    protected override string itemType()
    {
        return "room";
    }
    protected override void SpawnItem()
    {
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //PlantsManager.Instance.shadowCollider.gameObject.SetActive(true);
        GameObject spawnInstance = Instantiate(prefab);
        MouseManager.Instance.startDragItem(spawnInstance);
    }
    protected override bool CanPurchaseItem()
    {
        return true;
    }
    
}
