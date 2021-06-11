using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

[System.Serializable]
public class Dino
{
    public string Name;
    public Sprite Sprite;
    public Vector2 spriteOffset;
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
    [SerializeField] AudioSource audioSource;

    Dino currentDino;

    public Diet Diet { get { return diet; } }
    public Transform SpeechBubblePosition { get => speechBubblePosition; }
    public bool DinoFacingLeft { get => dinoFacingLeft; }
    public Dino CurrentDino { get => currentDino; }



    private void Start()
    {
        SwitchCurrentDino();
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
            if (audioSource) audioSource.Play();

            SwitchAnimal();
        }
    }

    void SwitchAnimal(float delay = -1f, bool tweenOut = true)
    {        
        float tweeningEndValue = 0f;
        if (dinoFacingLeft) tweeningEndValue = moveOutEndValue;
        else tweeningEndValue = -moveOutEndValue;
        
        float tweenDelay = 0f;
        if (delay == -1f) tweenDelay = moveOutDelay;
        else tweenDelay = delay;

        float tweenOutDuration = 0f;
        if (tweenOut) tweenOutDuration = moveOutDuration;
        else tweenOutDuration = 0f;

        dinoSpriteRenderer.transform.DOLocalMoveX(tweeningEndValue, tweenOutDuration).SetEase(moveOutEase).SetDelay(tweenDelay).OnStart(() => 
        {
            GameEvents.RetreatDino(this);
        }).OnComplete(() =>
        {
            SwitchCurrentDino();
            GameEvents.MoveDinoIn(this);

            dinoSpriteRenderer.transform.DOLocalMoveX(currentDino.spriteOffset.x, moveOutDuration).SetEase(moveOutEase);
        });
    }

    void SwitchCurrentDino()
    {
        List<Dino> alternateDinos = new List<Dino>();
        alternateDinos.AddRange(dinos);
        if (currentDino != null && dinos.Count > 1) alternateDinos.Remove(currentDino);

        Dino newDino = alternateDinos[Random.Range(0, alternateDinos.Count)];

        dinoSpriteRenderer.sprite = newDino.Sprite;
        dinoSpriteRenderer.transform.localPosition = new Vector3(dinoSpriteRenderer.transform.localPosition.x, newDino.spriteOffset.y, dinoSpriteRenderer.transform.localPosition.z);

        currentDino = newDino;
    }
}