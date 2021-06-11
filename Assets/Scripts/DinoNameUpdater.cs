using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DinoNameUpdater : MonoBehaviour
{
    [SerializeField] Animal dinoToName = null;
    [SerializeField] float fadeDuration = 0.5f;

    TextMeshProUGUI dinoName;



    private void Awake()
    {
        dinoName = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameEvents.DinoRetreated += HideName;
        GameEvents.DinoComingIn += UpdateName;
    }

    private void OnDisable()
    {
        GameEvents.DinoRetreated -= HideName;
        GameEvents.DinoComingIn -= UpdateName;
    }



    void UpdateName(Animal animalSwitching)
    {
        // Only act if the assigned animal is acting
        if (animalSwitching != dinoToName) return;

        dinoName.text = dinoToName.CurrentDino.Name;
        dinoName.enabled = true;

        dinoName.DOKill();
        dinoName.color = new Color(dinoName.color.r, dinoName.color.g, dinoName.color.b, 0f);
        dinoName.DOFade(1f, fadeDuration);
    }

    void HideName(Animal animalSwitching)
    {
        // Only act if the assigned animal is acting
        if (animalSwitching != dinoToName) return;

        dinoName.DOFade(0f, fadeDuration).OnComplete(() => 
        {
            dinoName.enabled = false;
        });
    }
}
