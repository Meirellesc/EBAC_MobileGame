using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string collectorTag = "Player";
    public float timeToHide = 3f;

    public GameObject graphicItem;
    //public ParticleSystem particleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag(collectorTag))
        {
            OnCollect();
        }
    }

    protected virtual void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        PlayerController.Instance.DoBounce();

        //particleSystem?.Play();
        //audioSource?.Play();
    }

}
