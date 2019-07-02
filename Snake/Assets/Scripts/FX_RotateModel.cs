// Nota:
// Utilizo "FX_" para Mono Behaviours que exercem função 
// especifica de efeitos, particulas etc..

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_RotateModel : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 100f * Time.deltaTime, 0));
    }
}
