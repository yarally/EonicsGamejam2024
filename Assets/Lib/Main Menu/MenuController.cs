using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lib.Main_Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private int layerDepth;
        [SerializeField] private GameObject levelsBox;
        [SerializeField] private GameObject levelBtn;
        private RectTransform _myRectTransform;

        private void Awake()
        {
            _myRectTransform = GetComponent<RectTransform>();

            for (var buildIndex = 1; buildIndex < SceneManager.sceneCountInBuildSettings; buildIndex++)
            {
                var path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
                var slash = path.LastIndexOf('/');
                var scenePath = path.Substring(slash + 1);
                var dot = scenePath.LastIndexOf('.');
                var btn = Instantiate(levelBtn, levelsBox.transform).GetComponent<Button>();
                var index = buildIndex;
                btn.onClick.AddListener(() => LoadLevel(index));
                btn.GetComponentInChildren<TextMeshProUGUI>().text = scenePath[..dot].FirstCharacterToUpper();
            }
        }

        public void SetLayer(int depth)
        {
            layerDepth = depth;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && layerDepth > 0)
            {
                layerDepth = 0;
            }
            var target = new Vector3(-1680 * layerDepth, _myRectTransform.localPosition.y, _myRectTransform.localPosition.z);
            if (Mathf.Abs(_myRectTransform.localPosition.x - target.x) < 0.1f) return;
            var move = Vector3.MoveTowards(_myRectTransform.localPosition, target, Time.deltaTime  * 4800);
            _myRectTransform.localPosition = move;
        }

        private static void LoadLevel(int index)
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
}