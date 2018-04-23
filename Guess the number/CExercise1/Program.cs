using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 

namespace CExercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Variables
        //Declare variables      
            string answer;
            string username;
            int usernumber;
            int RandomNumber = 0; 
            int repeat = 10;
            int Newscore = 0;
            int average = 0;
            int totalaverage = 0;
            int attemptsLeft = repeat;
            string line;
            string highscore; 
            #endregion

            #region MainMenu 

            Console.WriteLine("Please enter your name.");
            username = Console.ReadLine();
            line1:
            Console.Clear();
            Console.WriteLine("Welcome " + username + "! Type help for the command list and game rules."); 
            line2: 
            answer = Console.ReadLine();
            answer = answer.ToLower();

            #endregion 

            #region HelpMenu 
            // validate answer 

            if (answer == "help")  
            {
                Console.Clear();
                Console.WriteLine("              COMMAND LIST              ");
                Console.WriteLine("________________________________________");
                Console.WriteLine("1. Start a new game");
                Console.WriteLine("2. View high scores");
                Console.WriteLine("3. Settings");
                Console.WriteLine("4. Exit");
                Console.WriteLine("________________________________________");
                Console.WriteLine("               GAME RULES               ");
                Console.WriteLine("Try to guess the number that the computer");
                Console.WriteLine("is thinking of bewteen 1 and 10, you can ");
                Console.WriteLine("change the amount of times you guess by ");
                Console.WriteLine("typing settings.");
                Console.WriteLine("________________________________________");
                Console.WriteLine("Enter a command");
                                 
              
                goto line2;
            }
            #endregion 

            #region GameLogic 

            else if (answer == "start a new game" | answer == "1")
            {
                //SET DEFAULT VALUES 
                attemptsLeft = repeat;
                totalaverage = 0;
                RandomNumber = 0; 
                Newscore = 0;
               
                for (int count = 0; count < repeat; count = count + 1)
                {

                // BASIC INTERFACE 
                Console.Clear(); 
                Console.WriteLine ("       READ THE COMUPTERS MIND      "); 
                Console.WriteLine ("_______________________________________");
                Console.WriteLine ("                                   ");
                Console.WriteLine ("                                   "); 
                Console.WriteLine ("User name: " + username);
                Console.WriteLine ("                                   ");
                Console.WriteLine ("                                   ");
                Console.WriteLine ("                                   ");
                Console.WriteLine ("Correct guesses: " + Newscore + " / " + repeat );
                Console.WriteLine ("The computors previous number: " + RandomNumber);
                Console.WriteLine ("Attempts left: " + attemptsLeft); 
                Console.WriteLine ("_______________________________________");
                Console.WriteLine ("Your Score: " + totalaverage + "%");
                Console.WriteLine ("Please enter your number:");
                try
                { 
                    usernumber = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                                       
                    Console.WriteLine("Invalid. Please enter a number");               
                    usernumber = Int32.Parse(Console.ReadLine());
                }
                 //Create a random number 

                 Random random = new Random();
                 int randomNumber = random.Next(2,11);
                 RandomNumber = randomNumber;
                   
                 StartGame s = new StartGame();
                 int score = s.getresult(usernumber , randomNumber);
                    
                     // ACUMEULATE SCORE 
                     if (score == 1)
                     {
                         Newscore = Newscore + 1;
                         average = score * 100 / repeat;
                         totalaverage = average + totalaverage;
                     }

                     attemptsLeft = attemptsLeft - 1;

                }
            #endregion 

                #region ReadAndWrite

                //READ AND WRITE TO TEXT FILE 
                //READ FROM FILE 
               
                    StreamReader sr = new StreamReader("D:/Exercise1.txt");
                    line = sr.ReadLine();
                    string line2 = line.Substring(0, 2);
                    int var = int.Parse(line2.Replace(" ", ""));
               

                while (line != null)
                {
                    if (Int32.Parse(line2) < totalaverage)
                    {
                        
                        string firstline = line;
                        Console.WriteLine("_______________________________________");
                        Console.WriteLine("             HIGHSCORE!");
                        Console.WriteLine("                " + totalaverage + " % "); 
                        Console.WriteLine("Your score and username will now be recorded");
                        sr.Close();

                        //WRITE TO FILE 


                        StringBuilder newFile = new StringBuilder();

                        string temp = "";
                        string[] file = File.ReadAllLines(@"D:/Exercise1.txt");

                        foreach (string lines in file)
                        {
                            if (lines.Contains(firstline))
                            {
                                temp = lines.Replace(firstline, totalaverage.ToString() + " " + username);
                                newFile.Append(temp + "\r\n");
                                continue;
                            }
                            newFile.Append(lines + "\r\n");
                        } 
                        File.WriteAllText(@"D:/Exercise1.txt", newFile.ToString());
                        Console.WriteLine("Sucess!");
                        //END WRITE 
                    }
                    else
                    {
                        Console.WriteLine("_______________________________________");
                        Console.WriteLine("       Better luck next time!");
                        Console.WriteLine("Your final score: " + totalaverage + "%");
                    }    
                        Console.WriteLine("Press enter to continue to main menu");
                        Console.ReadLine();
                        goto line1;
                }
            }
                #endregion 

            #region HighScores
            else if (answer == "view high scores" | answer == "3")
            {
                Console.Clear(); 
                StreamReader nsr = new StreamReader("D:/Exercise1.txt");
                Console.WriteLine(" %  Username");
                Console.WriteLine("___________________");
                for (int counts = 0; counts < 5; counts = counts + 1)
                  
                {
                   highscore = nsr.ReadLine(); 
                   Console.WriteLine(highscore);
                                                      
                }
                nsr.Close();
                   Console.WriteLine("___________________");
                   Console.WriteLine("Press enter to continue"); 
                   Console.ReadLine();
                   Console.Clear(); 
                   goto line1; 
            }

            #endregion 
            #region Settings

            else if (answer == "settings" | answer == "4")
            {
                Console.Clear();
                Console.WriteLine("Please enter the number of times you want to attempt the game.");
                repeat = Int32.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("You are in the main menu. Type help for the command list and game rules.");
                attemptsLeft = repeat;
                goto line1;
            }
            #endregion
            #region Defualts
            else if (answer == "exit" | answer == "5")
            {
                Environment.Exit(0);  
            }
            else
            {
                Console.WriteLine("invaild option.");
            
                goto line2;
            }
            #endregion 
            
        }
    }
}
