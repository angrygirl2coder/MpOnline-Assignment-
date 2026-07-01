using System;
using System.Collections.Generic;

namespace PlaylistManager
{
    class Program
    {
        static List<string> playlist = new List<string>();
        const int MaxSongs = 4;

        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("\n===== PLAYLIST MENU =====");
                Console.WriteLine("1. Add a song");
                Console.WriteLine("2. Remove a song");
                Console.WriteLine("3. Display playlist (index + title)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddSong();
                        break;
                    case 2:
                        RemoveSong();
                        break;
                    case 3:
                        DisplayPlaylist();
                        break;
                    case 4:
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (choice != 4);
        }

        static void AddSong()
        {
            if (playlist.Count >= MaxSongs)
            {
                Console.WriteLine($"Cannot add more songs. Playlist is full (max {MaxSongs} songs).");
                return;
            }

            Console.Write("Enter song title: ");
            string title = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Song title cannot be empty.");
                return;
            }

            playlist.Add(title);
            Console.WriteLine($"\"{title}\" added to playlist. ({playlist.Count}/{MaxSongs} songs)");
        }

        static void RemoveSong()
        {
            if (playlist.Count == 0)
            {
                Console.WriteLine("Playlist is empty. Nothing to remove.");
                return;
            }

            Console.WriteLine("\nCurrent playlist:");
            for (int i = 0; i < playlist.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {playlist[i]}");
            }

            Console.Write("Enter the index of the song to remove (or 0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index > playlist.Count)
            {
                Console.WriteLine("Invalid index. Operation cancelled.");
                return;
            }

            if (index == 0) return;

            string removed = playlist[index - 1];
            playlist.RemoveAt(index - 1);
            Console.WriteLine($"\"{removed}\" removed from playlist.");
        }

        static void DisplayPlaylist()
        {
            if (playlist.Count == 0)
            {
                Console.WriteLine("Playlist is empty.");
                return;
            }

            Console.WriteLine("\n===== YOUR PLAYLIST =====");
            for (int i = 0; i < playlist.Count; i++)
            {
                Console.WriteLine($"Index {i + 1} : {playlist[i]}");
            }
            Console.WriteLine($"Total songs: {playlist.Count}/{MaxSongs}");
        }
    }
}
