using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ai4u;

public class PongController : Controller
{
       public string actuatorName = "pong";


        private float reward_sum = 0;

        override public string GetAction()
        {
            float actionValue = 0;
            string actionName = actuatorName;

            if (Input.GetKey(KeyCode.W))
            {
                actionValue = 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                actionValue = 2;
            }

            if (Input.GetKey(KeyCode.R))
            {
                actionName = "__restart__";
            }

            if (actionName != "__restart__")
            {
                //Debug.Log(ai4u.Utils.ParseAction(actionName, actionValue));
                return ai4u.Utils.ParseAction(actionName, new float[]{actionValue});
            } else
            {
                return ai4u.Utils.ParseAction("__restart__");
            }
        }

        override public void NewStateEvent()
        {
            int n = GetStateSize();
            for (int i = 0; i < n; i++)
            {
                if (GetStateName(i) == "reward" || GetStateName(i) == "score")
                {
                    float r = GetStateAsFloat(i);
                    reward_sum += r;
                }
                if (GetStateName(i) == "done" && GetStateAsFloat() > 0)
                {
                    Debug.Log("Reward Episode: " + reward_sum);
                    reward_sum = 0;
                }
            }
        }
}
