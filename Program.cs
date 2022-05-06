using Humanizer;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MovieProject
{
    class Movie

    {
        List<MovieDetails> movieDetails = new List<MovieDetails>();

        //Method for Choosing the Option in the Start Page.
        public void ChooseOption()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t************************************");
            Console.WriteLine("\t\t*** Welcome to MoviePlex Theatre ***");
            Console.WriteLine("\t\t************************************");
            Console.ResetColor();
            Console.WriteLine("\n\nPlease Select From The Following Options:");
            Console.WriteLine("1: Administrator");
            Console.WriteLine("2: Guests\n");
            Console.Write("Selection: ");
            String line = Console.ReadLine();
            Console.WriteLine();

            //Throwing Error or moving to the EnterPassword() method or GuestOption() method based on Selection.
            try
            {
                if (!(int.TryParse(line, out int caseSwitch)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Exception ex = new Exception("Invalid Selection! Please enter 1 or 2.");
                    throw ex;
                    Console.ResetColor();
                }
                else
                {
                    //If Entered 1 then EnterPassword(), 2 then GuestOption() else Gives an error.
                    switch (caseSwitch)
                    {
                        case 1:
                            EnterPassword();
                            break;

                        case 2:
                            GuestOption();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Selection! Please enter 1 or 2.\n");
                            Console.ResetColor();
                            ChooseOption();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                ChooseOption();
            }
        }

        //Method for Validating the Password & Keeping the Login count counter.
        int loginCount = 5;
        public void EnterPassword()
        {
            Console.WriteLine("Please Enter the Admin Password:");
            string pw = Console.ReadLine();
            Console.WriteLine();

            //Checking if the Password matches.
            if (pw == "*****")
            {
                EnterMovies();
            }
            else if(pw == "B" || pw == "b")
            {
                ChooseOption();
            }
            else
            {
                if (loginCount > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an Invalid password.");
                    
                    loginCount--;

                    Console.WriteLine($"You have {loginCount} more attempts to enter the correct password Or Press B to go back to previous screen.\n");
                    Console.ResetColor();

                    if (loginCount != 0)
                        EnterPassword();
                    else
                        ChooseOption();
                }
            }
        }

        //Method for the Guest User.
        public void GuestOption()
        {
            MovieDetails details = new MovieDetails();
            int numbering = 0;
            //If there are no movies played, generates an Message.
            if (movieDetails.Count == 0)
            {
                Console.WriteLine("Sorry! There are no movies playing today.");
                Console.WriteLine("Press S to go back to the Start Page.");
                var value = Console.ReadLine();

                if (value == "S" || value == "s")
                {
                    ChooseOption();
                }
                else
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Selection! Please enter S to go back to Start Page.");
                    Console.ResetColor();
                }
                Console.ReadKey();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\t\t************************************");
                Console.WriteLine("\t\t*** Welcome to MoviePlex Theatre ***");
                Console.WriteLine("\t\t************************************");
                Console.ResetColor();

                //Showing the number of movies Playing.
                Console.WriteLine("\n\nThere are " + movieDetails.Count + " movies playing today. Please choose from the following movies:\n");

                numbering = 0;
                foreach (var v in movieDetails)
                {
                    Console.WriteLine("\n" + "\t\t" + (numbering + 1) + ". " + v.MovieName + " [" + v.MovieAge + "]");
                    numbering++;
                }

                //Selecting the movie to watch.
                var movieNumber = "";
                do
                {
                    Console.WriteLine("\nWhich Movie Would You Like To Watch?: ");
                    movieNumber = Console.ReadLine();

                    if (!int.TryParse(movieNumber, out int num))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Selection! Please select a number from the given options.\n"); 
                        Console.ResetColor();
                    }
                    else if (string.IsNullOrEmpty(movieNumber) ||((Convert.ToInt32(movieNumber) >movieDetails.Count) || (Convert.ToInt32(movieNumber) <=0)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Selection! Please select a number from the given options.\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                var selectedMovieDetails = new MovieDetails();
                for (int i=1;i<=movieDetails.Count;i++)
                {
                    if (i==Convert.ToInt32(movieNumber))
                    {
                        selectedMovieDetails = movieDetails[i-1];
                        break;
                    }
                }

                //Validating Age for the User.
                do
                {
                    AgeVerification: Console.WriteLine("\nPlease Enter Your Age For Verification: ");
                    var number = Console.ReadLine();

                    if (string.IsNullOrEmpty(number))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter a valid age.");
                        Console.ResetColor();
                        goto AgeVerification;
                    }
                    else if (int.TryParse(number, out int num) && (Convert.ToInt32(number) < 1 || Convert.ToInt32(number) > 120))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter a valid age between 1 to 120.");
                        Console.ResetColor();
                        goto AgeVerification;
                    }

                    else if (!int.TryParse(number, out int ag) || Convert.ToInt32(number) < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPlease enter a valid age.");
                        Console.ResetColor();
                        goto AgeVerification;
                    }
                    else if (selectedMovieDetails.MovieAge == "PG" && Convert.ToInt32(number) < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEntered age should be greater than or equal to 10 for age type PG.");
                        Console.ResetColor();
                        goto AgeVerification;
                    }
                    else if (selectedMovieDetails.MovieAge == "PG-13" && Convert.ToInt32(number) < 13)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEntered age should be greater than or equal to 13 for age type PG-13.");
                        Console.ResetColor();
                        goto AgeVerification;
                    }
                    else if (selectedMovieDetails.MovieAge == "R" && Convert.ToInt32(number) < 15)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEntered age should be greater than or equal to 15 for age type R.");
                        Console.ResetColor();
                        goto AgeVerification;
                    }
                    else if (selectedMovieDetails.MovieAge == "NC-17" && Convert.ToInt32(number) < 17)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nEntered age should be greater than or equal to 17 for age type NC-17.");
                        Console.ResetColor();
                        goto AgeVerification;
                    }
                    else if (int.TryParse(selectedMovieDetails.MovieAge, out int a) && Convert.ToInt32(number) < Convert.ToInt32(selectedMovieDetails.MovieAge))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou are underage to watch this movie. Please choose a different movie.\n");
                        Console.ResetColor();
                        GuestOption();
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                Console.WriteLine("\nEnjoy The Movie!");

                Options: Console.WriteLine("\nPress M to go back to the Guest Main Menu.");
                Console.WriteLine("Press S to go back to the Start Page.");
                var value = Console.ReadLine();


                if (value == "M" || value == "m")
                {
                    Console.Clear();
                    GuestOption();
                }
                else if (value == "S" || value == "s")
                {
                    Console.Clear();
                    ChooseOption();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Selection! Please enter S to go back to Start Page OR enter M to go back to the Guest Main Menu.");
                    Console.ResetColor();
                    goto Options;
                }
                Console.ReadKey();
            }

          
        }

        //Entering the Movies for the administrator.
        public void EnterMovies()
        {
            int numbering = 0;
            Console.WriteLine("\nWelcome MoviePlex Administrator.\n");
            Console.Write("How many movies are playing Today?: ");
            string input = Console.ReadLine();
            int moviesCount;
            bool isNumber = int.TryParse(input, out moviesCount);


            if (isNumber == true)
            {
                if (moviesCount > 0 && moviesCount <= 10)
                {
                    //Displaying the Ratings for the administrator to select.
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\nG – General Audience, all ages admitted.");
                    Console.WriteLine("PG – Parental Guidance Suggested, some material may not be suitable for children.");
                    Console.WriteLine("PG-13 –  Parents Strongly Cautioned, Some materials may be inappropriate for children under 13. Parents are urged to be cautious.");
                    Console.WriteLine("R – Restricted, Under 17 requires accompanying parent or adult guardian. Contains some adult material.");
                    Console.WriteLine("NC-17 – Adults only, No one 17 and under admitted. Clearly adult Children’s are not admitted.");
                    Console.ResetColor();

                    //Validating the Movie Name.
                    for (int i = 0; i < moviesCount; i++)
                    {
                        var name = "";
                        var age = "";
                        do
                        {
                            Console.Write("\nPlease Enter The {0} Movie's Name: ", (i + 1).ToOrdinalWords());
                            name = Console.ReadLine();
                            if (name.Trim().Length == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please enter a valid movie name.");
                                Console.ResetColor();
                            }
                            else
                            {
                                break;
                            }
                            
                        }
                        while (true);

                        //Validating the Age Limit or Rating.
                        do
                        {
                            Console.Write("Please Enter Age Limit or Rating For The {0} Movie: ", (i + 1).ToOrdinalWords());
                            age = Console.ReadLine();

                            if (int.TryParse(age, out int num) && (Convert.ToInt32(age) < 0 || Convert.ToInt32(age) > 120))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nPlease enter valid Age between 0 to 120.\n");
                                Console.ResetColor();
                            }
                            else if ( (string.IsNullOrEmpty(age)) ||(!(int.TryParse(age, out int n))&&(age.Trim()!="G" && age.Trim()!= "PG" &&age.Trim()!= "PG-13" 
                                && age.Trim()!="R" && age.Trim() != "NC-17")))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nPlease Enter a Valid Age Limit or Rating.\n");
                                Console.ResetColor();
                            }
                            else
                            {
                                break;
                            }
                        }
                        while (true);

                        //Adding the Name of the movie & Rating/Age to the MovieDetails class.
                        MovieDetails details = new MovieDetails();
                        details.MovieName = name;
                        details.MovieAge = age;

                        movieDetails.Add(details);
                    }

                    //Displaying the Entered movies.
                    foreach (var v in movieDetails)
                    {
                        Console.WriteLine("\n" + "\t\t" + (numbering + 1) + ". " + v.MovieName + " [" + v.MovieAge + "]");
                        numbering++;
                    }
                    Satisfaction: Console.WriteLine("\nYour Movies Playing Today Are Listed Above. Are you satisfied? (Y/N)? ");

                    //Validating the Input for Confirmation of the Movies Playing.
                    var value = Console.ReadLine();
                    var satisfaction = value.ToUpper();
                    if (satisfaction == "Y" || satisfaction == "y")
                    {
                        Console.Clear();
                        ChooseOption();
                    }
                    else if (satisfaction == "N" || satisfaction == "n")
                    {

                        numbering = 0;
                        movieDetails.Clear();
                        EnterMovies();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please select the right input whether Y or N.");
                        Console.ResetColor();
                        goto Satisfaction;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Selection! Please select upto 10 number of movies.");
                    Console.ResetColor();
                    EnterMovies();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Selection! Please enter a number between 1 to 10.");
                Console.ResetColor();
                EnterMovies();
            }
        }

        class Program
        {
            //Calling the ChooseOption() method in the Main method.
            static void Main(string[] args)
            {
                Movie mov = new Movie();
                mov.ChooseOption();

            }
        }
    }
}
