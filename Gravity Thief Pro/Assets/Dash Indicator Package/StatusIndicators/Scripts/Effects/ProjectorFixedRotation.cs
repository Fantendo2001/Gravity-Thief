using UnityEngine;
using System.Collections;
using Werewolf.StatusIndicators;
//namespace Werewolf.StatusIndicators.Effects
//namespace Werewolf.StatusIndicators.Components
//{
    
  public class ProjectorFixedRotation : MonoBehaviour {
    public float Angle;
        
    void Update()
        {
      
      transform.eulerAngles = new Vector3(0, Angle, 0);
            
    }
       
    }
//}