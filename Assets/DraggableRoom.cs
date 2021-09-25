using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableRoom : Dragable
{
    public Cinemachine.CinemachineVirtualCamera buildCamera;
    protected override void build()
    {
        RegionManager.Instance.currentRegion.addRoom(this);
        Doozy.Engine.GameEventMessage.SendEvent("addItem");
        buildCamera.gameObject.SetActive(true);
    }

    protected override bool canBuildItem()
    {
        return RegionManager.Instance.currentRegion.canPlaceRoom(this);
    }


}
