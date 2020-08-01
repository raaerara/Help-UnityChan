using UnityEngine;
using UnityEngine.Playables;

public class PlayableController : MonoBehaviour
{
    public PlayableDirector playableDirector;

    void Start()
    {
        playableDirector = this.GetComponent<PlayableDirector>();
    }

    public void PlayTimeline()
    {
        playableDirector.Play();
    }

    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    public void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    public void StopTimeline()
    {
        playableDirector.Stop();
    }

}