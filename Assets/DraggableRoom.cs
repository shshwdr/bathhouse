using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { bedroom, spring}
public class DraggableRoom : Dragable
{
    public RoomType roomType;
    public bool occupied;
    public Cinemachine.CinemachineVirtualCamera buildCamera;
    protected override void build()
    {
        RegionManager.Instance.currentRegion.addRoom(this);
        RoomManager.Instance.addRoom(this);
        Doozy.Engine.GameEventMessage.SendEvent("addItem");
        buildCamera.gameObject.SetActive(true);
    }

    public void customerOccupy()
    {
        occupied = true;
    }

    public void customerRelease()
    {
        occupied = false;
    }

    protected override bool canBuildItem()
    {
        return RegionManager.Instance.currentRegion.canPlaceRoom(this);
    }

    private void OnDestroy()
    {

        RegionManager.Instance.currentRegion.removeRoom(this);
        RoomManager.Instance.removeRoom(this);
    }
}
