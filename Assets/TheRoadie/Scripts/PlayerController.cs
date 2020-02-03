using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{

	public delegate void OnFocusChanged(Interactable newFocus);
	public OnFocusChanged onFocusChangedCallback;

	public Interactable focus;

	public LayerMask movementMask;
	public LayerMask interactionMask;

	private Player player;
	private Camera cam;

	void Start()
	{
		player = GetComponent<Player>();
		cam = Camera.main;
	}

	void Update()
	{

		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, movementMask))
			{
				this.player.StopFixing();
				player.MoveTo(hit.point);

				SetFocus(null);
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, float.MaxValue, interactionMask))
			{
				this.player.StopFixing();

				SetFocus(hit.rigidbody.GetComponent<Interactable>());
			}
		}

	}

	void SetFocus(Interactable newFocus)
	{
		if (onFocusChangedCallback != null)
			onFocusChangedCallback.Invoke(newFocus);

		if (focus != newFocus && focus != null)
		{
			focus.OnDefocused();
		}

		focus = newFocus;

		if (focus != null)
		{
			focus.OnFocused(transform);
		}
	}
}