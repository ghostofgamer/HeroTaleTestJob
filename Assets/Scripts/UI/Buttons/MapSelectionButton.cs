using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Buttons
{
    public class MapSelectionButton : AbstractionButton
    {
        [SerializeField] private int _indexMap;

        protected override void OnClick()
        {
            SceneManager.LoadScene(_indexMap);
        }
    }
}
