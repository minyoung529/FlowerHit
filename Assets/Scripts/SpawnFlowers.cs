using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlowers : MonoBehaviour
{
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private GameObject apple;
    private List<Flower> currentFlowers;
    private List<FlowerObject> flowerObjects = new List<FlowerObject>();

    int count = 0;


    
    public void FlowerSpawn(Flower flower)
    {
        GameObject obj = Instantiate(apple);
        FlowerObject flowerObj = obj.GetComponent<FlowerObject>();

        obj.transform.SetParent(transform);
        SetRandomPos(obj);

        for (int i = 0; i < flowerObjects.Count; i++)
        {
            while(Vector2.Distance(flowerObjects[i].transform.localPosition, obj.transform.localPosition) < 0.4f)
            {
                SetRandomPos(obj);
                i = 0;
            }
        }

        flowerObj.SetFlower(flower);
        flowerObjects.Add(flowerObj);
    }

    private void SetRandomPos(GameObject obj)
    {
        float y = Random.Range(-col.radius, col.radius);
        float x = Mathf.Sqrt(Mathf.Pow(col.radius, 2) - Mathf.Pow(y, 2));

        if (Random.Range(0, 2) == 0)
            x *= -1;

        obj.transform.localPosition = new Vector2(x, y);
    }
}
