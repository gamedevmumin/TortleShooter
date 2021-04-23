using System.Collections;
using UnityEngine;

public class ScreeneFreezer : MonoBehaviour
{
    private bool isFrozen = false;
    private float duration;
    private bool shouldFreeze = false;

    public void Freeze(float duration)
    {
        if (shouldFreeze != true)
        {
            shouldFreeze = true;
            this.duration = duration;
        }
    }

    void Update()
    {
        if(shouldFreeze && !isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    private IEnumerator DoFreeze()
    {
        isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        isFrozen = false;
        shouldFreeze = false;
    }
}
