using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAnimator : MonoBehaviour
{

    const float locomationAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Speed", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
    }
}