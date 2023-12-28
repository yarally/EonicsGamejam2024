using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

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
            var guid = AssetDatabase.FindAssets($"t:scene");
            foreach (var s in guid)
            {
                var path = AssetDatabase.GUIDToAssetPath(s);
                if (path.Contains("Main Menu")) continue;
                var btn = Instantiate(levelBtn, levelsBox.transform).GetComponent<Button>();
                btn.onClick.AddListener(() => LoadLevel(path));
                btn.GetComponentInChildren<TextMeshProUGUI>().text = path.Split("/").Last().Split(".")[0].FirstCharacterToUpper();
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
            var target = new Vector3(-1280 * layerDepth, _myRectTransform.localPosition.y, _myRectTransform.localPosition.z);
            if (Mathf.Abs(_myRectTransform.localPosition.x - target.x) < 0.1f) return;
            var move = Vector3.MoveTowards(_myRectTransform.localPosition, target, Time.deltaTime  * 4800);
            _myRectTransform.localPosition = move;
        }

        private static void LoadLevel(string path)
        {
            EditorSceneManager.LoadSceneInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Single));
        }
    }
}