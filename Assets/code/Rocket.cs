using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField]float thrustspeed = 1000f;
    [SerializeField]float rotationspeed = 100f;
    [SerializeField] AudioClip deathsfx;
    [SerializeField] AudioClip winsfx;
    [SerializeField] AudioSource thrust;
    [SerializeField] AudioSource sfx;

    Rigidbody rigidbody;


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
                Startdeathsequence();
                break;
        }
    }

    private void Startdeathsequence()
    {
        state = State.dying;
        sfx.PlayOneShot(deathsfx);
        Invoke("Loadfirstscene", 1f);
    }

    private void Startsuccessequence()
    {
        state = State.levelcomplete;
        sfx.PlayOneShot(winsfx);
        Invoke("Loadnextscene", 1f);
    }

    private void Loadnextscene()
    {
        SceneManager.LoadScene(1);
    }
    private void Loadfirstscene()
    {
        SceneManager.LoadScene(0);
    }
}
