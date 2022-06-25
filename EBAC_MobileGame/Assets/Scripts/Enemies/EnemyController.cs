using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ScaleHelper ScaleHelper;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 endScale = transform.localScale;

        transform.localScale = Vector3.zero;

        ScaleHelper.ScaleByEndValue(endScale);
    }
}
