using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 20f;
    [SerializeField]
    private GameObject _ExplosionEffect;

    private SpawnManager _mySpawnManager;


    // Start is called before the first frame update
    void Start()
    {
        _mySpawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_mySpawnManager == null) { Debug.Log("NO Spawn Manager Assignet"); }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Laser") {
            Instantiate(_ExplosionEffect, transform.position, Quaternion.identity);
            _mySpawnManager.StartSpawning();
            Destroy(other.gameObject);
            Destroy(this.gameObject,0.2f);
        }
    }
}
