using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    public float speed;
    public Transform[] path;

    private float t;
    private Transform standTransform;
    private Transform lightTransform;
    private new Light light;

    private void Awake()
    {
        this.standTransform = this.transform.Find("Stand");
        this.lightTransform = this.standTransform.Find("Light");
        this.light = this.lightTransform.GetComponent<Light>();
    }

    private void Update()
    {
        if (path.Length == 0) 
            return;
        if (path.Length == 1)
        {
            this.LookAt(this.path[0].position);
            return;
        }

        this.t = Mathf.PingPong(Time.time * this.speed, this.path.Length - 1);

        Transform t0 = this.path[(int) t];
        Transform t1 = this.path.Length == 1 ? t0 : this.path[((int) t) + 1];
        
        float lerpT = t - ((int)t);
        this.LookAt(Vector3.Lerp(t0.position, t1.position, lerpT));
    }

    public bool IsInSight(Player player, LayerMask layerMask)
    {
        RaycastHit hit;
        Vector3 vector = player.GetRayCastPosition() - this.lightTransform.position;
        bool didHit = Physics.Raycast(this.lightTransform.position, vector, out hit, float.MaxValue, layerMask);
        bool isOnLight = Vector3.Angle(vector, this.lightTransform.forward) <= (this.light.spotAngle / 2.0f);
        bool isTheFirst = didHit && hit.rigidbody == player.GetComponent<Rigidbody>();

        return isOnLight && isTheFirst;
    }

    private void LookAt(Vector3 target)
    {
        this.standTransform.LookAt(new Vector3(target.x, this.standTransform.position.y, target.z));
        this.lightTransform.LookAt(target);
    }
}
