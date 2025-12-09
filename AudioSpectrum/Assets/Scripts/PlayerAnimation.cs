using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animator _animator;
    public AudioSpectrum audioSpectrum;
    private bool _isDancing;
    private float _speedModifier;

    private void Start() {
        _animator = GetComponent<Animator>();
    }
    private void Update() {
        _isDancing = audioSpectrum.isMusicPlaying;
        _speedModifier = audioSpectrum.SumOfAllBands() / 2.2f;

        Debug.Log("Velocidade de animação: " + _speedModifier);

        _animator.SetBool("isDancing", _isDancing);
        _animator.SetFloat("speedModifier", _speedModifier);
    }
}