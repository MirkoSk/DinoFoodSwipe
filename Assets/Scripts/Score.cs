using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Score : MonoBehaviour
{
    [SerializeField] float scoringDurationRightAnswer = 5f;
    [SerializeField] float scoringDurationWrongAnswer = 2f;

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

    int currentScore;
    AudioSource audioSource;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameEvents.FoodGiven += UpdateScore;
    }

    private void OnDisable()
    {
        GameEvents.FoodGiven -= UpdateScore;
    }



    void UpdateScore(int points, Animal animalFed = null, FoodType foodGiven = null)
    {
        currentScore += points;
        scoreCounter.text = currentScore.ToString();

        if (points > 0)
        {
            rightAnswerParticles.Play();
            audioSource.PlayOneShot(rightAnswerSound, rightAnswerVolume);

            StartCoroutine(Wait(scoringDurationRightAnswer, () => { GameEvents.EndScoring(currentScore); }));
        }
        else if (points < 0)
        {
            wrongAnswerParticles.Play();
            audioSource.PlayOneShot(wrongAnswerSound, wrongAnswerVolume);

            StartCoroutine(Wait(scoringDurationWrongAnswer, () => { GameEvents.EndScoring(currentScore); }));
        }
    }



    IEnumerator Wait(float seconds, System.Action onComplete)
    {
        yield return new WaitForSeconds(seconds);

        onComplete.Invoke();
    }
}
