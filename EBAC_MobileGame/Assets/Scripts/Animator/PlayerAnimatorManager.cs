using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimatorManager : MonoBehaviour
{
    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public Animator Animator;
    public List<PlayerAnimatorSetup> PlayerAnimatorSetup;

    public void Play(AnimationType type, float speedFactor = 1f)
    {
        try
        {
            PlayerAnimatorSetup animatorSetup = PlayerAnimatorSetup.Find(data => data.AnimationType == type);
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
public class PlayerAnimatorSetup
{
    public PlayerAnimatorManager.AnimationType AnimationType;
    public string Trigger;
    public float Speed = 1f;
}
