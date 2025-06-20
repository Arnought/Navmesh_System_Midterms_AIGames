using UnityEngine;
using UnityEngine.AI;
using TMPro;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class TargetPlayer : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float killDist = 1f;
    [SerializeField] float survivalTime = 30f;

    [SerializeField] Transform player;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject survivedUI;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Animator animator;

    private NavMeshAgent agent;
    private AgentLinkMover linkMover;
    private float timer;
    private bool gameEnded = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        linkMover = GetComponent<AgentLinkMover>();
        agent.speed = speed;

        gameOverUI.SetActive(false);
        survivedUI.SetActive(false);

        timer = survivalTime;
        Time.timeScale = 1f;

        linkMover.OnLinkStart += HandleLinkStart;
        linkMover.OnLinkEnd += HandleLinkEnd;
    }

    void Update()
    {
        if (gameEnded) return;
       
        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        agent.SetDestination(player.position);

        float distToPlayer = Vector3.Distance(transform.position, player.position);
        if (distToPlayer < killDist)
        {
            GameOver();
        }

        if (timer <= 0f)
        {
            Survived();
        }

        animator.SetBool("IsWalking", agent.velocity.magnitude > 0.01f);
    }

    void HandleLinkStart()
    {
        animator.SetBool("IsJumping", true);
    }

    void HandleLinkEnd()
    {
        animator.SetBool("IsJumping", false);
    }

    void GameOver()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Survived()
    {
        gameEnded = true;
        survivedUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
