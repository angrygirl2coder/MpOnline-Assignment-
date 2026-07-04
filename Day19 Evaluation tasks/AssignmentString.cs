//Password encoding/decoding based on following rules
//• Encodes it by:
//Shifting each character by +2 in the ASCII table (e.g., A → C, b → d).
//Reversing the entire string after shifting.
//•  Decodes it back to the original message by reversing the process.

using System;

class Assignment
{
    static void Main()
    {
        Console.Write("Enter a password to encode: ");
        string original = Console.ReadLine();


        string encoded = Encode(original);
        Console.WriteLine($"Encoded: {encoded}");


        string decoded = Decode(encoded);
        Console.WriteLine($"Decoded back: {decoded}");

        Console.WriteLine($"Original matches decoded? {original == decoded}");
    }


    static string Encode(string input)
    {

        char[] shifted = new char[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            shifted[i] = (char)(input[i] + 2);
        }


        Array.Reverse(shifted);
        return new string(shifted);
    }


    static string Decode(string encoded)
    {

        char[] reversed = encoded.ToCharArray();
        Array.Reverse(reversed);


        for (int i = 0; i < reversed.Length; i++)
        {
            reversed[i] = (char)(reversed[i] - 2);
        }

        return new string(reversed);
    }
}
