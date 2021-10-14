using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemAddController : SelectionController
{
    public bool isMainItem;
    protected override List<InfoBase> allItems()
    {
        var res = new List<InfoBase>();
        Dictionary<string,RoomItemInfo>.ValueCollection allInfos;
        if (isMainItem)
        {
            allInfos = RoomItemManager.Instance.mainItemInfoDict.Values;
        }
        else
        {

            allInfos = RoomItemManager.Instance.decoItemInfoDict.Values;
        }
        foreach (var info in allInfos)
        {
            //if (info.rooms.Contains(BuildModeManager.Instance.currentRoom.type))
            {
                res.Add(info);
            }
        }
        return res;
    }
}
