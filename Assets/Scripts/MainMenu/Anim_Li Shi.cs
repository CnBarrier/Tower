using System.Collections;
using UnityEngine;

public class Anim_LiShi : MonoBehaviour
{
    public Animator animator;
    
    void Start()
    {
        StartCoroutine(LiShi_Smoking());
    }

    IEnumerator LiShi_Smoking()
    {
        while(true)
        {
            animator.Play("Li Shi_Smoking", -1, 0f);
            yield return new WaitForSeconds(3f);
        }
    }
}
