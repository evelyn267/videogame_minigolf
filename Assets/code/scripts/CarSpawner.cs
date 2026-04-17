using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float respawnDelay = 1f;

    private GameObject currentCar;

    private void Start()
    {
        SpawnCar();
    }

    public void SpawnCar()
    {
        currentCar = Instantiate(carPrefab, pointA.position, Quaternion.identity);
        CarObstacle car = currentCar.GetComponent<CarObstacle>();
        car.Setup(pointA, pointB, this);
    }

    public void CarDestroyed()
    {
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnCar();
    }
}