using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RandomObjectPlacement : MonoBehaviour
{
    public GameObject plane;
    public GameObject prefabToPlace;
    public int numberOfObjects = 10;
    public bool placeInGroups = false;
    public int groupSize = 3;
    public float groupRadius = 1f;

    private List<GameObject> objectPool = new List<GameObject>();

    void Start()
    {
        StartCoroutine(PlaceObjectsAsync());
    }

    IEnumerator PlaceObjectsAsync()
    {
        if (plane == null || prefabToPlace == null)
        {
            Debug.LogError("Plane o Prefab no asignados.");
            yield break;
        }

        Bounds planeBounds = plane.GetComponent<Renderer>().bounds;
        List<Vector3> placedPositions = new List<Vector3>();

        // Inicializar el object pool
        for (int i = 0; i < numberOfObjects * 2; i++)
        {
            GameObject obj = Instantiate(prefabToPlace, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            objectPool.Add(obj);
        }

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition = GetRandomPositionInPlane(planeBounds);

            placedPositions.Add(randomPosition);
            GameObject obj = GetObjectFromPool();
            if (obj != null)
            {
                obj.transform.position = randomPosition;
                obj.SetActive(true);
            }

            if (placeInGroups)
            {
                PlaceGroup(randomPosition, placedPositions);
            }

            yield return null;
        }
    }

    Vector3 GetRandomPositionInPlane(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(randomX, bounds.center.y, randomZ);
    }

    void PlaceGroup(Vector3 position, List<Vector3> placedPositions)
    {
        for (int i = 0; i < groupSize - 1; i++)
        {
            Vector3 groupPosition = position + Random.insideUnitSphere * groupRadius;
            groupPosition.y = position.y;

            placedPositions.Add(groupPosition);
            GameObject obj = GetObjectFromPool();
            if (obj != null)
            {
                obj.transform.position = groupPosition;
                obj.SetActive(true);
            }
        }
    }

    GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        return null; // No hay objetos disponibles en el grupo
    }
}