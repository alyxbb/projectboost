using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Moveingobjects : MonoBehaviour {

    [SerializeField] Vector3 movementvector = new Vector3(10f, 0f, 0f);
    [Range(0,1)]float movementfactor;
    [SerializeField] float period = 2f;

    Vector3 startingpos;
    // Use this for initialization
    void Start () {
        startingpos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (period <= Mathf.Epsilon) { return; };
        
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawsinwave = Mathf.Sin(cycles * tau);

        movementfactor = rawsinwave / 2f + 0.5f;
        Vector3 offset = movementvector * movementfactor;
        transform.position = startingpos + offset;
        
	}
}
