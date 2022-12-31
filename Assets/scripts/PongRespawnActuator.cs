using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;

public class PongRespawnActuator : Actuator
{
    // Start is called before the first frame u
    public override void OnSetup(Agent agent)
    {
        this.agent = (BasicAgent) agent;
        this.agent.beginOfEpisodeEvent += HandleBeginOfEpisode;
    }

    public void HandleBeginOfEpisode(BasicAgent a)
    {
        var pos = this.agent.gameObject.transform.position; 
        this.agent.gameObject.transform.position = new Vector2(pos.x, 0);
        this.agent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.agent.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
}
