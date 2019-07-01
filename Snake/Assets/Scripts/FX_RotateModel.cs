// Nota:
// Utilizo "FX_" para Mono Behaviors que exercem função especifica de efeitos, particulas etc..

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_RotateModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 100f * Time.deltaTime, 0));
    }
}
