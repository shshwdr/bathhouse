//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DraggableRoom : Draggable
//{
//    public bool occupied;
//    public Cinemachine.CinemachineVirtualCamera buildCamera;
//    public List<DraggableItem> items = new List<DraggableItem>();
//    public DraggableItem mainItem;
//    int maxItem = 2;
//    public bool isPreBuild = false;
//    public bool isEditing = true;

//    public List<string> allLikeableItems()
//    {
//        List<string> res=new List<string>();

//        res.Add(info.name);

//        res.Add(mainItem.info.name);

//        foreach(var item in items)
//        {

//            res.Add(item.info.name);
//        }

//        return res;
//    }
//    protected override void build()
//    {
//        //RegionManager.Instance.currentRegion.addRoom(this);
//        getIntoEditMode();
//        consumeRequirements();
//    }

//    public void getIntoEditMode()
//    {

//        BuildModeManager.Instance.buildRoom(this);
//        Doozy.Engine.GameEventMessage.SendEvent("addItem");
//        buildCamera.gameObject.SetActive(true);
//    }


//    public void setMainItem(DraggableItem item)
//    {
//        if (mainItem)
//        {
//            Destroy(mainItem.gameObject);
//        }
//        mainItem = item;
//        item.room = this;
//    }

//    public void addItem(DraggableItem item)
//    {
//        items.Add(item);
//        item.room = this;
//    }

//    public bool canAddNewItem()
//    {
//        return items.Count < maxItem;
//    }

//    public void removeItem(DraggableItem item)
//    {
//        items.Remove(item);
//    }

//    public bool canPlaceItem(DraggableItem item)
//    {

//        var itemCollider = item.placeCollider;
//        if (!Utils.colliderContainFromTop(placeCollider,item.placeCollider))
//        {
//            return false;
//        }
//        foreach (var r in items)
//        {
//            if (r.placeCollider.bounds.Intersects(itemCollider.bounds))
//            {
//                return false;
//            }
//        }
//        if (!item.isMainItem)
//        {
//            if (mainItem.placeCollider.bounds.Intersects(itemCollider.bounds))
//            {
//                return false;
//            }
//        }
//        return true;
//    }

//    public void customerOccupy()
//    {
//        occupied = true;
//    }

//    public void customerRelease()
//    {
//        occupied = false;
//    }

//    protected override bool canBuildItem()
//    {
//        return RegionManager.Instance.currentRegion.canPlaceRoom(this);
//    }

//    private void OnDestroy()
//    {

//        //RegionManager.Instance.currentRegion.removeRoom(this);
//        //RoomManager.Instance.removeRoom(this);

//        foreach(var item in items)
//        {
//            item.removeDragItem();
//        }
//        if (mainItem)
//        {
//            mainItem.removeDragItem();
//        }
//    }
//    protected override void Start()
//    {
//        base.Start();
//        if (isPreBuild)
//        {
//            isDragging = false;
//            finishBuild();
//        }
//    }
//    public void finishBuild()
//    {
//        isEditing = false;
//        RegionManager.Instance.currentRegion.addRoom(this);
//        RoomManager.Instance.addRoom(this);
//    }
//}
