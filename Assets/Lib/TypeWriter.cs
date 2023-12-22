using System.Collections;
using TMPro;
using UnityEngine;

namespace Lib
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TypeWriter : MonoBehaviour
    {
        private TextMeshProUGUI _tmp;
        private string _txt;

        private void Awake()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        public void Show(string text)
        {
            _txt = text;
            _tmp.text = "";
            StopAllCoroutines();
            StartCoroutine(ShowText());
        }

        private IEnumerator ShowText()
        {
            _tmp.text = "";
            _tmp.maxVisibleCharacters = int.MaxValue;
            foreach (var c in _txt)
            {
                if (_tmp.text.Length == 0)
                {
                    _tmp.text = '|'.ToString();
                    yield return new WaitForSeconds(0.1f);
                }

                _tmp.text = _tmp.text[..^1] + c + '|';
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.3f);
            _tmp.maxVisibleCharacters = _tmp.text.Length - 1;
            yield return new WaitForSeconds(3f);
            _tmp.text = "";
        }
    }
}