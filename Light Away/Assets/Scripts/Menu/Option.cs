using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Option : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void setOption(bool isSelected)
    {
        animator.SetBool("isSelected", isSelected);
    }
}
