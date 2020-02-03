using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

	Transform target;
	NavMeshAgent agent;

	Transform raycastTarget;

	float totalFixingTime = 1.0f;
	float actualFixingTime;
	bool isFixing;
	Breakable breakable;

    void Start()
	{
		raycastTarget = this.transform.Find("RaycastTarget");
		agent = GetComponent<NavMeshAgent>();
		GetComponent<PlayerController>().onFocusChangedCallback += OnFocusChanged;
	}

	public void MoveTo(Vector3 point)
	{
		agent.SetDestination(point);
	}

	void OnFocusChanged(Interactable newFocus)
	{
		if (newFocus != null)
		{
			agent.stoppingDistance = newFocus.radius;
			agent.updateRotation = false;

			target = newFocus.interactionTransform;
		}
		else
		{
			agent.stoppingDistance = 0f;
			agent.updateRotation = true;
			target = null;
		}
	}

	void Update()
	{
		if (target != null)
		{
			MoveTo(target.position);
			FaceTarget();
		}

		if (this.isFixing)
		{
			this.actualFixingTime += Time.deltaTime;
			this.isFixing = this.actualFixingTime < this.totalFixingTime;
			if (!this.isFixing)
				this.breakable.Fix();
		}

		Manager.Instance.UpdateFixingBar(this.isFixing, this.actualFixingTime / this.totalFixingTime);
	}

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	public Vector3 GetRayCastPosition()
	{
		return this.raycastTarget.position;
	}

	public void Fix(Breakable breakable)
	{
		this.breakable = breakable;
		this.isFixing = true;
		this.actualFixingTime = 0.0f;
		this.totalFixingTime = breakable.timeToFix;
	}

	public void StopFixing()
	{
		this.breakable = null;
		this.isFixing = false;
		this.actualFixingTime = 0.0f;
		this.totalFixingTime = 1.0f;
	}
}