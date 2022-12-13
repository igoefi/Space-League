using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] string targetTag = "Player";
    [SerializeField] int rays = 8;
    [SerializeField] int distance = 33;
    [SerializeField] float angle = 40;
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;
    private NavMeshAgent Nana;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        Nana = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            if (RayToScan())
            {
                Nana.enabled = true;   // Контакт с целью
            }
            else
            {
                // Поиск цели...
            }
        }
    }

    bool GetRaycast(Vector3 dir)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 pos = transform.position + offset;
        if (Physics.Raycast(pos, dir, out hit, distance))
        {
            if (hit.transform == target)
            {
                result = true;
                Debug.DrawLine(pos, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(pos, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(pos, dir * distance, Color.red);
        }
        return result;
    }

    bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float j = 0;
        for (int rayNum = 0; rayNum < rays; rayNum++)
        {
            var sin = Mathf.Sin(j);
            var cos = Mathf.Cos(j);

            j += angle * Mathf.Deg2Rad / rays;

            Vector3 direction = transform.TransformDirection(new Vector3(sin, 0, cos));
            if (GetRaycast(direction)) a = true;

            if (sin != 0)
            {
                direction = transform.TransformDirection(new Vector3(-sin, 0, cos));
                if (GetRaycast(direction)) b = true;
            }
        }

        if (a || b) result = true;
        return result;
    }
}