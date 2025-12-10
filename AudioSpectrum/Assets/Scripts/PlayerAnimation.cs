using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animator _animator;
    public AudioSpectrum audioSpectrum;
    private bool _isDancing;
    private float _speedModifier;
    
    [SerializeField] public UIUpdate uiUpdate;

    private void Start() {
        _animator = GetComponent<Animator>();
    }
    private void Update() {
        _isDancing = audioSpectrum.isMusicPlaying;
        _speedModifier = audioSpectrum.SumOfAllBands() / 2.2f;

        uiUpdate.UpdateDanceSpeed(_speedModifier);

        _animator.SetBool("isDancing", _isDancing);
        _animator.SetFloat("speedModifier", _speedModifier);
    }
}