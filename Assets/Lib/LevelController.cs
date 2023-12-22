using System.Collections;
using UnityEngine;

namespace Lib
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private string authorName;
        [SerializeField] private string levelName;
        [SerializeField][TextArea] private string levelDescription;
        [SerializeField] private TypeWriter displayBox;

        private void Start()
        {
            StartCoroutine(ShowText());
        }

        /**
         * Prints text to the UI canvas displaying information about the author and level. Authors can use this to
         * inform the player about special mechanics that they have implemented in their level.
         */
        private IEnumerator ShowText()
        {
            displayBox.Show("[" + levelName + "]\nBy " + authorName);
            yield return new WaitForSeconds(5f);
            displayBox.Show(levelDescription);
        }
    }
}
