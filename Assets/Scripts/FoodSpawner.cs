using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    //bool hasSpawned = false;
    [SerializeField] GameObject[] foodList;
    [SerializeField] float spawnDelay = 1;
    private int foodIndex = 0;
    private void Awake()
    {
        this.GameObject().GetComponent<Renderer>().enabled = false;
    }

    private void Start()
    {
        foodIndex = Random.Range(0, foodList.Length);
        Instantiate(foodList[foodIndex], this.transform.position, this.transform.rotation);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            StartCoroutine("Spawn");
        }
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        foodIndex = Random.Range(0, foodList.Length);
        Instantiate(foodList[foodIndex], this.transform.position, this.transform.rotation);
    }
}
