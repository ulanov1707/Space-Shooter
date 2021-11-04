using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public int ProjectileSpeed = 10;
    private bool _IsEnemysLaser = false;
  
    void Update()
    {
        if (_IsEnemysLaser == false)
        {
            MoveUp();
        }
        else 
        {
            MoveDown();
        }
       
    }
    void MoveUp()
    {   //transform.translate() , Moves the transform by x along the x axis, y along the y axis, and z along the z axis.
        transform.Translate(Vector3.up* ProjectileSpeed * Time.deltaTime);

        //Destroys a gameobject
        if (transform.position.y > 7f)
        {
            //check if the object has a parent and destroy
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);

            Destroy(this.gameObject);

        }
    }
    void MoveDown()
    {  //transform.translate() , Moves the transform by x along the x axis, y along the y axis, and z along the z axis.
        transform.Translate(Vector3.down * ProjectileSpeed * Time.deltaTime);

        //Destroys a gameobject
        if (transform.position.y > 7f)
        {
            //check if the object has a parent and destroy
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);

            Destroy(this.gameObject);

        }
    }

    public void SetEnemyLaser() {
        _IsEnemysLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _IsEnemysLaser == true)
        {
            other.gameObject.GetComponent<Player>().DecreaseLive();
        }
      
    }
}
