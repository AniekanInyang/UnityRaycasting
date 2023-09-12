using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class Raycastscript : MonoBehaviour
{
    new public Camera camera;
    Vector3 targetDirection;
    Vector3 sourcePosition;
    public RaycastHit hit;
    public RaycastHit[] hits;
    
    // Start is called before the first frame update
    void Start()
    {        
        targetDirection = new Vector3(-194, -5, -197);

        if(camera == null) 
        {
            Debug.Log("camera is null");
            camera = GetComponent<Camera>();
            sourcePosition = camera.transform.position;
            Debug.Log(sourcePosition);
        }

        RayCast_fn();
        RaycastAll_fn();
        LineCast_fn();   
        Layermask_fn(new Vector3(194, -5, 197));
    }

    // Update is called once per frame
    void Update ()
    {
         
    }

    void RayCast_fn()
    {   
        Debug.Log("Raycast from " + sourcePosition + " to " + targetDirection);
        if(Physics.Raycast(sourcePosition, targetDirection, out hit, Mathf.Infinity))
        {
            Debug.Log("Object name and position");
            Debug.Log(hit.transform.gameObject.name + " at " + hit.point + ", with ray magnitude (distance) = " + hit.distance);
            Debug.Log(hit.transform.gameObject.tag);
            Debug.Log(hit.transform.gameObject.layer);

        }
    }

    void RaycastAll_fn() 
    {
        Debug.Log("RaycastAll from " + sourcePosition + " to " + targetDirection);
        Vector3 direction = sourcePosition - targetDirection;
        hits = Physics.RaycastAll(sourcePosition, direction, Mathf.Infinity);  
        Debug.Log("Number of objects hit = "+ hits.Length);  
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log("Object name and position");
            Debug.Log(hit.transform.gameObject.name + " at " + hit.point + ", with ray magnitude (distance) = " + hit.distance);
            Debug.Log(hit.transform.gameObject.tag);
            Debug.Log(hit.transform.gameObject.layer);
        }
    }

    void LineCast_fn()
    {
        Debug.Log("Linecast from " + sourcePosition + " to " + camera.transform.TransformDirection(Vector3.down));
        Physics.Linecast(sourcePosition, camera.transform.TransformDirection(Vector3.down), out hit);
        Debug.Log("Object name and position");
        Debug.Log(hit.transform.gameObject.name + " at " + hit.point + ", with ray magnitude (distance) = " + hit.distance);
        Debug.Log(hit.transform.gameObject.tag);
        Debug.Log(hit.transform.gameObject.layer);
    }

    void Layermask_fn(Vector3 position) 
    {
        Debug.Log("RaycastAll from " + sourcePosition + " to " + position);
        Vector3 direction = sourcePosition + position;

        //The traffic signal assets were assigned to Layer 6 in the project.
        //Here, I create a bit mask for that layer by bit shifting the index. This cast rays against colliders in that layer only.
        int layer6_bitMask = 1 << 6;

        // To exclude objects (colliders) in that layer from the raycast, I negate the bit mask.
        layer6_bitMask = ~layer6_bitMask;

        hits = Physics.RaycastAll(sourcePosition, direction, Mathf.Infinity, layer6_bitMask); // This excludes traffic signal objects from being hit by the ray.
        Debug.Log("Number of objects hit = "+ hits.Length);  
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log("Object name and position");
            Debug.Log(hit.transform.gameObject.name + " at " + hit.point + ", with ray magnitude (distance) = " + hit.distance);
            Debug.Log(hit.transform.gameObject.tag);
            Debug.Log(hit.transform.gameObject.layer);
        }
    }
}
