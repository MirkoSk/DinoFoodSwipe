using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Animal : MonoBehaviour
{
    [SerializeField] Diet diet;

    public Diet Diet { get { return diet; } }
}