using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : Dragable
{
    public bool isMainItem;
    public DraggableRoom room;
    public void Init(string t, InfoBase i, bool ism)
    {
        base.Init(t,i);
        isMainItem = ism;
    }

    protected override void Update()
    {
        base.Update();

    }
    protected override bool canBuildItem()
    {
        return BuildModeManager.Instance.currentRoom.canPlaceItem(this);
    }
    protected override void build()
    {
        isBuilt = true;
        if (isMainItem)
        {

            BuildModeManager.Instance.currentRoom.setMainItem(this);
        }
        else
        {
            BuildModeManager.Instance.currentRoom.addItem(this);
        }

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
