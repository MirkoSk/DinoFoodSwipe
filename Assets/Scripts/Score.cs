using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Score : MonoBehaviour
{
    [Header("Answer Popups")]
    [SerializeField] float popupDuration = 3f;
    [SerializeField] float popupScaleDuration = 0.3f;
    [SerializeField] Ease easingCurve = Ease.Linear;

    [Header("Particles")]
    [SerializeField] ParticleSystem rightAnswerParticles;
    [SerializeField] ParticleSystem wrongAnswerParticles;

    [Header("Sounds")]
    [SerializeField] AudioClip rightAnswerSound;
    [Range(0f,1f)]
    [SerializeField] float rightAnswerVolume = 1f;
    [SerializeField] AudioClip wrongAnswerSound;
    [Range(0f, 1f)]
    [SerializeField] float wrongAnswerVolume = 1f;

    [Header("References")]
    [SerializeField] TextMeshProUGUI scoreCounter;
    [SerializeField] GameObject rightAnswerPopup;
    [SerializeField] GameObject wrongAnswerPopup;

    int currentScore;
    AudioSource audioSource;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        rightAnswerPopup.SetActive(false);
        wrongAnswerPopup.SetActive(false);
    }

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
            rightAnswerPopup.transform.localScale = Vector3.zero;
            rightAnswerPopup.transform.DOScale(1f, popupScaleDuration).SetEase(easingCurve);

            rightAnswerParticles.Play();
            audioSource.PlayOneShot(rightAnswerSound, rightAnswerVolume);
        }
        else
        {
            wrongAnswerPopup.SetActive(true);
            wrongAnswerPopup.transform.localScale = Vector3.zero;
            wrongAnswerPopup.transform.DOScale(1f, popupScaleDuration).SetEase(easingCurve);

            wrongAnswerParticles.Play();
            audioSource.PlayOneShot(wrongAnswerSound, wrongAnswerVolume);
        }

        scoreCounter.text = "Punkte: " + currentScore;
    }



    IEnumerator Wait(float seconds, System.Action onComplete)
    {
        yield return new WaitForSeconds(seconds);

        onComplete.Invoke();
    }
}
