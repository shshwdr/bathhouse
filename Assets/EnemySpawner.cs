using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Transform spawnerPositionParent;
    public float generateTime = 1f;
    float currentGenerateTime;
    BattleRegionController battleRegion;
    // Start is called before the first frame update
    void Awake()
    {
        spawnerPositionParent = transform;
        battleRegion = GetComponentInParent<BattleRegionController>();

    }

    void spawnEnemy()
    {
        int rand = Random.Range(0, spawnerPositionParent.childCount);
        Transform selectedPosition = spawnerPositionParent.GetChild(rand);
        if (selectedPosition.childCount > 0)
        {
            return;
        }

        var enemyPrefab = Resources.Load<GameObject>("enemy/slime");
        var enemy = Instantiate(enemyPrefab, selectedPosition.position, Quaternion.identity, selectedPosition);
        battleRegion.addEnemy(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        currentGenerateTime += Time.deltaTime;
        if(currentGenerateTime>= generateTime)
        {
            currentGenerateTime = 0;
            spawnEnemy();
        }


    }
}
