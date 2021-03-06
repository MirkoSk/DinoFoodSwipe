using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SpeechBubbles : MonoBehaviour
{
    [Space]
    [SerializeField] float popupScaleDuration = 0.3f;
    [SerializeField] Ease easingCurve = Ease.Linear;

    [Header("References")]
    [SerializeField] GameObject rightAnswerSpeechBubble;
    [SerializeField] GameObject wrongAnswerSpeechBubble;


    void Awake()
    {
        HideSpeechBubbles();
    }

    private void OnEnable()
    {
        GameEvents.FoodGiven += ShowSpeechBubble;
        GameEvents.DinoRetreated += HideSpeechBubbles;
        GameEvents.ScoringEnded += HideSpeechBubbles;
    }

    private void OnDisable()
    {
        GameEvents.FoodGiven -= ShowSpeechBubble;
        GameEvents.DinoRetreated -= HideSpeechBubbles;
        GameEvents.ScoringEnded += HideSpeechBubbles;
    }

    void ShowSpeechBubble(int points, Animal animalFed, FoodType foodGiven)
    {
        if (points > 0)
        {
            rightAnswerSpeechBubble.SetActive(true);
            rightAnswerSpeechBubble.transform.localScale = Vector3.zero;
            rightAnswerSpeechBubble.transform.position = animalFed.SpeechBubblePosition.position;

            if (animalFed.DinoFacingLeft)
            {
                rightAnswerSpeechBubble.GetComponentInChildren<TextMeshPro>().transform.localScale = new Vector3(1f, 1f, 1f);
                rightAnswerSpeechBubble.transform.DOScale(1f, popupScaleDuration).SetEase(easingCurve);
            }
            else if (!animalFed.DinoFacingLeft)
            {
                rightAnswerSpeechBubble.GetComponentInChildren<TextMeshPro>().transform.localScale = new Vector3(-1f, 1f, 1f);
                rightAnswerSpeechBubble.transform.DOScale(new Vector3(-1f, 1f, 1f), popupScaleDuration).SetEase(easingCurve);
            }
        }
        else if (points < 0)
        {
            wrongAnswerSpeechBubble.SetActive(true);
            wrongAnswerSpeechBubble.transform.localScale = Vector3.zero;
            wrongAnswerSpeechBubble.transform.position = animalFed.SpeechBubblePosition.position;

            if (animalFed.DinoFacingLeft)
            {
                wrongAnswerSpeechBubble.GetComponentInChildren<TextMeshPro>().transform.localScale = new Vector3(1f, 1f, 1f);
                wrongAnswerSpeechBubble.transform.DOScale(1f, popupScaleDuration).SetEase(easingCurve);
            }
            else if (!animalFed.DinoFacingLeft)
            {
                wrongAnswerSpeechBubble.GetComponentInChildren<TextMeshPro>().transform.localScale = new Vector3(-1f, 1f, 1f);
                wrongAnswerSpeechBubble.transform.DOScale(new Vector3(-1f, 1f, 1f), popupScaleDuration).SetEase(easingCurve);
            }
        }
    }

    void HideSpeechBubbles(Animal animalSwitching = null)
    {
        rightAnswerSpeechBubble.SetActive(false);
        wrongAnswerSpeechBubble.SetActive(false);
    }

    void HideSpeechBubbles(int points)
    {
        HideSpeechBubbles();
    }
}
