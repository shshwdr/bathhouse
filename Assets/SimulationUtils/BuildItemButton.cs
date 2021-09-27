﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BuildItemButton : MonoBehaviour
{

    string itemName;
    InfoBase itemInfo;
    protected GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    public abstract string itemType();
    public void init(InfoBase info)
    {
        itemName = info.name;
        itemInfo = info;
        GetComponent<SelectCell>().init(info);
        prefab = Resources.Load<GameObject>("item/"+itemType() + "/" + itemName);

    }

    

    public IEnumerator delaySpawn()
    {
        yield return new WaitForSeconds(0.1f);

        SpawnItem();
    }

    public abstract void SpawnItem();
    public abstract bool CanPurchaseItem();



    // Update is called once per frame
    void Update()
    {
        //var currentPlantableState = PlantsManager.Instance.IsPlantable(helperPlant.type);
        //if (currentPlantableState)
        //{
        //    GetComponent<Button>().interactable = true;
        //    if (!previousPlantableState)
        //    {

        //        TutorialManager.Instance.canPurchasePlant(helperPlant.type);
        //    }
        //}
        //else
        //{
        //    GetComponent<Button>().interactable = false;
        //}
        //previousPlantableState = currentPlantableState;
    }
}
