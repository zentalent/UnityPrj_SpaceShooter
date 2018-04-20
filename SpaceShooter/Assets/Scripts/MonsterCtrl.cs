using UnityEngine;
using System.Collections;

public class MonsterCtrl : MonoBehaviour {

    public enum MonsterState { idle,trace,attack,die};
    public MonsterState monsterState = MonsterState.idle;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    public float traceDist = 10.0f;
    public float attackDist = 2.0f;

    private bool isDie = false;
	// Use this for initialization
	void Start () {

        monsterTr = this.gameObject.GetComponent<Transform>();

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        //nvAgent.destination = playerTr.position;

        StartCoroutine(this.CheckMonsterState());

        StartCoroutine(this.MonsterAction());

        animator = this.gameObject.GetComponent<Animator>();
	
	}


    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if(dist <= attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else if (dist <= traceDist)
            {
                monsterState = MonsterState.trace;

            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.Stop();
                    animator.SetBool("isTrace", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();
                    animator.SetBool("isAttack", false);
                    animator.SetBool("isTrace", true);
                    break;
                case MonsterState.attack:
                    nvAgent.Stop();
                    animator.SetBool("isAttack", true);
                    break;

            }
            yield return null;
        }
    }

}
