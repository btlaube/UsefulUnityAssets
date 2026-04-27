using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTransition", menuName = "Custom/New Transition")]
public class Transition : ScriptableObject
{

    public GameObject transitionObject;
    public string[] scenesToLoad;
    public string[] scenesToUnload;
    public float transitionDuration;

}
