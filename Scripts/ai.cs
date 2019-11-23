using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class ai : MonoBehaviour
{

    int damage_per_second;
    int speed;
    float health;
    float attack_distance;
    float distance_from_enemy;
    List<float> distance_from_enemys = new List<float>();
    GameObject attack_enemy;
    NavMeshAgent agent;
    Rigidbody rb;
    string search_for_tag; 

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

        if (this.gameObject.tag == "ai1")
        {
            damage_per_second = 5;
            speed = 1;
            health = 100;
            attack_distance = 10;

        }
        else if (this.gameObject.tag == "ai2")
        {
            damage_per_second = 20;
            speed = 20;
            health = 50;
            attack_distance = 5;
        }
        //GetComponent<BoxCollider>().size = new Vector3(attack_distance, 1f, attack_distance);

    }


    // Update is called once per frame
    void Update()
    {
      
        if (attack_enemy)
        {




        }
        else
        {
            distance_from_enemys.Clear();


            Search_for_enemy();
        }

    }
    void FixedUpdate()
    {
        float distance_from_enemy = Vector3.Distance(attack_enemy.transform.position, transform.position);
        if (attack_enemy && distance_from_enemy >= attack_distance)
        {
            Vector3 direction_moving = (attack_enemy.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction_moving * speed * Time.deltaTime);
        }
        else
        {
            attack();
        }
    }
    void Search_for_enemy()
    {
        if(this.tag == "ai2")
        {
            search_for_tag = "ai1";
        }
        else if (this.tag == "ai1")
        {
            search_for_tag = "ai2";
        }

        GameObject[] enemys = GameObject.FindGameObjectsWithTag(search_for_tag);
        foreach (GameObject enemy in enemys)
        {
            float distance_from_enemy = Vector3.Distance(enemy.transform.position, transform.position);


            distance_from_enemys.Add(distance_from_enemy);

        }
        float closest_enemy_distance = distance_from_enemys.Min();
        foreach (GameObject enemy in enemys)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) == closest_enemy_distance)
            {
                Debug.Log(this.name + enemy.name);
                attack_enemy = enemy;
            }

        }

    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attack_distance);
    }
    void attack()
    {
        if (attack_enemy.GetComponent<ai>().health > 0) { 
        attack_enemy.GetComponent<ai>().health = attack_enemy.GetComponent<ai>().health - damage_per_second;
        
        }
        else
        {
            Destroy(attack_enemy.gameObject);
        }
    }

}
