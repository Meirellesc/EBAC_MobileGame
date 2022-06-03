using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string playerTag = "Player";
    public float timeToHide = 3f;

    public GameObject graphicItem;
    //public ParticleSystem particleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag(playerTag))
        {
            Collect();
        }
    }

    private void Collect()
    {
        Invoke(nameof(HideObject), timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        //particleSystem?.Play();
        //audioSource?.Play();
    }

}
