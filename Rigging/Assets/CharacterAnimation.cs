using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private bool macarena = false;
    private bool hipHop = false;
    private bool capoeira = false;

    private bool idle;
    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        idle = (!macarena && !hipHop && !capoeira);

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        if (Input.GetKeyDown(KeyCode.C)) {
            capoeira = !capoeira;
            macarena = false;
            hipHop = false;
        } 

        if (Input.GetKeyDown(KeyCode.H)) {
            hipHop = !hipHop;
            macarena = false;
            capoeira = false;
        } 

        if (Input.GetKeyDown(KeyCode.M)) {
            macarena = !macarena;
            capoeira = false;
            hipHop = false;
        }

        animator.SetBool("isIdling", idle);
        animator.SetBool("dancingHipHop", hipHop);
        animator.SetBool("dancingMacarena", macarena);
        animator.SetBool("dancingCapoeira", capoeira);
    }
}