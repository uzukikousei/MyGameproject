using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;

    [SerializeField] private Player player;

    const string Is_Walk = "IsWalking";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(Is_Walk, player.IsWalking());
    }
}
