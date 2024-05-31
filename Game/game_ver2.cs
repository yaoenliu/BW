using System;
using System.Threading;
using System.Windows;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    class Game
    {
        static void GameStart(string[] args)
        {
            string status = "";
            Console.WriteLine("Please enter 'Start' to begin the game");
            status = Console.ReadLine();
            while (status == "Start")
            {
                bool isFrameWin = false;
                Player myPlayer = new Player();
                Player Enemy = new Player();
                string hand1 = "";
                string hand2 = "";
                while (true)
                {
                    Console.WriteLine("Please enter a hand gesture: Rock/Scissors/Paper");
                    hand1 = Console.ReadLine(); // First input
                                                //Thread.Sleep(3000);
                    Console.WriteLine("Please enter the same hand gesture: Rock/Scissors/Paper");
                    hand2 = Console.ReadLine(); // Second input
                    if (hand1 == hand2) // Check if the two inputs are the same
                    {
                        break;
                    }
                }
                myPlayer.SetFrame(hand1);
                // Enemy randomly chooses a hand gesture
                int enemyFrame = new Random().Next(0, 3);
                if (enemyFrame == 0)
                {
                    Console.WriteLine("Enemy: Rock");
                    Enemy.SetFrame("Rock");
                }
                else if (enemyFrame == 1)
                {
                    Console.WriteLine("Enemy: Scissors");
                    Enemy.SetFrame("Scissors");
                }
                else
                {
                    Console.WriteLine("Enemy: Paper");
                    Enemy.SetFrame("Paper");
                }
                // Determine the winner
                if (myPlayer.GetHand() == "Rock")
                {
                    if (Enemy.GetHand() == "Rock")
                    {
                        Console.WriteLine("Draw");
                        continue;
                    }
                    else if (Enemy.GetHand() == "Scissors")
                    {
                        Console.WriteLine("Win");
                        isFrameWin = true;
                    }
                    else
                    {
                        Console.WriteLine("Lose");
                        isFrameWin = false;
                    }
                }
                else if (myPlayer.GetHand() == "Scissors")
                {
                    if (Enemy.GetHand() == "Rock")
                    {
                        Console.WriteLine("Lose");
                        isFrameWin = false;
                    }
                    else if (Enemy.GetHand() == "Scissors")
                    {
                        Console.WriteLine("Draw");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Win");
                        isFrameWin = true;
                    }
                }
                else
                {
                    if (Enemy.GetHand() == "Rock")
                    {
                        Console.WriteLine("Win");
                        isFrameWin = true;
                    }
                    else if (Enemy.GetHand() == "Scissors")
                    {
                        Console.WriteLine("Lose");
                        isFrameWin = false;
                    }
                    else
                    {
                        Console.WriteLine("Draw");
                        continue;
                    }
                }
                // Read the hand direction
                hand1 = "";
                hand2 = "";
                while (true)
                {
                    Console.WriteLine("Please enter a direction: Up/Down/Left/Right");
                    hand1 = Console.ReadLine(); // First input
                                                //Thread.Sleep(3000);
                    Console.WriteLine("Please enter the same direction: Up/Down/Left/Right");
                    hand2 = Console.ReadLine(); // Second input
                    if (hand1 == hand2) // Check if the two inputs are the same
                    {
                        break;
                    }
                }
                myPlayer.SetDirection(hand1);

                // Enemy's hand direction
                int enemyDirection = new Random().Next(0, 4);
                if (enemyDirection == 0)
                {
                    Console.WriteLine("Enemy: Up");
                    Enemy.SetDirection("Up");
                }
                else if (enemyDirection == 1)
                {
                    Console.WriteLine("Enemy: Down");
                    Enemy.SetDirection("Down");
                }
                else if (enemyDirection == 2)
                {
                    Console.WriteLine("Enemy: Left");
                    Enemy.SetDirection("Left");
                }
                else
                {
                    Console.WriteLine("Enemy: Right");
                    Enemy.SetDirection("Right");
                }
                // Check if the directions match
                if (myPlayer.GetDirection() != Enemy.GetDirection())
                {
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("Next Round Again");
                    continue;
                }

                if (isFrameWin)
                {
                    Console.WriteLine("You are Winner");
                }
                else
                {
                    Console.WriteLine("Yor are Loser");
                }
                // Ask if the user wants to play again
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Please enter 'Start' to begin the game");
                status = Console.ReadLine();
            }
        }
    }

}

