using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("# Scroll info")]
    [SerializeField] private Vector2 scroolVec;

    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void FixedUpdate()
    {
        material.mainTextureOffset += scroolVec * Time.fixedDeltaTime;
    }
}
