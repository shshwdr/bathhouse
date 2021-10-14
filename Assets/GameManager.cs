using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KeyValueBase
{
    public string key;
    public int amount;

}

public class InfoBase
{
    public string name;
    public string displayName;
    public string description;
}

public class InfoWithRequirementBase : InfoBase
{
    public KeyValueBase[] requireResources;
}
public class GameManager : Singleton<GameManager>
{

    public bool debugCustomerBehavior = true;

    public int[] gameSpeeds = new int[] { 0, 1, 2, 4 ,8};
    public int gameSpeedIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gameSpeedIndex++;
            if (gameSpeedIndex >= gameSpeeds.Length)
            {
                gameSpeedIndex = 0;
            }
            Time.timeScale = gameSpeeds[gameSpeedIndex];
        }
    }
}
