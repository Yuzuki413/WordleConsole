using System;
using System.Linq;

namespace Wordle
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variable to know if the player want to play again
            string playAgain = "Y";
            // Array with valid words in the game. It has 266 different words
            string[] validWords = { "Abuse", "Admit", "Adopt", "Adult", "Agent", "Agree", "Allow", "Alter", "Anger", "Apple", "Apply", "Argue", "Arise", "Avoid", "Award", "Basis", "Beach", "Begin", "Birth", "Blame", "Block", "Blood", "Board", "Brain", "Bread", "Break", "Bring", "Brown", "Build", "Burst", "Buyer", "Carry", "Catch", "Cause", "Chain", "Chair", "Check", "Chest", "Chief", "Child", "China", "Claim", "Class", "Clean", "Clear", "Climb", "Clock", "Close", "Coach", "Coast", "Count", "Court", "Cover", "Cream", "Crime", "Cross", "Crowd", "Crown", "Cycle", "Dance", "Death", "Depth", "Doubt", "Draft", "Drama", "Dream", "Dress", "Drink", "Drive", "Earth", "Enemy", "Enjoy", "Enter", "Entry", "Error", "Event", "Exist", "Faith", "Fault", "Field", "Fight", "Final", "Floor", "Focus", "Force", "Frame", "Frank", "Front", "Fruit", "Glass", "Grant", "Grass", "Green", "Group", "Guess", "Guide", "Heart", "Henry", "Horse", "Hotel", "House", "Image", "Imply", "Index", "Input", "Issue", "Japan", "Jones", "Judge", "Knife", "Laugh", "Laura", "Layer", "Learn", "Leave", "Level", "Lewis", "Light", "Limit", "Lunch", "Major", "March", "Marry", "Match", "Metal", "Model", "Money", "Month", "Motor", "Mouth", "Music", "Night", "Noise", "North", "Novel", "Nurse", "Occur", "Offer", "Order", "Other", "Owner", "Panel", "Paper", "Party", "Peace", "Peter", "Phase", "Phone", "Piece", "Pilot", "Pitch", "Place", "Plane", "Plant", "Plate", "Point", "Pound", "Power", "Press", "Price", "Pride", "Prize", "Proof", "Prove", "Queen", "Radio", "Raise", "Range", "Ratio", "Reach", "Refer", "Relax", "Reply", "Right", "River", "Round", "Route", "Rugby", "Scale", "Scene", "Scope", "Score", "Sense", "Serve", "Shall", "Shape", "Share", "Sheep", "Sheet", "Shift", "Shirt", "Shock", "Shoot", "Sight", "Simon", "Skill", "Sleep", "Smile", "Smith", "Smoke", "Solve", "Sound", "South", "Space", "Speak", "Speed", "Spend", "Spite", "Split", "Sport", "Squad", "Staff", "Stage", "Stand", "Start", "State", "Steam", "Steel", "Stick", "Stock", "Stone", "Store", "Study", "Stuff", "Style", "Sugar", "Table", "Taste", "Teach", "Terry", "Thank", "Theme", "Thing", "Think", "Throw", "Title", "Total", "Touch", "Tower", "Track", "Trade", "Train", "Treat", "Trend", "Trial", "Trust", "Truth", "Uncle", "Union", "Unity", "Value", "Video", "Visit", "Voice", "Waste", "Watch", "Water", "While", "White", "Whole", "Woman", "World", "Worry", "Would", "Write", "Youth" };

            while (playAgain == "Y")
            {
                Console.Clear();
                // Variable with the Word to search for
                Random rnd = new Random();
                int word = rnd.Next(validWords.Length - 1);
                string gameWord = validWords[word].ToUpper();
                //string gameWord = "PRICE"; // test line
                // Variable to know the number of tries
                int tryNumber = 0;
                // Variable to storage the player words
                string playerWord = "";
                // Show game rules
                showRules();
                // Convert the array strings to upper
                string[] validWordsUpper = Array.ConvertAll<string, string>(validWords, delegate (string s) { return s.ToUpper(); });
                // Play the game
                playAgain = playGame(tryNumber, playerWord, gameWord, validWordsUpper);
            }
        }

        /// <summary>
        /// Validate the words the player wrotes and call the method to compare that word with the game word
        /// </summary>
        /// <param name="tryNumber">int. Is the attempt number</param>
        /// <param name="playerWord">string. Is the word the player wrote</param>
        /// <param name="gameWord">string. Is the word the game select from the array of words for the game</param>
        /// <param name="validWords">string[]. Is the array with the valid words for the game in capital letters</param>
        /// <returns>sring. playAgain. It is the player's response indicating if he/she wants to play again.</returns>
        public static string playGame(int tryNumber, string playerWord, string gameWord, string[] validWords)
        {
            // Variable to know if the player wants to play again
            string playAgain = "Y";
            // Variable to show a message indicating if the player wins or not
            string winOrLose = "";
            // Show a message indicating to the player that the games start
            Console.WriteLine("Write 5 letter words:");
            // Validating the number of tries
            while (tryNumber < 6)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Try number {tryNumber + 1}: ");
                playerWord = Console.ReadLine().ToUpper();

                // Validate the player word length to be 5
                if (playerWord.Length != 5)
                {
                    Console.Write("               ----- Invalid word. The length word is not 5.-----");
                    Console.WriteLine();
                }
                // Validate if the word exists in the array of words for the game
                else if (!validWords.Contains(playerWord))
                {
                    Console.Write($"               -----Invalid word. The word '{playerWord}' is not valid for the game.-----");
                    Console.WriteLine();
                }
                // If word length = 5 and word exists in the array of words for the game
                else
                {
                    // Assing the result of the compareWords method to a variable
                    var result = compareWords(playerWord, gameWord, tryNumber);
                    // Assign the result of the variable to their respective ones
                    tryNumber = result.tryNumber;
                    winOrLose = result.message;
                }
            }
            // When game ends
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            // Show message indicating that the player wins or not
            Console.WriteLine(winOrLose);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Show message indicating the game has finished
            Console.WriteLine("Game Finished!");
            // Show the winner word
            Console.WriteLine($"The word was: {gameWord}");
            Console.WriteLine();
            // Ask to the player if he/she wants to play again
            Console.Write("Do you want to play again? (Y/N)");
            playAgain = Console.ReadLine().ToUpper();
            return playAgain;
        }

        /// <summary>
        /// Compare the player word and the game word and change the color of each letter accordingly
        /// </summary>
        /// <param name="playerWord">string. Is the word the player wrote</param>
        /// <param name="gameWord">string. Is the word the game select from the array of words for the game</param>
        /// <param name="tryNumber">int. Is the attempt number</param>
        /// <returns>tryNumber. Is the new attempt number. message. Is the win or lose message.</returns>
        public static (int tryNumber, string message) compareWords(string playerWord, string gameWord, int tryNumber)
        {
            int tryN = tryNumber;
            // Message to show when game finish
            string message = "";
            // Array to save the position color of the letters for the player word
            string[] playerWordLetterPosition = { "Gray", "Gray", "Gray", "Gray", "Gray" };
            // Compare if the player world is the same that the game world
            if (playerWord.Equals(gameWord))
            {
                // Change color of player word to Green
                for (int i = 0; i < playerWord.Length; i++)
                {
                    playerWordLetterPosition[i] = "Green";
                }
                // The value now is 6 because I want to finish the while cicle in the playGame method
                tryN = 6;
                message = "Congratulations, you won!";
            }
            else
            {
                // Compare each letter of the player word and game word in the same position to identify if they are in the right position
                for (int i = 0; i < gameWord.Length; i++)
                {
                    if ((playerWord[i]).Equals(gameWord[i]))
                    {
                        playerWordLetterPosition[i] = "Green";
                    }
                }
                // I scroll through each letter of the game word
                for (int i = 0; i < gameWord.Length; i++)
                {
                    // En caso de que la letra actual de la palabra del juego no se encuentre marcada como en la posicion correcta (verde)
                    // o en alguna otra posicion (amarillo), procedo a compararla contra la cadena que escribió el jugador
                    // In case the current letter of the game word is no marked as being in the correct position (green)
                    // or in some other position (yellow), I proceed to compare it against the string that the player wrote
                    if (playerWordLetterPosition[i].Equals("Gray"))
                    {
                        // Recorro la cadena del jugador letra por letra
                        // I scroll through the player word letter by letter
                        for (int j = 0; j < playerWord.Length; j++)
                        {
                            // Comparo que la primera letra de la palabra del jugador no se encuentre marcada, es decir, que estè en color gris
                            // en caso de que sea comparable (color gris), pregunto si la letra actual de la palabra del juego (i) y la letra actual
                            // de la palabra del jugador (j) son iguales
                            // en ese caso la marco como amarilla, porque existe pero no está en el lugar correcto
                            // I compare that the first letter of the player word is not marked, i.e. that it is in gray color
                            // in case it is comparable (gray color), I ask if the current letter of the game word (i) and the current letter
                            // of the player word (j) are the same
                            // in that case I mark it as yellow, because it exists but it is not in the right place
                            if (playerWordLetterPosition[j].Equals("Gray") && playerWord[j].Equals(gameWord[i]))
                            {
                                playerWordLetterPosition[j] = "Yellow";
                                break;
                            }
                        }
                    }
                }
                tryN++;
                // If the number of tries are 6, the game is end
                if (tryN == 6)
                {
                    message = "Sorry, you lose.";
                }
            }
            // Print the player word with their corresponding colors
            printPlayerWord(playerWord, playerWordLetterPosition);
            // return result
            return (tryN, message);
        }

        /// <summary>
        /// print the player word with the corresponding color
        /// </summary>
        /// <param name="playerWord">string. Is the word the player wrote</param>
        /// <param name="playerWordLetterPosition">string[]. Is the array of colors correspondign to the player word</param>
        public static void printPlayerWord(string playerWord, string[] playerWordLetterPosition)
        {
            for (int i = 0; i < playerWord.Length; i++)
            {
                // Chance the color to print the player word
                switch (playerWordLetterPosition[i])
                {
                    case "Gray":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case "Green":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                }
                Console.Write(playerWord[i]);
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Shows the menu of the game
        /// </summary>
        public static void showRules()
        {
            Console.WriteLine("***********************************************************************************************");
            Console.WriteLine("****************************************  How To Play  ****************************************");
            Console.WriteLine("Each guess must be a valid 5-letter word.");
            Console.WriteLine("The color of the tiles will change to show how close your guess was to the word.");
            Console.WriteLine("Examples");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("W");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("EARY");
            Console.WriteLine("W is in the word and in the correct spot.");
            Console.WriteLine();
            Console.Write("P");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("I");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("LLS");
            Console.WriteLine("I is in the word but in the wrong spot.");
            Console.WriteLine();
            Console.Write("VAG");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("U");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("E");
            Console.WriteLine("U is not in the word in any spot.");
            Console.WriteLine();
            Console.WriteLine("***********************************************************************************************");
        }
    }
}
