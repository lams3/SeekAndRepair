using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Interactable : MonoBehaviour
{

	public float radius = 3f;
	public Transform interactionTransform;

	bool isFocus = false;
	Transform player;

	bool hasInteracted = false;

	protected virtual void Awake()
	{
		if(interactionTransform == null)
		{
			interactionTransform = this.transform;
		}
	}

	protected virtual void Update()
	{
		if (isFocus)
		{
			float distance = Vector3.Distance(player.position, interactionTransform.position);
			if (!hasInteracted && distance <= radius)
			{
				hasInteracted = true;
				Interact();
			}
		}
	}

	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		hasInteracted = false;
		player = playerTransform;
	}

	public void OnDefocused()
	{
		isFocus = false;
		hasInteracted = false;
		player = null;
	}

	public abstract void Interact();

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}
}