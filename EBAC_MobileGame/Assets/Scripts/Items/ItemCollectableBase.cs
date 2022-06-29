using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string collectorTag = "Player";
    public float timeToHide = 3f;

    public GameObject graphicItem;
    public ParticleSystem ParticleSystem;

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

        if (ParticleSystem != null)
        {
            ParticleSystem.transform.SetParent(null);
            ParticleSystem.Play();

            Invoke(nameof(StopParticleSystem), 1f);
        }
        //audioSource?.Play();
    }

    private void StopParticleSystem()
    {
        if(ParticleSystem != null)
        {
            ParticleSystem.Stop();
        }
    }

}
