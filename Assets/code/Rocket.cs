using UnityEngine;
using UnityEngine.SceneManagement;

    public class Rocket : MonoBehaviour {

        [SerializeField]float thrustspeed = 1000f;
    [SerializeField]float rotationspeed = 100f;
    [SerializeField] AudioClip deathsfx;
    [SerializeField] AudioClip winsfx;
    [SerializeField] AudioSource thrust;
    [SerializeField] AudioSource sfx;
    [SerializeField] ParticleSystem thrustfx;
    [SerializeField] ParticleSystem deathfx;
    [SerializeField] ParticleSystem winfx;
    [SerializeField] float levelloadtime = 2f;
    [SerializeField] bool debug = false;

    Rigidbody rigidbody;
    bool collisonchecker = true;


    enum State {alive,dying,levelcomplete }
    State state = State.alive;

    // Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (state == State.alive) 
        {
            Respondtothrustinput();
            Respondtorotateinput();
        }
        else
        {
            thrust.Stop();
            thrustfx.Stop();
        }
        if (debug)
        {
            debugkeys();
        }
    }

    private void Respondtothrustinput()
    {
        float thrustthisframe = thrustspeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            Thrust(thrustthisframe);
        }
        else
        {
            thrust.Stop();
            thrustfx.Stop();
        }
    }
    private void Respondtorotateinput()
    {
        rigidbody.freezeRotation = true;
        float rotationthisframe = rotationspeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationthisframe);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationthisframe);
        }
        rigidbody.freezeRotation = false;
    }

    private void Thrust(float thrustthisframe)
    {
        rigidbody.AddRelativeForce(Vector3.up * thrustthisframe);
        if (!thrust.isPlaying)
        {
            thrust.Play();
        }
        thrustfx.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive){return;}
        switch (collision.gameObject.tag)
        {
            case "friendly":
                break;
            case "Finish":
                Startsuccessequence();
                break;
            default:
                if (collisonchecker)
                {
                    Startdeathsequence();
                }
                break;
        }
   
    }

    private void Startdeathsequence()
    {
        state = State.dying;
        deathfx.Play();
        sfx.PlayOneShot(deathsfx);
        Invoke("Loadfirstscene", levelloadtime);
    }
    private void Startsuccessequence()
    {
        state = State.levelcomplete;
        winfx.Play();
        sfx.PlayOneShot(winsfx);
        Invoke("Loadnextscene", levelloadtime);
    }

    private void Loadnextscene()
    {
        SceneManager.LoadScene(1);
    }
    private void Loadfirstscene()
    {
        SceneManager.LoadScene(0);
    }

    private void debugkeys()
    {
        if (Input.GetKey(KeyCode.C))
        {
            Loadnextscene();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            print("collisons off");
            collisonchecker = !collisonchecker;
        }
    }
}
