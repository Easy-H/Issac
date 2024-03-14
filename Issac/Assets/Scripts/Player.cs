using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField] float _speed;
    // Start is called before the first frame update
    void Start()
    {
        if(TryGetComponent<Rigidbody2D>(out _rb)) return;

    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        _rb.velocity = new Vector2(deltaX, deltaY).normalized * _speed;

    }
}
