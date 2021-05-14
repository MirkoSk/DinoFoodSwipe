using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreCounter;
    [SerializeField] GameObject rightAnswerPopup;
    [SerializeField] GameObject wrongAnswerPopup;
    [SerializeField] float popupDuration = 3f;

    int currentScore;



    private void OnEnable()
    {
        GameEvents.FoodGiven += UpdateScore;
    }

    private void OnDisable()
    {
        GameEvents.FoodGiven -= UpdateScore;
    }



    void UpdateScore(int points)
    {
        currentScore += points;

        if (points > 0) ShowScoringPopup(true);
        else ShowScoringPopup(false);

        StartCoroutine(Wait(popupDuration, () =>
        {
            scoreCounter.text = "Punkte :" + currentScore;

            rightAnswerPopup.SetActive(false);
            wrongAnswerPopup.SetActive(false);

            GameEvents.UpdateScore(currentScore);
        }));
    }

    void ShowScoringPopup(bool rightAnswer)
    {
        if (rightAnswer)
        {
            rightAnswerPopup.SetActive(true);
        }
        else
        {
            wrongAnswerPopup.SetActive(true);
        }
    }



    IEnumerator Wait(float seconds, System.Action onComplete)
    {
        yield return new WaitForSeconds(seconds);

        onComplete.Invoke();
    }
}
