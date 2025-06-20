using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class AiMovement : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private Animator animator;
    private NavMeshAgent agent;
    private AgentLinkMover linkMover;

    private RaycastHit[] hits = new RaycastHit[1];
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        linkMover = GetComponent<AgentLinkMover>();

        linkMover.OnLinkStart += HandleLinkStart;
        linkMover.OnLinkEnd += HandleLinkEnd;
    }

    private void HandleLinkStart()
    {
        animator.SetBool("IsJumping", true);
    }

    private void HandleLinkEnd()
    {
        animator.SetBool("IsJumping", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.RaycastNonAlloc(ray, hits) > 0)
            {
                agent.SetDestination(hits[0].point);
            }

            
        }
        animator.SetBool("IsWalking", agent.velocity.magnitude > 0.01f);

    }
}
