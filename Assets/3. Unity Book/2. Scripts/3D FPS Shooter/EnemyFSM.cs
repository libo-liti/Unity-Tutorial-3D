using System.Collections;
using UnityEngine;
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

    private float currentTime = 0f;
    private float attackDelay = 2f;

    public int attackPower = 3;
    public int hp = 15;
    private int maxHp = 15;
    public Slider hpSlider;

    private Vector3 originPos;
    public float moveDistance = 20f;
    private void Start()
    {
        m_State = EnemyState.Idle;
        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        anim = transform.GetComponentInChildren<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(Time.deltaTime * moveSpeed * dir);

            transform.forward = dir;
        }
        else
        {
            m_State = EnemyState.Attack;
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
                player.GetComponent<FPSPlayerMove>().DamageAction(attackPower);
                Debug.Log("공격");
            }
        }
        else
        {
            currentTime = 0f;
            m_State = EnemyState.Move;
            Debug.Log("상태 전환 : Attack -> Move");
        }
    }
    private void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(Time.deltaTime * moveSpeed * dir);
            transform.forward = dir;
        }
        else
        {
            transform.position = originPos;
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
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            Debug.Log("상태 전환 : Any State -> Damaged");
        }
        else
        {
            m_State = EnemyState.Die;
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
        yield return new WaitForSeconds(0.5f);
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
