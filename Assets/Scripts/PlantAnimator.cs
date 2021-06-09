using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlantAnimator : MonoBehaviour
{
    [SerializeField] Animal dinoToReactTo = null;

    [Space]
    [SerializeField] float animDuration = 1.5f;
    [SerializeField] float animStrength = 45f;
    [SerializeField] int animVibrato = 10;
    [SerializeField] Ease animEase = Ease.Linear;

    [Space]
    [SerializeField] AudioSource audioSource = null;



    private void OnEnable()
    {
        GameEvents.DinoRetreated += Animate;
        GameEvents.DinoComingIn += Animate;
    }

    private void OnDisable()
    {
        GameEvents.DinoRetreated -= Animate;
        GameEvents.DinoComingIn -= Animate;
    }



    void Animate(Animal animalSwitching)
    {
        if (dinoToReactTo != animalSwitching || DOTween.IsTweening(transform)) return;

        if (audioSource) audioSource.Play();
        transform.DOShakeRotation(animDuration, animStrength, animVibrato).SetEase(animEase);
    }
}
