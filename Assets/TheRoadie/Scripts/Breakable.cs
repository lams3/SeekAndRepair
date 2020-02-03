using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Breakable : Interactable
{
    public bool isBroken = false;
    public float timeToFix = 1.0f;
    public GameObject visualIndicator;


    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        this.audioSource = this.GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        this.visualIndicator.SetActive(this.isBroken);
    }

    public override void Interact()
    {
        if (this.isBroken)
            Manager.Instance.player.Fix(this);
    }

    public void Fix()
    {
        if (!this.isBroken)
            return;

        this.isBroken = false;
        this.audioSource.mute = false;
    }

    public void Break()
    {
        if (this.isBroken)
            return;

        this.isBroken = true;
        this.audioSource.mute = true;
    }
}
