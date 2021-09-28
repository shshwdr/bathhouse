using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomSelectionController : SelectionController
{
    protected override void Start()
    {
        base.Start();
        EventPool.OptIn("changeRegion", updateUI);
    }
    protected override List<InfoBase> allItems()
    {
        var res = new List<InfoBase>();
        foreach(var info in RoomManager.Instance.roomInfoDict.Values)
        {
            if (info.regions.Contains(RegionManager.Instance.currentRegion.regionType))
            {
                res.Add(info);
            }
        }
        return res;
    }
}
