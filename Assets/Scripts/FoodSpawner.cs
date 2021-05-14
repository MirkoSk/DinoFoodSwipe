using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] List<FoodType> foods = new List<FoodType>();
    [SerializeField] GameObject foodPrefab;



    private void Start()
    {
        SpawnFood(0);
    }

    private void OnEnable()
    {
        GameEvents.ScoreUpdated += SpawnFood;
    }

    private void OnDisable()
    {
        GameEvents.ScoreUpdated -= SpawnFood;
    }



    void SpawnFood(int pointsTotal)
    {
        Food food = Instantiate(foodPrefab, transform.position, Quaternion.identity).GetComponent<Food>();
        food.Initialize(foods[Random.Range(0, foods.Count)]);
    }
}
