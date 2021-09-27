using UnityEngine;
using UnityEngine.UI;
using Pool;

public class MouseManager : Singleton<MouseManager>
{
    public GameObject currentDragItem;

    public void startDragItem(GameObject go)
    {
        if (currentDragItem)
        {
            Destroy(currentDragItem);
        }
        currentDragItem = go;
    }
    public void cancelCurrentDragItem()
    {

        Destroy(currentDragItem);
        currentDragItem = null;
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
        Doozy.Engine.GameEventMessage.SendEvent("addItem");
    }
    private void Update()
    {

    }

}