using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectRange : MonoBehaviour
{
    DraggableItem parentItem;
    // Start is called before the first frame update
    void Start()
    {
        parentItem = GetComponentInParent<DraggableItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().enabled = true;
        var otherItem = other.GetComponent<DraggableItem>();
        if (otherItem && otherItem.isMainItem != parentItem.isMainItem)
        {
            parentItem.affectedItems.Add(otherItem);
            otherItem.affectedItems.Add(parentItem);
            otherItem.GetComponentInChildren<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var otherItem = other.GetComponent<DraggableItem>();
        if (otherItem && otherItem.isMainItem != parentItem.isMainItem)
        {
            if (!parentItem.affectedItems.Contains(otherItem))
            {
                Debug.LogError("item does not exist in affected items " + otherItem);
                return;
            }
            if (!otherItem.affectedItems.Contains(parentItem))
            {
                Debug.LogError("reverse item does not exist in affected items " + otherItem);
                return;
            }
            otherItem.affectedItems.Remove(parentItem);
            parentItem.affectedItems.Remove(otherItem);
            otherItem.GetComponentInChildren<Renderer>().material.color = Color.white;
        }
    }

    public void hideEffectOnAffectedItems()
    {
        GetComponent<Renderer>().enabled = false;
        foreach(var item in parentItem.affectedItems)
        {
            item.GetComponentInChildren<Renderer>().material.color = Color.white;

        }
    }

    public void showEffectOnAffectedItems()
    {
        foreach (var item in parentItem.affectedItems)
        {
            item.GetComponentInChildren<Renderer>().material.color = Color.yellow;

        }
    }
}
