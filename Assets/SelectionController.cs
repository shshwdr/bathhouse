using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SelectionController : MonoBehaviour
{
    public Transform contentParent;

    protected abstract int allItemInfoCount();
    protected abstract InfoBase itemInfo(int i);
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
       // var allInfo = allItemInfo();
        for (; i < allItemInfoCount(); i++)
        {
            contentParent.GetChild(i).gameObject.SetActive(true);

            BuildItemButton button = contentParent.GetChild(i).GetComponent<BuildItemButton>();
            button.init(itemInfo(i));
        }
        for (; i < contentParent.childCount; i++)
        {
            contentParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
