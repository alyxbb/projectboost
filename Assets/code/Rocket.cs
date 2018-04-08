using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField]float thrustspeed = 1000f;
    [SerializeField]float rotationspeed = 100f;

    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State {alive,dying,levelcomplete }
    State state = State.alive;

    // Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (state == State.alive) 
        {
            Thrust();
            Rotate();
        }
	}
    private void Thrust()
    {
        float thrustthisframe = thrustspeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustthisframe);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    private void Rotate()
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

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "friendly":
                break;
            case "Finish":
                state = State.levelcomplete;
                Invoke("Loadnextscene",1f);
                break;
            default:
                state = State.dying;
                Invoke("Loadfirstscene", 1f);
                break;
        }
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
