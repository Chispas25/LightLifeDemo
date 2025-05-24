using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzObjFin : MonoBehaviour
{
    public SegmentedLaser scriptLaser;
    public Light2D scriptLuz;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
        void Update()
    {
        if (scriptLaser.luzObjFinal == false)
        {
            scriptLuz.enabled = false;
        }
        else
        {
            scriptLuz.enabled = true;
        }
    }
}
