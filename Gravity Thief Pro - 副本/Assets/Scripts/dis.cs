using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dis : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        gameObject.SetActive(false);
#endif
    }


}
