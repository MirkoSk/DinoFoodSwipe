using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{



    private void OnEnable()
    {
        GameEvents.FoodGiven += UpdateScore;
    }

    private void OnDisable()
    {
        GameEvents.FoodGiven -= UpdateScore;
    }



    void UpdateScore(Diet diet)
    {
        Debug.Log("Food recieved: " + diet);
    }
}
