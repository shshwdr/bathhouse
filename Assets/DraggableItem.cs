using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : Dragable
{
    public bool isMainItem;
    public void Init(string t, bool ism)
    {
        base.Init(t);
        isMainItem = ism;
    }
    protected override bool canBuildItem()
    {
        return BuildModeManager.Instance.currentRoom.canPlaceItem(this);
    }
    protected override void build()
    {
        BuildModeManager.Instance.currentRoom.setMainItem(this);
    }
}
