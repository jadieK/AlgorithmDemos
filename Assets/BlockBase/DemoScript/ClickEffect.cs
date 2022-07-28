using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BaseMgr.Instance().BlockSelected = block =>
        {
            block.MarkSelected();
        };
    }
}
