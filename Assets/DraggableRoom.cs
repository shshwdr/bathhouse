using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { bedroom, spring}
public class DraggableRoom : Dragable
{
    public RoomType roomType;
    public bool occupied;
    public Cinemachine.CinemachineVirtualCamera buildCamera;
    public List<DraggableItem> items = new List<DraggableItem>();
    protected override void build()
    {
        //RegionManager.Instance.currentRegion.addRoom(this);
        BuildModeManager.Instance.buildRoom(this);
        Doozy.Engine.GameEventMessage.SendEvent("addItem");
        buildCamera.gameObject.SetActive(true);
    }

    public void addItem(DraggableItem item)
    {
        items.Add(item);
    }

    public void removeItem(DraggableItem item)
    {
        items.Remove(item);
    }

    public bool canPlaceItem(DraggableItem item)
    {

        var itemCollider = item.placeCollider;
        if (!Utils.colliderContainFromTop(placeCollider,item.placeCollider))
        {
            return false;
        }
        foreach (var r in items)
        {
            if (r.placeCollider.bounds.Intersects(itemCollider.bounds))
            {
                return false;
            }
        }
        return true;
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

        //RegionManager.Instance.currentRegion.removeRoom(this);
        //RoomManager.Instance.removeRoom(this);

        foreach(var item in items)
        {
            Destroy(item.gameObject);
        }
    }
}
