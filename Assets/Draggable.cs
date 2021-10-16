using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Draggable : MonoBehaviour
{
    public bool isDragging = true;
    Vector3 screenPoint;
    Camera dragCamera;
    Material material;
    public Renderer renderer;
    public Renderer overlay;
    public Collider placeCollider;
    public string type;
    public InfoBase info;
    public bool isBuilt = false;


    public void changeOverlayColor(Color color)
    {
        overlay.material.color = color;
    }

    public void showEnableOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(true);
            changeOverlayColor(Color.green);
        }
        else
        {

            renderer.material.color = Color.green;
        }
    }

    public void showDisableOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(true);
            changeOverlayColor(Color.red);
        }
        else
        {

            renderer.material.color = Color.red;
        }
    }

    public void showActiveOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(true);
            changeOverlayColor(Color.yellow);
        }
        else
        {
            renderer.material.color = Color.yellow;
        }
    }

    public void hideOverlay()
    {
        if (overlay)
        {
            overlay.gameObject.SetActive(false);
        }
        else
        {

            renderer.material.color = Color.white;
        }

    }


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
        material = renderer.material;
        screenPoint = dragCamera.WorldToScreenPoint(gameObject.transform.position);
    }

    protected virtual void Update()
    {
        if (isDragging)
        {
            bool canbuild = canBuildItem();
            if (!canbuild)
            {
                showDisableOverlay();
            }
            else
            {
                showEnableOverlay();
            }

            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 mousePosition = dragCamera.ScreenToWorldPoint(newPosition);
            transform.position = mousePosition;
            

        }
    }

    public void tryBuild()
    {
        bool canbuild = canBuildItem();
        if (canbuild)
        {
            isDragging = false;
            material.color = Color.white;
            MouseManager.Instance.finishDragItem(gameObject);
            build();
        }
    }

    public void removeDragItem()
    {
        addBackRequirements();
        Destroy(gameObject);
    }
}
