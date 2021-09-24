using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BuildItemButton : MonoBehaviour
{

    public TMP_Text name;
    public Image image;
    bool previousPlantableState = false;


    public GameObject explainPanel;
    public TMP_Text explainText;

    string itemName;
    protected GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        init("bedroom");
    }

    protected abstract string itemType();
    public void init(string n)
    {
        itemName = n;
        prefab = Resources.Load<GameObject>("item/"+itemType() + "/" + itemName);
        //spawnPlantPrefab = plant;
        //helperPlant = plant.GetComponent<HelperPlant>();
        //name.text = PlantsManager.Instance.plantName[helperPlant.type];
        image.sprite = Resources.Load<Sprite>("icon/" + itemType() + "/" + itemName);
        //image.color = plant.GetComponent<SpriteRenderer>().color;

    }

    public void OnMouseDown()
    {
        if (CanPurchaseItem())
        {
            StartCoroutine(delaySpawn());
        }
    }

    IEnumerator delaySpawn()
    {
        yield return new WaitForSeconds(0.1f);

        SpawnItem();
    }

    protected abstract void SpawnItem();
    protected abstract bool CanPurchaseItem();



    public void PointerEnter()
    {
        explainPanel.SetActive(true);
    }
    public void PointerExit()
    {
        explainPanel.SetActive(false);
    }
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
