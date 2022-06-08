using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatorManager : MonoBehaviour
{
    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public Animator Animator;
    public List<AnimatorSetup> AnimatorSetups;

    public void Play(AnimationType type, float speedFactor = 1f)
    {
        try
        {
            AnimatorSetup animatorSetup = AnimatorSetups.Find(data => data.AnimationType == type);
            Animator.SetTrigger(animatorSetup.Trigger);
            Animator.speed = animatorSetup.Speed * speedFactor;
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }

    }
}

[Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType AnimationType;
    public string Trigger;
    public float Speed = 1f;
}
