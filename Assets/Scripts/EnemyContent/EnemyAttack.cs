using System.Collections;
using Interfaces;
using PlayerContent;
using SO;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyContent
{
    public class EnemyAttack : MonoBehaviour, IAttackable
    {
        private const string AttackState = "Attack";

        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _stateIdle;
        [SerializeField] private GameObject _stateAttack;
        [SerializeField] private Image _imageStateAttack;
        [SerializeField] private Image _imageStateIdle;
        [SerializeField] private float _delayAttack;

        private float _delay;
        private int _damage;
        private Enemy _enemy;
        private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.4f);
        private float _elapsedTime = 0;
        private float _targetFill = 1f;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _stateIdle.SetActive(true);
            _stateAttack.SetActive(false);
        }

        public void Init()
        {
            _enemy = GetComponent<Enemy>();
            _delay = _enemyData.AttackDelay;
            _damage = _enemyData.Damage;
        }

        public void ApplyAttack()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            while (_enemy.Player.GetComponent<PlayerHealth>().CurrentHealth > 0)
            {
                yield return StartCoroutine(FillImage(_imageStateIdle, _delay));
                _stateAttack.SetActive(true);
                _stateIdle.SetActive(false);
                _animator.SetTrigger(AttackState);
                yield return _waitForSeconds;
                _enemy.Player.GetComponent<PlayerHealth>().TakeDamage(_damage);
                yield return StartCoroutine(FillImage(_imageStateAttack, _delayAttack));
                _stateAttack.SetActive(false);
                _stateIdle.SetActive(true);
            }
        }

        private IEnumerator FillImage(Image image, float delay)
        {
            _elapsedTime = 0;
            image.fillAmount = 0;

            while (_elapsedTime < delay)
            {
                _elapsedTime += Time.deltaTime;
                image.fillAmount = Mathf.Lerp(0, _targetFill, _elapsedTime / delay);
                yield return null;
            }

            image.fillAmount = _targetFill;
        }
    }
}