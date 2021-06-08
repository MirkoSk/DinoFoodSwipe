using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



[System.Serializable]
public class Dino
{
    public string Name;
    public Sprite Sprite;
}

public class Animal : MonoBehaviour
{
    [SerializeField] Diet diet;
    [SerializeField] bool dinoFacingLeft;
    [SerializeField] float moveOutDelay = 3f;
    [SerializeField] float moveOutEndValue;
    [SerializeField] float moveOutDuration = 1f;
    [SerializeField] Ease moveOutEase = Ease.InOutQuad;

    [Space]
    [SerializeField] List<Dino> dinos = new List<Dino>();

    [Header("References")]
    [SerializeField] Transform speechBubblePosition;
    [SerializeField] SpriteRenderer dinoSpriteRenderer;

    Dino currentDino;

    public Diet Diet { get { return diet; } }
    public Transform SpeechBubblePosition { get => speechBubblePosition; }
    public bool DinoFacingLeft { get => dinoFacingLeft; }



    private void Start()
    {
        SwitchAnimal(0f, false);
    }

    private void OnEnable()
    {
        GameEvents.FoodGiven += HandleFoodRecieved;
    }

    private void OnDisable()
    {
        GameEvents.FoodGiven -= HandleFoodRecieved;
    }



    void HandleFoodRecieved(int points, Animal animalFed, FoodType foodGiven)
    {
        // only act if this animal was fed
        if (animalFed != this) return;

        if (foodGiven.Diet == diet)
        {
            SwitchAnimal();
        }
    }

    void SwitchAnimal(float delay = -1f, bool tweenOut = true)
    {
        float tweeningStart = dinoSpriteRenderer.transform.localPosition.x;
        
        float tweeningEndValue = 0f;
        if (dinoFacingLeft) tweeningEndValue = moveOutEndValue;
        else tweeningEndValue = -moveOutEndValue;
        
        float tweenDelay = 0f;
        if (delay == -1f) tweenDelay = moveOutDelay;
        else tweenDelay = delay;

        float tweenOutDuration = 0f;
        if (tweenOut) tweenOutDuration = moveOutDuration;
        else tweenOutDuration = 0f;

        dinoSpriteRenderer.transform.DOLocalMoveX(tweeningEndValue, tweenOutDuration).SetEase(moveOutEase).SetDelay(tweenDelay).OnComplete(() =>
        {
            SwitchCurrentDino();

            dinoSpriteRenderer.transform.DOLocalMoveX(tweeningStart, moveOutDuration).SetEase(moveOutEase);
        });
    }

    void SwitchCurrentDino()
    {
        List<Dino> alternateDinos = new List<Dino>();
        alternateDinos.AddRange(dinos);
        if (currentDino != null) alternateDinos.Remove(currentDino);

        Dino newDino = alternateDinos[Random.Range(0, alternateDinos.Count)];

        dinoSpriteRenderer.sprite = newDino.Sprite;

        currentDino = newDino;
    }
}