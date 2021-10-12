using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dragable : MonoBehaviour
{
    protected bool isDragging = true;
    Vector3 screenPoint;
    Camera dragCamera;
    Material material;
    public Collider placeCollider;
    public string type;
    public InfoBase info;
    public bool isBuilt = false;

    public void consumeRequirements()
    {
        InfoWithRequirementBase infoWithRequirement = (InfoWithRequirementBase)info;
        var requirements = infoWithRequirement.requireResources;
        foreach(var req in requirements)
        {
            Inventory.Instance.consumeItem(req.key, req.amount);
        }
    }
    public void addBackRequirements()
    {
        InfoWithRequirementBase infoWithRequirement = (InfoWithRequirementBase)info;
        var requirements = infoWithRequirement.requireResources;
        foreach (var req in requirements)
        {
            Inventory.Instance.addItem(req.key, req.amount);
        }
    }
    

    public void Init(string t,InfoBase i)
    {
        type = t;
        info = i;
    }
    protected abstract bool canBuildItem();
    protected abstract void build();

    protected virtual void Start()
    {
        dragCamera = Camera.main;
        material = GetComponentInChildren<Renderer>().material;
        screenPoint = dragCamera.WorldToScreenPoint(gameObject.transform.position);
    }

    protected virtual void Update()
    {
        if (isDragging)
        {
            bool canbuild = canBuildItem();
            if (!canbuild)
            {
                material.color = Color.red;
            }
            else
            {
                material.color = Color.green;
            }

            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 mousePosition = dragCamera.ScreenToWorldPoint(newPosition);
            transform.position = mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (canbuild)
                {
                    isDragging = false;
                    material.color = Color.white;
                    MouseManager.Instance.finishDragItem(gameObject);
                    build();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                cancelDragItem();
            }

        }
    }

    public virtual void cancelDragItem()
    {
        MouseManager.Instance.cancelDragItem(gameObject);
    }
    public void removeDragItem()
    {
        addBackRequirements();
        Destroy(gameObject);
    }
}
