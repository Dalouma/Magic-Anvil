using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindstoneHandler : MonoBehaviour
{
    private SharpeningV2 sharpeningManager;
    private ParticleSystem sparksParticles;

    // Start is called before the first frame update
    void Start()
    {
        sharpeningManager = GameObject.FindGameObjectWithTag("Sharpening Manager").GetComponent<SharpeningV2>();
        sparksParticles = GameObject.FindGameObjectWithTag("sparks").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sparksParticles.Play();
        if (AudioManager.instance != null)
            {
                    AudioManager.instance.PlaySFX("SwordGrind");
            }
        sharpeningManager.SetSharpening(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sparksParticles.Stop();
        sharpeningManager.SetSharpening(false);
        sharpeningManager.ReleaseSharpening();
    }

}
