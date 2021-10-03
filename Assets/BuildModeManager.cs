using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildModeManager : Singleton<BuildModeManager>
{
    public DraggableRoom currentRoom;

    public void buildRoom(DraggableRoom room)
    {
        currentRoom = room;
    }

    public void finishCurrentRoom()
    {
        //if (!currentRoom.isValid())
        //{

        //    return;
        //}

        RegionManager.Instance.currentRegion.addRoom(currentRoom);
        RoomManager.Instance.addRoom(currentRoom);
        currentRoom = null;
    }

    public void cancelCurrentRoom()
    {
        Destroy(currentRoom.gameObject);
        currentRoom = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
