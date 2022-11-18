using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using TMPro;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnpointsList;
    [SerializeField] private GameObject[] peopleList;
    [SerializeField] private GameObject[] foodList;
    [SerializeField] private float spawnDelay = 10;
    [SerializeField] private TextMeshPro order;
    
    private float nextSpawnTime = 0;
    private Dictionary<int, bool> isSpawnAvailableList = new Dictionary<int, bool>();
    private Dictionary<GameObject, int> personToSeat = new Dictionary<GameObject, int>();
    private void Awake()
    {
        for (int i = 0; i < spawnpointsList.Length; i++)
        {
            spawnpointsList[i].GameObject().GetComponent<Renderer>().enabled = false;
            isSpawnAvailableList.Add(i, true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldSpawn())
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        nextSpawnTime = Time.time + spawnDelay;

        if(IsAnySpawnAvailable())
        {
            int index;
            do
            {
                index = Random.Range(0, spawnpointsList.Length);

            } while (isSpawnAvailableList[index] == false);

			int personIndex = Random.Range(0, peopleList.Length);
            int indexFood = Random.Range(0, foodList.Length);

            GameObject person = Instantiate(peopleList[personIndex]
                , spawnpointsList[index].transform.position
                , spawnpointsList[index].transform.rotation);

            personToSeat.Add(person, index);

            person.GetNamedChild("OrderCanvas").GetNamedChild("OrderText").GetComponent<TextMeshProUGUI>().text = foodList[indexFood].name;
            Debug.Log(person.GetNamedChild("OrderCanvas").name);
            Debug.Log(person.GetNamedChild("OrderCanvas").GetNamedChild("OrderText").name);
            Debug.Log(person.GetNamedChild("OrderCanvas").GetNamedChild("OrderText").GetComponent<TextMeshProUGUI>().text);
            isSpawnAvailableList[index] = false;

        }
    }
    

    private bool ShouldSpawn()
    {
        return Time.time > nextSpawnTime;
    }

    private bool IsAnySpawnAvailable()
    {
        for (int i = 0; i < isSpawnAvailableList.Count; i++)
        {
            if (isSpawnAvailableList[i])
                return true;
        }
        return false;
    }

    public void DestroyCustomer(GameObject gameObject)
    {
        int index = personToSeat[gameObject];
        isSpawnAvailableList[index] = true;
        //DestroyImmediate(personToFood[gameObject], true);
        Destroy(gameObject);
    }

    public bool CheckIfServingIsCorrect(GameObject food, GameObject person)
    {

        string customerChoice = person.GetNamedChild("OrderCanvas").GetNamedChild("OrderText").GetComponent<TextMeshProUGUI>().text;
        if (food.name == customerChoice + "(Clone)") return true;
        return false;
    }
}
