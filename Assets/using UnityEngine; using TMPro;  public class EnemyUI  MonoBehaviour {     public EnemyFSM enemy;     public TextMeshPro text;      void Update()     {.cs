using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public Transform player;          // игрок
    public float patrolSpeed = 2f;    // скорость патруля
    public float chaseSpeed = 4f;     // скорость погони
    public float attackDistance = 1.5f;   // расстояние атаки
    public float detectDistance = 6f;     // радиус "заметки"

    private enum State { Patrol, Chase, Attack }
    private State currentState = State.Patrol;

    private Vector3 patrolPoint;

    void Start()
    {
        patrolPoint = GetRandomPoint();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                // если игрок вошёл в радиус заметки → начинаем погоню
                if (dist < detectDistance)
                {
                    currentState = State.Chase;
                    Debug.Log("Игрок замечен! Начинаю погоню!");
                }
                break;

            case State.Chase:
                Chase();
                if (dist < attackDistance)
                {
                    currentState = State.Attack;
                    Debug.Log("Достиг игрока! Атакую!");
                }
                else if (dist > detectDistance)
                {
                    currentState = State.Patrol;
                    Debug.Log("Игрок скрылся. Возвращаюсь к патрулю.");
                }
                break;

            case State.Attack:
                Attack();
                if (dist > attackDistance)
                {
                    currentState = State.Chase;
                    Debug.Log("Игрок убежал! Снова догоняю!");
                }
                break;
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoint, patrolSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, patrolPoint) < 0.5f)
            patrolPoint = GetRandomPoint();
    }

    void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    void Attack()
    {
        Debug.Log("Атака игрока!");
        // здесь потом можно добавить урон
    }

    Vector3 GetRandomPoint()
    {
        return new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }
}
