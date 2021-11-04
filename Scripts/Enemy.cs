using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private float _MoveDownSpeed = 4f;   
    private UIManager myManager;
    private Player globalPlayerRef;
    private Animator _anim;
    private AudioSource _AudioSourceRef;
    [SerializeField]
    private Laser _Projectlie;

    void Start() {

        myManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        globalPlayerRef = GameObject.Find("Player").GetComponent<Player>();
        _AudioSourceRef = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        if (_anim == null) { Debug.Log("Animator isnt set"); }
        if (_AudioSourceRef == null) { Debug.LogFormat("AudioSource isnt set"); }
        if (_Projectlie == null) { Debug.Log("Projectile Component isnt set"); }
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

        if (_Projectlie.transform.position.y <= -6.5) 
        {
            Destroy(_Projectlie.gameObject);
        }
    }

    void CalculateMovement()
    {
        transform.Translate((new Vector3(0, -1, 0)) * _MoveDownSpeed * Time.deltaTime);

        if (transform.position.y <= -6.5f)
        {
            float RandomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(RandomX, 7, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //By defalult in Unity you can onlu access ".transform" component directly
        // Other Comm]ponent such as MeshRenderer, Box Collider etc you can only access throught class comunications
        //other.transform.GetComponent<MeshRenderer>();

        
        
        if (globalPlayerRef == other.transform.GetComponent<Player>() && globalPlayerRef != null ) {

           //search  by tag example 
            if (other.tag == "Player")
            {
                _MoveDownSpeed = 0f;
                _anim.SetTrigger("OnEnemyDead");//Triggers a transition (Allow to go to the next animation state)
                _AudioSourceRef.Play();
                Destroy(GetComponent<Collider2D>());//Destroys the collision
                //destroying us
                Destroy(this.gameObject,2.5f);//waits till the enemy finishes an animation
                globalPlayerRef.DecreaseLive();
                globalPlayerRef.ChangeScore(10);
                myManager.ChangeScoreUI(globalPlayerRef.GetScore());




            }
          
        }

        //if other is a projctile 
        if (other.tag == "Laser") {

            _MoveDownSpeed = 0f;
            _anim.SetTrigger("OnEnemyDead");//Triggers a transition (Allow to go to the next animation state)
            _AudioSourceRef.Play();
            Destroy(GetComponent<Collider2D>());//Destroys the collision
            Destroy(other.gameObject);
            Destroy(this.gameObject,2.5f);//waits till the enemy finishes an animation
            //Why we are using Player 
            globalPlayerRef.ChangeScore(10);
            myManager.ChangeScoreUI(globalPlayerRef.GetScore());



        }
        //destroy projectile
        //Destroy us

    }

    IEnumerator Shoot() {
        while (true)
        {
            Debug.Log("Shooted");
            Laser _LaserSpawned = Instantiate(_Projectlie, transform.position + new Vector3(0, -0.933f, 0), Quaternion.identity);
            _LaserSpawned.SetEnemyLaser();
            yield return new WaitForSeconds(3f);
        }
        
    }
}
