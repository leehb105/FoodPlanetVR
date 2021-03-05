using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public bool openDoors = true;

    public LoadBar loadBar;

    private Animation anim;

    void Start()
    {
      anim = this.GetComponent<Animation>();

      // When scene starts check if doors has to be opened and play door open animation.
      if(openDoors)
      {
        anim.Play("OpenDoors");
      }
    }

    // Used to load next level.
    public void LoadLevel(int sceneIndex)
    {
      // Play close door animation.
      anim.Play("CloseDoors");
      // Load scene async.
      StartCoroutine(LoadLevelAsync(sceneIndex));
    }

    IEnumerator LoadLevelAsync(int sceneIndex)
    {
      // Delay for door close animation.
      yield return new WaitForSeconds(0.5f);

      // Loading scene async and getting loading progress.
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

      // While loading isn't done.
      while(!operation.isDone)
      {
        // Get loading progress.
        float progress = Mathf.Clamp01(operation.progress / 0.9f);
        // Load progress to the loadbar.
        loadBar.progress = 1 - progress;
        yield return null;
        // Save loadbar rotation for the next scene.
        loadBar.saveRotation();
      }
    }
}
