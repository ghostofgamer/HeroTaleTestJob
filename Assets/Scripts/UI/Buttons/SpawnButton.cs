using UnityEngine;

namespace UI.Buttons
{
    public class SpawnButton : AbstractionButton
    {
        [SerializeField] private Spawner _spawner;

        protected override void OnClick()
        {
            _spawner.StartSearch();
            gameObject.SetActive(false);
        }
    }
}