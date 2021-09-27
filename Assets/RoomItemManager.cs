using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoomItemInfo : InfoBase
{
    public string[] rooms;

}
public class AllRoomItemInfo
{
    public List<RoomItemInfo> allMainItem;
    public List<RoomItemInfo> allDecorations;
}
public class RoomItemManager : Singleton<RoomItemManager>
{
    public Dictionary<string, RoomItemInfo> mainItemInfoDict = new Dictionary<string, RoomItemInfo>();
    // Start is called before the first frame update
    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/item").text;
        var allNPCs = JsonMapper.ToObject<AllRoomItemInfo>(text);
        foreach (RoomItemInfo info in allNPCs.allMainItem)
        {
            mainItemInfoDict[info.name] = info;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
