using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using DG.Tweening;

public class Popup : MonoBehaviour
{
    public TMP_Text textLabel;
    static public void createPopup(string text,Transform parent)
    {
        var prefab = Resources.Load<GameObject>("UI/popup");
        var go = Instantiate(prefab, parent);
        go.GetComponent<Popup>().textLabel.text = text;
        go.transform.DOJump(go.transform.position, 1, 1, 1);
        Destroy(go, 1);


        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
