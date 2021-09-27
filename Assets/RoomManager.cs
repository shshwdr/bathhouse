using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBase
{
    public string name;
    public string displayName;
    public string description;
}

public class RoomInfo: InfoBase
{
    public string region;

}
public class AllRoomInfo
{
    public List<RoomInfo> allRoom;
}

public class RoomManager : Singleton<RoomManager>
{
    //public List<DraggableRoom> rooms = new List<DraggableRoom>();
    public Dictionary<string, RoomInfo> roomInfoDict = new Dictionary<string, RoomInfo>();
    public Dictionary<RoomType, List<DraggableRoom>> rooms = new Dictionary<RoomType, List<DraggableRoom>>();
    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/room").text;
        var allNPCs = JsonMapper.ToObject<AllRoomInfo>(text);
        foreach (RoomInfo info in allNPCs.allRoom)
        {
            roomInfoDict[info.name] = info;
        }

        foreach (RoomType type in System.Enum.GetValues(typeof(RoomType)))
        {
            rooms[type] = new List<DraggableRoom>();
        }
    }



    public void addRoom(DraggableRoom room)
    {
        rooms[room.roomType].Add(room);
    }

    public void removeRoom(DraggableRoom room)
    {
        rooms[room.roomType].Remove(room);
    }

    public List<DraggableRoom> availableBedroom()
    {
        List<DraggableRoom> res = new List<DraggableRoom>();
        foreach(DraggableRoom room in rooms[RoomType.bedroom])
        {
            if (!room.occupied)
            {
                res.Add(room);
            }
        }
        return res;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
