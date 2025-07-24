using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    private enum EnemyState {Idle, Move, Attack, Return, Damaged, Die}
    private EnemyState m_State;

    private Animator anim;

    public float findDistance = 0f;
    private Transform player;
    public float attackDistance = 3f;
    public float moveSpeed = 5f;
    private CharacterController cc;

    private NavMeshAgent smith;

    private float currentTime = 0f;
    private float attackDelay = 2f;

    public int attackPower = 3;
    public int hp = 15;
    private int maxHp = 15;
    public Slider hpSlider;

    private Vector3 originPos;
    private Quaternion originRot;
    public float moveDistance = 20f;
    private void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        anim = transform.GetComponentInChildren<Animator>();

        originPos = transform.position;
        originRot = transform.rotation;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        smith = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle :
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }

        hpSlider.value = (float)hp / (float)maxHp;
    }

    private void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            anim.SetTrigger("IdleToMove");
            m_State = EnemyState.Move;
            Debug.Log("상태 전환 : Idle -> Move");
        }
    }
    private void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            Debug.Log("상태 전환 : Move -> Return");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            smith.isStopped = true;
            smith.ResetPath();
            
            smith.stoppingDistance = attackDistance;
            smith.SetDestination(player.position);
        }
        else
        {
            m_State = EnemyState.Attack;
            anim.SetTrigger("MoveToAttackDelay");
            currentTime = attackDelay;
            Debug.Log("상태 전환 : Move -> Attack");
        }
    }
    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= attackDelay)
            {
                currentTime = 0f;
                // player.GetComponent<FPSPlayerMove>().DamageAction(attackPower);
                anim.SetTrigger("StartAttack");
                Debug.Log("공격");
            }
        }
        else
        {
            currentTime = 0f;
            anim.SetTrigger("AttackToMove");
            m_State = EnemyState.Move;
            Debug.Log("상태 전환 : Attack -> Move");
        }
    }

    public void AttackAction()
    {
        player.GetComponent<FPSPlayerMove>().DamageAction(attackPower);
    }
    private void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            smith.SetDestination(originPos);
            smith.stoppingDistance = 0f;
        }
        else
        {
            smith.isStopped = true;
            smith.ResetPath();
            
            transform.position = originPos;
            transform.rotation = originRot;
            hp = 15;
            anim.SetTrigger("MoveToIdle");
            m_State = EnemyState.Idle;
            Debug.Log("상태 전환 : Return -> Idle");
        }
    }

    public void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
            return;
        
        hp -= hitPower;
        smith.isStopped = true;
        smith.ResetPath();
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            anim.SetTrigger("Damaged");
            Debug.Log("상태 전환 : Any State -> Damaged");
        }
        else
        {
            m_State = EnemyState.Die;
            anim.SetTrigger("Die");
            Debug.Log("상태 전환 : Any State -> Die");
            Die();
        }
    }
    private void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(1f);
        m_State = EnemyState.Move;
        Debug.Log("상태 전환 : Damaged -> Move");
    }
    private void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);
        Debug.Log("소멸");
        Destroy(gameObject);
    }
}
