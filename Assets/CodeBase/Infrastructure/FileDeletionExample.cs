using System.IO;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class FileDeletionExample : MonoBehaviour
    {
        private const string playerDataFileName = "PlayerSave.json";

        public void DeleteJsonFile()
        {
            DeleteJsonFile(playerDataFileName);
        }

        private void DeleteJsonFile(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"File '{fileName}' deleted.");
            }
            else
            {
                Debug.Log($"File '{fileName}' does not exist.");
            }
        }
    }
}
