//Make a basic number guessing challenge against the computer


using System;

class NumberGuessingGame
{
    private int secretNumber;
    private bool gameOver = false;
    private string winner = "";

    public NumberGuessingGame()
    {
        Random rand = new Random();
        secretNumber = rand.Next(1, 101);
    }

    public void Play()
    {
        Console.WriteLine("I have picked a number between 1 and 100.");
        Console.WriteLine("You and the computer will take turns guessing.");
        Console.WriteLine("Whoever guesses correctly first wins!\n");

        while (!gameOver)
        {

            Console.Write("Your guess: ");
            int playerGuess = int.Parse(Console.ReadLine());

            if (CheckGuess(playerGuess, "Player"))
                break;


            int compGuess = ComputerGuess(1, 100);
            Console.WriteLine($"Computer guesses: {compGuess}");

            if (CheckGuess(compGuess, "Computer"))
                break;
        }

        Console.WriteLine($"\nGame Over! The secret number was {secretNumber}.");
        Console.WriteLine($"Winner: {winner}!");
    }

    private bool CheckGuess(int guess, string playerName)
    {
        if (guess == secretNumber)
        {
            winner = playerName;
            gameOver = true;
            Console.WriteLine($"{playerName} guessed correctly!");
            return true;
        }
        else if (guess < secretNumber)
        {
            Console.WriteLine($"{playerName}'s guess is too low.");
        }
        else
        {
            Console.WriteLine($"{playerName}'s guess is too high.");
        }
        return false;
    }

    private int ComputerGuess(int low, int high)
    {
        return (low + high) / 2;

    }
}

class Program
{
    static void Main()
    {
        NumberGuessingGame game = new NumberGuessingGame();
        game.Play();
    }
}
