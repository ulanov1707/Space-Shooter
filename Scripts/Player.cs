using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private only this class can access(private cant be seen and adjusted in the editor)
    //public all classes can access(can be adjusted in the editor)


    // you can see and edit private variable from the editor by using "Atribute"(Uproperty)
    [SerializeField]
    private int _speed = 10;
    [SerializeField] 
    private GameObject _ProjectileToSpawn;
    [SerializeField]
    private float _fireRate = 0.05f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _live = 3;
    private SpawnManager _spawnManagerRef;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject ShieldVisualiser;
    private int _score = 0;
    private UIManager _MyUiManager;
    [SerializeField]
    private GameObject[] DamagePlayerEffect;
    [SerializeField]
    private AudioClip _laserSoundFile;//it is a sound file 
    private AudioSource _laserAudioSource;//ref to Audio Component
    void Start()
    {
        //setting a start location to a player 
        transform.position = new Vector3(0, -3.68f, 0);

        //Referencing the Spawn Manager from the scene by name
        //GameObject.Find("Name of the object in the scene").GetComponent<This is a component which Spawn_Manager contains>();
        _spawnManagerRef = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManagerRef == null)
            Debug.Log("Spawn Manager Not found");


        //Instantiate(ParticleToSpawn, new Vector3(0, 2, 0), Quaternion.identity);

        if (_tripleShotPrefab == null)
            Debug.Log("Triple Shot Prefab isnt set");
        
        //turns off Shield Visualiser
        ShieldVisualiser.SetActive(false);

        // ref to UI Manager
        _MyUiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //Ref to Audio Component
        _laserAudioSource = GetComponent<AudioSource>();
        if (_laserAudioSource == null) { Debug.Log("Audio Source is null"); }
        else { _laserAudioSource.clip = _laserSoundFile; }//assigning sound file to a component

    }

    // Update is called once per frame
    void Update()
    {


        UpdateMovement();

        //When mouse button clicked
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)//Time.time return how many seconds passed since the game started
            Shoot();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    void UpdateMovement() {

        //Each 1 second object move to 1 meter on x axis
        // transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime/*Time.Deltatime means 1 second*/);

        //in the editor there is a predefined by engine Input values(jump,shoot etc.) and you get them by calling this function
        float HorizontalAxisValue = Input.GetAxis("Horizontal");
        float VerticalAxisValue = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(
           HorizontalAxisValue * _speed,
           VerticalAxisValue * _speed,
            0) * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 0f), 0);

        if (transform.position.x < -13)
            transform.position = new Vector3(13, transform.position.y, 0);
        else if (transform.position.x > 13)
            transform.position = new Vector3(-13, transform.position.y, 0);
    }

    void Shoot() {

        //cool down logic
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == false)
        {
            
            //spawn a projectile a little bit above the player
            Instantiate(_ProjectileToSpawn, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

        }
        else if (_isTripleShotActive == true) 
        {
            Instantiate(_tripleShotPrefab, transform.position , Quaternion.identity);
        }

        _laserAudioSource.Play();//plays a sound , and sound file is already set in Start()
    }

   public void DecreaseLive() {

        if (_isShieldActive == true)//shield behave like extra life , as soon its gets hitted by enemy , shield gets destroyed
        {
            _isShieldActive = false;//turn off the shield
            ShieldVisualiser.SetActive(false);//turns off shield visualiser
            return;//leaves the function
            

        }
        else if (_isShieldActive == false) {

            _live--;
            
            switch (_live) {
                case 2:
                     DamagePlayerEffect[0].SetActive(true);
                    break;
                case 1:
                    DamagePlayerEffect[1].SetActive(true);
                    break;
            }
          
            if (_MyUiManager != null) { _MyUiManager.UpdateLives(_live); }
 
        }
       

        if (_live <= 0)
        {
            _spawnManagerRef.SetIsPlayerAlive(false);
            Destroy(this.gameObject);
           
        }
            
    }

    public void setTripleShotActive() {
        //activates tripleshot
        _isTripleShotActive = true;
        StartCoroutine(setTripleShotINACTIVE());
    }
    private IEnumerator setTripleShotINACTIVE() {
        //after 5 secs deactivates a triple shot
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void SetSpeedBoostActive() 
    {
        //sets a new speed
        _speed *=2;
        StartCoroutine(SetSpeedBoostINACTIVE());
    }
    IEnumerator SetSpeedBoostINACTIVE() {
        yield return new WaitForSeconds(6);
        //return back an old speed
        _speed /=2;
    }   
    public void SetShieldActive() {
        _isShieldActive= true;
        ShieldVisualiser.SetActive(true);
        
        
    }

    public void ChangeScore(int scoreVal) {
        _score +=scoreVal;
    }
    public int GetScore() {
        return _score;
    }
}
