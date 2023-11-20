using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;
    [SerializeField] private float distance;

    [SerializeField] private Player player;
    #region Components
    private Camera cam;
    #endregion

    private void Start()
    {
        cam = Camera.main;
    
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    private void Update()
    {
        if (!player.PlayerMoving) return;

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(cam.transform.position.x, 0), parallaxEffect * Time.deltaTime);

        if (cam.transform.position.x - transform.position.x > distance)
        {
            transform.position = new Vector2(cam.transform.position.x + distance, 0);
        }
    }
}
