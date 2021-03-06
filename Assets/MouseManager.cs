using UnityEngine;
using UnityEngine.UI;
using Pool;

public class MouseManager : Singleton<MouseManager>
{
    public GameObject currentDragItem;
    public bool isInBuildMode;
    public GameObject selectedItem;
    public float rotateSmooth = 10.0f;
    void selectItem(GameObject go)
    {
        selectedItem = go;
    }
    public void deselectIem()
    {
        selectedItem = null;
    }
    public void startDragItem(GameObject go)
    {
        if (currentDragItem)
        {
            Debug.Log("already have dragItem");
            Destroy(currentDragItem);
        }
        currentDragItem = go;
    }
    public void cancelCurrentDragItem()
    {

        Destroy(currentDragItem);
        currentDragItem = null;
        isInBuildMode = false;
    }

    public void finishCurrentDragItem()
    {
        

        isInBuildMode = false;
        currentDragItem = null;
    }

    public void startBuildMode()
    {
        isInBuildMode = true;
    }
    public void cancelDragItem(GameObject go)
    {
        if(currentDragItem != go)
        {
            Debug.LogError("cancel " + go + " is not the current one " + currentDragItem);
        }

        Destroy(currentDragItem);
        currentDragItem = null;
    }

    public void finishDragItem(GameObject go)
    {

        if (currentDragItem != go)
        {
            Debug.LogError("cancel " + go + " is not the current one " + currentDragItem);
        }

        currentDragItem = null;
    }
    private void Start()
    {
        //Doozy.Engine.GameEventMessage.SendEvent("addItem");
    }



    private void Update()
    {
        if (currentDragItem)
        {
            float tilt = Input.GetAxisRaw("Rotate");
            currentDragItem.transform.RotateAround(Vector3.zero, Vector3.up* tilt, rotateSmooth * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                print("on ui");
                return;
            }
            Debug.Log("mouse down");

            if (currentDragItem)
            {
                cancelCurrentDragItem();
                return;
            }


            if (currentDragItem == null && isInBuildMode )
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                foreach (var hit in Physics.RaycastAll(ray))
                {
                    Debug.Log("hit " + hit.transform.gameObject);
                    var hitItem = hit.transform.GetComponent<DraggableItem>();
                    if (hitItem)
                    {
                        Doozy.Engine.GameEventMessage.SendEvent("ItemAction");
                        selectItem(hit.transform.gameObject);
                        return;
                    }
                }
            }
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                print("on ui");
                return;
            }

            if(selectedItem && currentDragItem)
            {
                Debug.LogError("could only hold one item");
                return;
            }
            if (currentDragItem)
            {
                currentDragItem.GetComponent<Draggable>().tryBuild();
            }

            if (selectedItem)
            {
                deselectIem();

                Doozy.Engine.GameEventMessage.SendEvent("CancelItemAction");
            }
            //if (currentDragItem == null)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //    foreach (var hit in Physics.RaycastAll(ray))
            //    {
            //        Debug.Log("hit " + hit.transform.gameObject);
            //        var hitItem = hit.transform.GetComponent<DraggableItem>();
            //        if (hitItem)
            //        {
            //            if (hitItem.room.isEditing)
            //            {
            //                startDragItem(hitItem.gameObject);
            //                return;
            //            }
            //        }
            //    }
            //}

            return;
        }
    }

    //public void removeSelectedRoom()
    //{
    //    if(!selectedItem || !selectedItem.GetComponent<DraggableRoom>())
    //    {
    //        Debug.LogError(selectedItem + " is not a room that can be removed");
    //    }
    //    selectedItem.GetComponent<DraggableRoom>().cancelDragItem();
    //}

    //public void startDraggingRoom()
    //{
    //    if (!selectedItem || !selectedItem.GetComponent<DraggableRoom>())
    //    {
    //        Debug.LogError(selectedItem + " is not a room that can be moved");
    //    }
    //    selectedItem.GetComponent<DraggableRoom>().getIntoEditMode();
    //}

    //public void editSelectedRoomItems()
    //{
    //    if (!selectedItem || !selectedItem.GetComponent<DraggableRoom>())
    //    {
    //        Debug.LogError(selectedItem + " is not a room that can be moved");
    //    }
    //    selectedItem.GetComponent<DraggableRoom>().getIntoEditMode();
    //}

    public void removeCurrentDragging()
    {
        //pop up 
        currentDragItem.GetComponent<Draggable>().removeDragItem();
    }

}