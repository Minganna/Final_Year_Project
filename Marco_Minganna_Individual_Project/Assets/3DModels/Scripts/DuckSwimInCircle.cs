using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSwimInCircle : MonoBehaviour
{
    Transform parent;
    Transform child;
    // Start is called before the first frame update
    void Start()
    {
        child = this.transform;
         parent = this.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        child.RotateAround(parent.position, Vector3.up, 10 * Time.deltaTime);

    }
}
