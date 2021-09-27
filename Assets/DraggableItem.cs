using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : Dragable
{
    protected override bool canBuildItem()
    {
        return BuildModeManager.Instance.currentRoom.canPlaceItem(this);
    }
    protected override void build()
    {
        BuildModeManager.Instance.currentRoom.addItem(this);
    }
}
