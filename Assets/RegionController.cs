using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour
{
    public Collider regionCollider;
    public int index;
    public List<DraggableRoom> rooms = new List<DraggableRoom>();

    public Cinemachine.CinemachineVirtualCamera buildCamera;

    public void addRoom(DraggableRoom room)
    {
        rooms.Add(room);
    }

    public void removeRoom(DraggableRoom room)
    {
        rooms.Remove(room);
    }
    public bool canPlaceRoom(DraggableRoom room)
    {
        var roomCollider = room.placeCollider;
        if (!regionCollider.bounds.Contains(roomCollider.bounds.min) ||!regionCollider.bounds.Contains(roomCollider.bounds.max))
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
