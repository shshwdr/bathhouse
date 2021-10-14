using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : Dragable
{
    public bool isMainItem;
    public bool occupied;
    public AffectRange effectRangeItem;

    public List<DraggableItem> affectedItems = new List<DraggableItem>();

    public string catelog { get { return ((RoomItemInfo)info).catelog; } }

    public void customerOccupy()
    {
        occupied = true;
    }

    public void customerRelease()
    {
        occupied = false;
    }

    public void Init(string t, InfoBase i, bool ism)
    {
        base.Init(t,i);
        isMainItem = ism;
        if (!isMainItem)
        {
            //update radius
            float radius = ((RoomDecorationInfo)info).affectRadius;
            effectRangeItem.transform.localScale = new Vector3(radius, 1, radius);
        }
        else
        {
            //use max radius
            float radius = 0;// RoomItemManager.Instance.maxAffectRange;
            effectRangeItem.GetComponent<Renderer>().enabled = false;
            effectRangeItem.transform.localScale = new Vector3(radius, 1, radius);
        }
    }

    protected override void Update()
    {
        base.Update();

    }
    protected override bool canBuildItem()
    {
        return RegionManager.Instance.currentRegion.canPlaceRoom(this);
    }
    protected override void build()
    {
        isBuilt = true;
        RoomItemManager.Instance.addItem(this);
        effectRangeItem.hideEffectOnAffectedItems();

        consumeRequirements();
    }

    public override void cancelDragItem()
    {
        base.cancelDragItem();
        if (isBuilt)
        {
            //pop up
            removeDragItem();
        }
    }
}
