using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPowers : MonoBehaviour
{
    private float _moveSpeed = 4f;
    private Player _mainPlayer;

    //0-triple shot
    //1-speed boost
    //2- shield
    [SerializeField]
    private int _specialPowerID;
    [SerializeField]
    private AudioClip _AudioClip;

  

    void Update()
    {
        transform.Translate((new Vector3(0, -1, 0)) * _moveSpeed * Time.deltaTime);

        if (transform.position.y <= -6.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            //checking if we hit a player 
            _mainPlayer = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_AudioClip, transform.position);//this method allows us to play sound even the object gets destroyd

            if (_mainPlayer != null)
            {
               //switch(variable to go through)
                switch (_specialPowerID) {
                    case 0:
                        _mainPlayer.setTripleShotActive();
                        break;
                    case 1:
                        _mainPlayer.SetSpeedBoostActive();
                        break; 
                    case 2:
                        _mainPlayer.SetShieldActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
               
            }
            Destroy(this.gameObject);
        }
    }
}
