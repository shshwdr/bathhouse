using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGeneration : MonoBehaviour
{
    public Vector2 customerGenerationTimeMinMax = new Vector2(1f, 2f);
    float customerCurrentGenerationTime;
    float customerGenerationTimer = 0;

    public Transform generationPositionParent;

    // Start is called before the first frame update
    void Start()
    {
        resetCustomerGenerationTimer();
    }

    void resetCustomerGenerationTimer()
    {
        customerCurrentGenerationTime = Random.Range(customerGenerationTimeMinMax.x, customerGenerationTimeMinMax.y);
        customerGenerationTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        customerGenerationTimer += Time.deltaTime;
        if (customerGenerationTimer >= customerCurrentGenerationTime)
        {
            resetCustomerGenerationTimer();
            var availableBedrooms = RoomItemManager.Instance.availableBedItem();
            if (availableBedrooms.Count > 0)
            {
                var selectedRoom = availableBedrooms[0];
                selectedRoom.customerOccupy();
                Transform generationTransform = generationPositionParent.GetChild(Random.Range(0, generationPositionParent.childCount));

                GameObject go = CustomerManager.Instance.createCustomer(generationTransform.position, selectedRoom);
                

            }
        }
    }
}
