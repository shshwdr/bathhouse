using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour
{
    public Collider regionCollider;
    public string regionType;
    public int index;
    public List<DraggableItem> rooms = new List<DraggableItem>();

    public Cinemachine.CinemachineVirtualCamera buildCamera;

    public void addRoom(DraggableItem room)
    {
        rooms.Add(room);
    }

    public void removeRoom(DraggableItem room)
    {
        rooms.Remove(room);
    }
    public bool canPlaceRoom(DraggableItem item)
    {
        var roomCollider = item.placeCollider;
        if (!Utils.colliderContainFromTop(regionCollider, roomCollider))
        {
            return false;
        }
        foreach(var r in rooms)
        {
            if (r.placeCollider.bounds.Intersects(roomCollider.bounds))
            {
                return false;
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        RegionManager.Instance.addRegion(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
