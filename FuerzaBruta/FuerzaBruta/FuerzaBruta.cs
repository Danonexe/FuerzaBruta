namespace FuerzaBruta;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class FuerzaBruta
{
    public static string encontrarPorFuerzaBruta(string hashedInput, string filePath)
    {
        // Leer el archivo línea por línea
        foreach (string line in File.ReadLines(filePath))
        {
            // Hashear la línea actual
            string lineHash = ComputeHash(line);

            // Comparar el hash de la línea con el hash proporcionado
            if (lineHash == hashedInput)
            {
                // Si coinciden, devolver la línea sin hashear
                return line;
            }
        }

        // Si no se encuentra ninguna coincidencia, devolver null
        return null;
    }

    private static string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Convertir los bytes del hash en una cadena hexadecimal
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}