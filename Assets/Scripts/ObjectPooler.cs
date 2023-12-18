using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] int amount;
    List<GameObject> objects = new();

    private void Start()
    {
        for(int i = 0; i < amount; i++)
        {
            var obj = Instantiate(objectToSpawn, transform);
            obj.transform.localPosition = Vector3.zero;
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
            {
                obj.SetActive(true);
                obj.transform.SetParent(null);
                ResetObject(obj);
                return obj;
            }
        }

        return null;
    }

    async void ResetObject(GameObject obj)
    {
        await Task.Delay(8 * 1000);

        if (!Application.isPlaying) return;

        if (!obj.activeInHierarchy) return;

        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
    }
}
