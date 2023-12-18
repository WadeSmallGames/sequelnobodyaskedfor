using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int amount;
    List<GameObject> objects;

    private void Start()
    {
        for(int i = 0; i < amount; i++)
        {
            var obj = Instantiate(objectToSpawn, transform);
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    public void Spawn<T>(out T output) => Spawn().TryGetComponent(out output);

    public GameObject Spawn()
    {
        foreach (var obj in objects)
        {
            if (!obj.activeInHierarchy)
                obj.SetActive(true);
            return obj;
        }

        return null;
    }
}
