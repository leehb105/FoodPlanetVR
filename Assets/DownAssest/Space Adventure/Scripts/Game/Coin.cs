using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animation anim;
    private AudioSource audioSource;
    private bool taken;

    void Start()
    {
        anim = this.GetComponent<Animation>();
        audioSource = this.GetComponent<AudioSource>();
        taken = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Checks if coin has been taken. (Because player can re-enter coin while coin destroy animation is playing)
        if(!taken)
        {
            // Checks if coin collides with one of the player colliders.
            if(col.name == "Front" || col.name == "Back")
            {
                // Increases player wallet amount.
                Wallet.SetAmount(Wallet.GetAmount() + 1);
                // Play coin destroy animation.
                anim.Play("Coin-Destroy-Down");
                // Play coin collect sound.
                audioSource.Play();
                // Sets coin as taken.
                taken = true;
            }
        }
    }
}
