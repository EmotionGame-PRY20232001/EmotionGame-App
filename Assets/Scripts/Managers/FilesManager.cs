using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using UnityEngine;

public class FilesManager : MonoBehaviour
{

    public static string SanitizeFileName(string name, string replacement = "_")
    {
        // Remove invalid characters
        string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
        string invalidReStr = $"[{invalidChars}]+";
        string sanitized = Regex.Replace(name, invalidReStr, replacement);

        // Trim spaces and dots at start/end
        sanitized = sanitized.Trim().TrimEnd('.');

        // Avoid reserved names (Windows)
        string[] reservedNames = {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };

        foreach (string reserved in reservedNames)
        {
            if (sanitized.Equals(reserved, System.StringComparison.OrdinalIgnoreCase))
            {
                sanitized = $"_{sanitized}";
                break;
            }
        }

        return sanitized;
    }

    public static string SanitizeFilePlayerName(string playerName)
    {
        playerName.Trim();
        playerName.Replace(" ", "-");
        playerName.Replace("_", "-");
        playerName = SanitizeFileName(playerName, "-");
        return playerName;
    }

    /// <summary>
    /// <b>Returns</b> [playerNameSanitized]/[playerNameSanitized][_date-hour][_name?].[extension]
    /// <br></br><b>Split _</b> [0]playerName [1]customDate [2]detName?.extension
    /// </summary>
    /// <param name="extension">whitout dot</param>
    /// <param name="detName">detailed name after player name</param>
    public static string GetDefaultFilePathName(string folder = "",
                                                string extension = "csv",
                                                string detName = "",
                                                System.DateTime date = default)
    {
        var gm = GameManager.Instance;
        string playerName = gm == null ? "Player" : gm.GetCurrentPlayer().Name;
        playerName = SanitizeFilePlayerName(playerName);

        string filePath = Application.persistentDataPath;
        if (folder != "")
        {
            filePath = Path.Combine(Application.persistentDataPath, folder);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
                Debug.Log($"Created folder: {filePath}");
            }
        }

        filePath = Path.Combine(filePath, playerName);
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            Debug.Log($"Created folder: {filePath}");
        }

        System.DateTime dateExported = date == default ? System.DateTime.Now : date;
        string customDate = "yyyyMMdd-HHmmssff";
        customDate = dateExported.ToString(customDate);

        string fileName = playerName +
                          "_" + customDate +
                          (detName == "" ? "" : "_" + detName) +
                          "." + extension;
        filePath = Path.Combine(filePath, fileName);

        return filePath;
    }

    public static Sprite LoadSpriteFromSavedJPG(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File not found at: " + filePath);
            return null;
        }

        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2); // size will be replaced by LoadImage
        if (!texture.LoadImage(fileData))
        {
            Debug.LogWarning("Failed to load image data.");
            return null;
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        texture = Utils.RotateTexture90(texture);
#endif

        // Create the sprite (pivot at center, no borders)
        Sprite sprite = Sprite.Create(
                                    texture,
                                    new Rect(0, 0, texture.width, texture.height),
                                    new Vector2(0.5f, 0.5f) // pivot at center
                                );

        return sprite;
    }

    /// <summary>
    /// This function does include Application.persistentDataPath
    /// </summary>
    /// <param name="folderName"></param>
    public static void DeleteFolderPermanently(string folderName)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, folderName);

        if (Directory.Exists(fullPath))
        {
            try
            {
                Directory.Delete(fullPath, true); // true = recursive delete
                Debug.Log("Folder and all contents deleted: " + fullPath);
            }
            catch (IOException ex)
            {
                Debug.LogWarning("IO error deleting folder: " + ex.Message);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Debug.LogWarning("Access denied deleting folder: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Folder not found: " + fullPath);
        }
    }
}