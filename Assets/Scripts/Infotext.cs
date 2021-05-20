using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Infotext : MonoBehaviour
{
    [Space]
    [SerializeField] float popupScaleDuration = 0.5f;
    [SerializeField] Ease easingCurve = Ease.Linear;

    [Header("References")]
    [SerializeField] TextMeshPro textMesh;
    [SerializeField] SpriteRenderer foodSpriteRenderer;


    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.FoodGiven += ShowText;
        GameEvents.ScoringEnded += HideText;
    }

    private void OnDisable()
    {
        GameEvents.FoodGiven -= ShowText;
        GameEvents.ScoringEnded -= HideText;
    }



    void ShowText(int points, Animal animalFed, FoodType foodGiven)
    {
        // Only show info when answer is correct
        if (points < 0) return;

        textMesh.text = foodGiven.Info;
        foodSpriteRenderer.sprite = foodGiven.Sprite;

        transform.localScale = Vector3.zero;
        transform.GetChild(0).gameObject.SetActive(true);

        transform.DOScale(1f, popupScaleDuration).SetEase(easingCurve);
    }

    void HideText(int points)
    {
        textMesh.text = "";
        foodSpriteRenderer.sprite = null;

        transform.GetChild(0).gameObject.SetActive(false);
    }
}
