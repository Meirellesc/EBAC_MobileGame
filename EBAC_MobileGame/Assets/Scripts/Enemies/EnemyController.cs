using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ScaleHelper ScaleHelper;

    public ParticleSystem ParticleSystem;

    public string TagPlayer = "Player";

    // Start is called before the first frame update
    void Start()
    {
        Vector3 endScale = transform.localScale;

        transform.localScale = Vector3.zero;

        ScaleHelper.ScaleByEndValue(endScale);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(TagPlayer))
        {
            PlayerController player;
            bool check = collision.gameObject.TryGetComponent<PlayerController>(out player);

            if (check && player != null)
            {
                if (player.CheckIsInvencible())
                {
                    if (ParticleSystem != null)
                    {
                        gameObject.SetActive(false);

                        ParticleSystem.transform.SetParent(null);
                        ParticleSystem.transform.localScale = Vector3.one;
                        ParticleSystem.Play();

                        Invoke(nameof(StopParticleSystem), 1f);
                    }
                }
            }
        }
    }

    private void StopParticleSystem()
    {
        if (ParticleSystem != null)
        {
            ParticleSystem.Stop();
        }
    }
}
