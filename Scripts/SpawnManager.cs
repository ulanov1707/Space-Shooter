using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyToSpawn;
    [SerializeField]
    private GameObject _enemyContainer;
    
    [SerializeField]
    private GameObject [] _CollctiveToSpawn;

 

    private bool _isPlayerAlive = true; 

    private void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());//Its neccessary to call StartCoroutine() in order to coroutine work
        StartCoroutine(SpawnCollective());
    }


    //Here we are using Coroutine, which means you can stop and wait a function exetution 
    //Coroutines always works with yield return , and infinite loops
    IEnumerator SpawnEnemy() 
    {
       yield return null;//waits 1 frame and then execute code below 

        float RandomX = Random.Range(-9.5f, 9.5f);
        while (_isPlayerAlive) 
        {
            Debug.Log("Called");
            GameObject newEnemy = Instantiate(_enemyToSpawn, new Vector3(RandomX, 7, 0),Quaternion.identity);
            //all newly created enemies will be child of Enemy Container , just to have a clean hierarchy in the editor
            newEnemy.transform.SetParent(_enemyContainer.transform); 

            yield return new WaitForSeconds(3);//waits 3 seconds and starts again the loop , it doesnt exits 
        }

    }

    IEnumerator SpawnCollective() {
        float RandomX = Random.Range(-9.5f, 9.5f);
        while (_isPlayerAlive)
        {
            int RandomID = Random.Range(0, _CollctiveToSpawn.Length);
            Instantiate(_CollctiveToSpawn[RandomID] , new Vector3(RandomX, 7, 0), Quaternion.identity); ;
            
            yield return new WaitForSeconds(Random.Range(5f,10f));
        }
    }


   
   public void SetIsPlayerAlive(bool playerValue) {
        _isPlayerAlive = playerValue;
    }
}
