using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]
public class BrickData : ScriptableObject
{
    public Sprite[] sprites;
    public AnimationClip[] animations;
    public AnimatorController[] animators;
}
