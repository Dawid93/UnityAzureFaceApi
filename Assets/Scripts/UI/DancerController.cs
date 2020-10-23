using UnityEngine;

namespace FacialExpression.UI
{
   public class DancerController : MonoBehaviour
   {
         [SerializeField] private Animator animator;
         
         private static readonly int Dance = Animator.StringToHash("Dance");

         public void StartDance()
         {
             animator.SetBool(Dance, true);
         }
         
         public void StopDance()
         {
             animator.SetBool(Dance, false);
         }
   }
}
