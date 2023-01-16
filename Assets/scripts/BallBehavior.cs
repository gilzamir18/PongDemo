using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;


 public static class Vector2Extension {
     
     public static Vector2 Rotate(this Vector2 v, float degrees) {
         float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
         float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);
         return v;
     }
 }

public class BallBehavior : MonoBehaviour, IAgentResetListener
{

    public BasicAgent agent;
    public float force = 100f;
    public GameObject leftPlayer;
    public GameObject rightPlayer;
    private Vector2 direction;
    private Rigidbody2D rBody;

    private bool started = false;
    private Vector2 initialPosition = Vector2.zero;

    // Start is called before the first frame update
    void Awake()
    {
        agent.AddResetListener(this);
        rBody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!agent.Done)
        {
            if (!started)
            {
                rBody.AddForce(force * direction, ForceMode2D.Impulse);
                started = true;
            }
        }
        else if (started)
        {
                rBody.velocity = Vector2.zero;
                rBody.angularVelocity = 0.0f;
                started = false;
                gameObject.transform.position = initialPosition;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 center = col.gameObject.transform.position;
        float y = 0.0f;
        for (int i = 0; i < col.contactCount; i++)
        {
            y += col.GetContact(i).point.y;
        }
        y = y/col.contactCount;

        var delta = center.y - y;

        if (col.gameObject == leftPlayer)
        {
            rBody.velocity = Vector2.zero;
            rBody.angularVelocity = 0.0f;
            var dir = new Vector2(1, 0).Rotate(delta*10).normalized;
            rBody.AddForce(dir * force, ForceMode2D.Impulse);
        }
        else if (col.gameObject == rightPlayer)
        {
            rBody.velocity = Vector2.zero;
            rBody.angularVelocity = 0.0f;
            var dir = new Vector2(-1, 0).Rotate(delta*10).normalized;
            rBody.AddForce(dir * force, ForceMode2D.Impulse);
        }
    }

    public void OnReset(Agent agent)
    {
        if (Random.Range(0, 2) > 0)
        {
            direction = new Vector2(1, 0);
        }
        else
        {
            direction = new Vector2(-1, 0);
        }
        rBody.velocity = Vector2.zero;
        rBody.angularVelocity = 0.0f;
        transform.position = Vector2.zero;
        started = false;
    }
}
