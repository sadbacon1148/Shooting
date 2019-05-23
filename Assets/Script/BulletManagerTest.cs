using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implement the following class. This class manages the game objects used as bullets in a game.
/// Assume that there will be a need to spawn a lot of bullets as efficiently as possible.
/// You have 1 day to work on it.
/// </summary>
public class BulletManagerTest : MonoBehaviour
{
    //Prefab for a single bullet
    public GameObject BulletPrefab;

    //TODO - declar the neccessary member variables
    private int count; //for counting the number of bullet
    public Camera mainCamera;
    public Transform firePoint; 
    private GameObject newBullet; //for instantiate
    string update;

    

    void Start()
    {
        //TODO - system initailization

    }
    
    /// <summary>
    /// Spawn bullets
    /// </summary>
    /// <param name="count">number of bullets to spawn</param>
    /// <param name="aim">starting position and direction of the bullets</param>
    /// <param name="lifeTime">life time of bullets in seconds</param>
    /// <param name="force">how fast the bullets should travel per second</param>
    /// <param name="maxSpreadAngle">how wide the bullets can spread in degree</param>
    public void SpawnBullets(int count, Ray aim, float lifeTime, float force, float maxSpreadAngle)
    {
        //TODO - spawn the bullets according to the input parameters
        for (int i = 0; i < count; i++)
        {
            RaycastHit hit;

            Vector3 shootDirection = firePoint.transform.forward;
            Quaternion fireRotation = Quaternion.LookRotation(shootDirection);
            //shootDirection.x += Random.Range(-maxSpreadAngle, maxSpreadAngle); 
            //shootDirection.y += Random.Range(-maxSpreadAngle, maxSpreadAngle);
            Quaternion randomRotation = Random.rotation;
            fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, maxSpreadAngle)); //this will make bullet to random the rotatation in the shoot direction



            aim = new Ray(firePoint.position, fireRotation*Vector3.forward);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(newBullet!= null)
            {
                if (Physics.Raycast(aim, out hit))
                {
                    Debug.Log("Hit");

                    //Debug.DrawRay(newBullet.transform.position, newBullet.transform.forward, Color.red);
                } 
            }
            
            newBullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation); // Instatiate the bullet at firePoint
            //newBullet.transform.position = firePoint.position;
            //var rot = newBullet.transform.rotation.eulerAngles;
           // newBullet.transform.rotation = Quaternion.Euler(rot.x, transform.eulerAngles.y, rot.z);
            newBullet.transform.Translate(Vector3.forward * force * Time.deltaTime, Space.Self);  //to shoot the new bullet forward
            newBullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * force, ForceMode.Impulse); //add force to the bullet, making it faster
            
            Destroy(newBullet, lifeTime); //destroy the instantiate bullet in lifetime seconds
            update = newBullet.transform.position.ToString();


            Debug.Log("count:" + count);
        }
            
        

    }


    private void UpdateBullets()
    {
        //TODO - update the position of the bullets
        if (newBullet!= null)
        {
            Debug.Log(update); //update the position of bullets in Console

        }

    }

    private void DrawBullets()
    {
        //TODO - visualize the bullets
        BulletPrefab.SetActive(true); //initially the bullet(cube) prefab was made to be SetActive(false) in the Unity
    }

    void Update()
    {
        UpdateBullets();
        DrawBullets();

        //For testing
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnBullets(50, new Ray(Vector3.zero, Vector3.forward), 3, 100, 10);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnBullets(50, new Ray(Vector3.zero, Vector3.back), 3, 100, 10);
        }
    }
}



