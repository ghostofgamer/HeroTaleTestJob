using System;
using System.Collections;
using SO;
using UnityEngine;

namespace EnemyContent
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private ParticleSystem _dieEffect;
        [SerializeField] private Animator _animator;

        private Enemy _enemy;
        private int _experience;
        private Coroutine _coroutine;
        private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.6f);
        
        private void OnEnable()
        {
            _enemyHealth.Died += Die;
        }

        private void OnDisable()
        {
            _enemyHealth.Died -= Die;
        }

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _experience = _enemyData.Experience;
        }

        private void Die()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(StartDie());
        }

        private IEnumerator StartDie()
        {
            _dieEffect.gameObject.SetActive(true);
            yield return _waitForSeconds;
            _dieEffect.gameObject.SetActive(false);
            _enemy.Spawner.StartSearch();
            _enemy.Awarding.Init(_experience);
        }
    }
}