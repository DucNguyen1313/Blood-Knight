using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] protected Camera cam;
    [SerializeField] protected Transform followTarget;
    protected Vector2 startingPosition;
    protected float startingPositionZ;
    protected Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
    protected float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    protected float clippingPlane =>
        (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    protected float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingPositionZ = transform.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startingPositionZ);
    }
}
