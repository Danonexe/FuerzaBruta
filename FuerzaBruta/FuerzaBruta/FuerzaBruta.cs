namespace FuerzaBruta;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class FuerzaBruta
{
    public static string encontrarPorFuerzaBruta(string hashedInput, string filePath, int startLine)
    {
        int currentLine = 0;
    
        foreach (string line in File.ReadLines(filePath))
        {
            currentLine++;
            
            if (currentLine < startLine)
                continue;

            string lineHash = ComputeHash(line);

            if (lineHash == hashedInput)
            {
                return line;
            }
        }

        return null;
    }

    public static string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
