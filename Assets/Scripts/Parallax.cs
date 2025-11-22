using UnityEngine;

public class Parallax : MonoBehaviour
{
    Camera cam;
    public float parallaxValue;

    Vector3 startPos;

    private void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 relativePos = cam.transform.position * parallaxValue;
        relativePos.z = startPos.z;
        transform.position = startPos + relativePos;
    }
}
