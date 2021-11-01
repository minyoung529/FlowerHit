using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlowers : MonoBehaviour
{
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private GameObject flower;
    private List<FlowerObject> flowerObjects = new List<FlowerObject>();

    private readonly string flowerObjectName = "Flower";

    public void FlowerSpawn(Flower flower)
    {
        Debug.Log("spawn");
        GameObject obj = SpawnOrPool();
        FlowerObject flowerObj = flowerObjects.Find(flower => flower.gameObject == obj);
        flowerObj ??= obj.GetComponent<FlowerObject>();

        obj.transform.SetParent(transform);
        SetRandomPos(obj);

        for (int i = 0; i < flowerObjects.Count; i++)
        {
            while (flowerObjects[i].gameObject.activeSelf && Vector2.Distance(flowerObjects[i].transform.localPosition, obj.transform.localPosition) < 0.35f)
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

    public void DespawnFlowers()
    {
        foreach (FlowerObject obj in flowerObjects)
        {
            obj.Despawn();
        }
    }

    private GameObject SpawnOrPool()
    {
        if (GameManager.Instance.CheckPool(flowerObjectName))
        {
            return GameManager.Instance.ReturnPoolObject(flowerObjectName);
        }

        else
        {
            return Instantiate(flower);
        }
    }
}
