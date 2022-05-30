using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject bigAmmo;
    public GameObject shield;
    public CharacterController character;
    private Rigidbody rb;
    public float force;
    public bool bigShoota;
    public bool shielded = false;
    public Timer bigShootaTimer;
    public Timer shieldTimer;
    public GameObject activeProjectile;

    public int bigShootaTime = 15;
    public int shieldedTime = 15;
    public AudioSource shootSound;
    public AudioSource bigShootSound;
    
    public float shieldCurrentTime;
    public float shootCurrentTime;

    public GameObject shieldBar;
    public GameObject shootBar;

    public int shotsFired = 0;

    public GameObject gh;

    // Start is called before the first frame update
    void Awake()
    {
       bigShoota = false;
       shield.SetActive(false);
       gh = GameObject.Find("GameHandler");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shielded)
        {
            shieldBar.SetActive(false);
            shield.SetActive(false);
            shieldTimer.Pause();
        }

        if (shielded)
        {
            shieldBar.SetActive(true);
            shieldCurrentTime = shieldTimer.CurrentTime;
            if (shieldTimer.Done())
            {
                shieldTimer.Reset();
                shielded = false;
            }
        }

        if (!bigShoota)
        {
            shootBar.SetActive(false);
            activeProjectile = projectile;
            bigShootaTimer.Pause();
        }
        
        if (bigShoota)
        {
            shootBar.SetActive(true);
            shootCurrentTime = bigShootaTimer.CurrentTime;
            activeProjectile = bigAmmo;
            
            if (bigShootaTimer.Done())
            {
                bigShootaTimer.Reset();
                bigShoota = false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (bigShoota)
            {
                bigShootSound.Play();
            }
            else
            {
                shootSound.Play();
            }
            
            GameObject newProjectile = Instantiate(activeProjectile, transform.position, transform.rotation);
            rb = newProjectile.GetComponent<Rigidbody>();
            
            rb.AddForce(-rb.transform.forward * force, ForceMode.Impulse);

            gh.SendMessage("UpdateShotsFired", 1);
        }

    }

    public void BigShootin()
    {
        bigShoota = true;
        bigShootaTimer.Start(bigShootaTime);
    }
    public void Shielded()
    {
        shield.SetActive(true);
        shielded = true;
        shieldTimer.Start(shieldedTime);
    }
}
