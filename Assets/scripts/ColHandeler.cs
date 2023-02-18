using UnityEngine;
using UnityEngine.SceneManagement;

public class ColHandeler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip landingClip;
    [SerializeField] AudioClip crashingClip;
    [SerializeField] ParticleSystem landingParticles;
    [SerializeField] ParticleSystem crashingParticles;

    AudioSource audioSource;
    
    bool isTransitioning = false;
    bool isCollisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        respondToDebugKeys();
    }

    void respondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L)) 
        {
            nextLevel();
        }
        if (Input.GetKey(KeyCode.C)) 
        {
            isCollisionDisabled = !isCollisionDisabled;
        }
    }



    void OnCollisionEnter(Collision Other) 
    {
        if (isTransitioning || isCollisionDisabled) return;
        switch (Other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit a firiendly!");
                break;
            case "Finish":
                startCompletionSequence();
                break;
            default:
                startCrashSequence();
                break;
        }   
    }

    void startCompletionSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(landingClip);
        landingParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("nextLevel",nextLevelDelay);
    }

    void startCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashingClip);
        crashingParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel",nextLevelDelay);
    }

                


    void reloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }

    void nextLevel()
    {
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex)+1; 
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }


}
