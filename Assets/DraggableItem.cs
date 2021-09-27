using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableItem : Dragable
{
    protected override bool canBuildItem()
    {
        return true;
    }
    protected override void build()
    {
    }
}
