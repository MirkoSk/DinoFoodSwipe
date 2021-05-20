using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Animal : MonoBehaviour
{
    [SerializeField] Diet diet;
    [SerializeField] Transform speechBubblePosition;
    [SerializeField] bool speechBubbleFacingRight;

    public Diet Diet { get { return diet; } }
    public Transform SpeechBubblePosition { get => speechBubblePosition; }
    public bool SpeechBubbleFacingRight { get => speechBubbleFacingRight; }
}