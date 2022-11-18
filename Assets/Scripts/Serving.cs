using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Serving : MonoBehaviour
{
    private GameObject scoreText;
    CustomerSpawner customerSpawner;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Food"))
        {
            customerSpawner = GameObject.FindGameObjectWithTag("CustomerSpawner").GetComponent<CustomerSpawner>();
            if(customerSpawner.CheckIfServingIsCorrect(other.gameObject, this.gameObject))
            {
                int textSum = 0;
                int.TryParse(scoreText.GetComponent<TextMeshPro>().text, out textSum);
                textSum += 5;
                scoreText.GetComponent<TextMeshPro>().text = textSum.ToString();
                Destroy(other.gameObject);
                customerSpawner.DestroyCustomer(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        scoreText = GameObject.Find("ScoreCounter");
    }
}
