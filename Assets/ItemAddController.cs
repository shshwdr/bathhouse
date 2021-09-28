using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemAddController : SelectionController
{
    protected override List<InfoBase> allItems()
    {
        var res = new List<InfoBase>();
        foreach (var info in RoomItemManager.Instance.mainItemInfoDict.Values)
        {
            if (info.rooms.Contains(BuildModeManager.Instance.currentRoom.type))
            {
                res.Add(info);
            }
        }
        return res;
    }
}
