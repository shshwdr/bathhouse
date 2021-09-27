using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSelectionController : SelectionController
{
    protected override int allItemInfoCount()
    {
        return RoomManager.Instance.roomInfoDict.Values.Count;
    }
    protected override InfoBase itemInfo(int i)
    {
        return RoomManager.Instance.roomInfoDict.Values.ToList()[i];
    }
}
