using System;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class BlackoutController : MonoBehaviour
{
   private Animator animator;
   
   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   public void ShowBlackout()
   {
      animator.SetBool("IsShown",true);
   }
   
   public void HideBlackout()
   {
      animator.SetBool("IsShown",false);
   }
}
