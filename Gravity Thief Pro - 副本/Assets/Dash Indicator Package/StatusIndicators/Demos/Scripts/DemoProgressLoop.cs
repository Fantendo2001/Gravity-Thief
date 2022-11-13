using UnityEngine;
using Werewolf.StatusIndicators.Components;

namespace Werewolf.StatusIndicators.Demo {
  public class DemoProgressLoop : MonoBehaviour {
    private Splat splat;
      
        public float speed ;
 public player player;//用于获取player 是否处于空中二段跳的状态
    void Start() 
        {
      splat = GetComponent<Splat>();
   
        }

    void FixedUpdate() {
            if ( player.IsCountering==true)
            {
                gameObject.GetComponent<Projector>().enabled= true;
                splat.Progress = 1-(float)(player.DoubleJumpCounter)/(float)(player.GetComponent<PlayerMove>().TimeBetweenJumps);
            }
            else if (player.IsCountering == false)
            {
                gameObject.GetComponent<Projector>().enabled = false;
                splat.Progress = 1;
            }

        }
  }
}