using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemAddController : SelectionController
{
    protected override int allItemInfoCount()
    {
        return RoomItemManager.Instance.mainItemInfoDict.Values.Count;
    }
    protected override InfoBase itemInfo(int i)
    {
        return RoomItemManager.Instance.mainItemInfoDict.Values.ToList()[i];
    }
}
