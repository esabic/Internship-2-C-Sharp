using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Dynamic;

namespace LibraryManagementSystem
{
    internal class Program
    {
        
        static void Main()
        {
            
            ShowMainMenu();
        }

 
        static void ShowMainMenu()
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("LIBRARY LOAN MANAGEMENT APPLICATION\n\n");
                Console.WriteLine("1 - Library members\n" +
                                  "2 - Loans\n" +
                                  "3 - Statistics\n" +
                                  "0 - EXIT\n\n");

                Console.Write("Option:");

                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            ShowMembersMenu();
                            break;

                        case '2':
                            Console.WriteLine();
                            ShowLoansMenu();
                            break;

                        case '3':
                            Console.WriteLine();
                            ShowStatistics();
                            break;

                        case '0':
                            Console.WriteLine();
                            Console.WriteLine("Closing application...........");
                            return;
                            

                        default:
                            Console.WriteLine();
                            Console.WriteLine("INVALID OPTION! You need to choose between (0-3).\n\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("INVALID INPUT! You need to enter a single character (0-3).\n\n");
                }
               
            }
        }

        static void ShowMembersMenu()
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Add a new member\n" +
                                  "2 - Delete a member\n" +
                                  "3 - Edit a member\n" +
                                  "4 - View all members\n" +
                                  "0 - Return to the main menu\n\n");

                Console.Write("Option:");

                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            AddMember();
                            break;

                        case '2':
                            Console.WriteLine();
                            DeleteMember();
                            break;

                        case '3':
                            Console.WriteLine();
                            EditMember();
                            break;

                        case '4':
                            Console.WriteLine();
                            PrintAllMembers();
                            break;

                        case '0':
                            Console.WriteLine();
                            Console.WriteLine("Returning to the main menu...........\n");
                            return;
                            

                        default:
                            Console.WriteLine();
                            Console.WriteLine("INVALID OPTION! You need to choose between (0-4).\n\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("INVALID INPUT! You need to enter a single character (0-4).\n\n");
                }

            }

        }

        static void AddMember()
        {

        }

        static void DeleteMember()
        {

        }

        static void EditMember()
        {

        }

        static void PrintAllMembers()
        {

        }
        static void ShowLoansMenu()
        { 

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Add a new loan\n" +
                                  "2 - Return a book\n" +
                                  "3 - Delete a loan\n" +
                                  "4 - Edit an existing loan\n" +
                                  "5 - View all loans\n" +
                                  "6 - Reports and analytics\n" +
                                  "0 - Return to the main menu\n\n");

                Console.Write("Option:");

                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            AddLoan();
                            break;

                        case '2':
                            Console.WriteLine();
                            ReturnBook();
                            break;

                        case '3':
                            Console.WriteLine();
                            DeleteLoan();
                            break;

                        case '4':
                            Console.WriteLine();
                            EditLoan();
                            break;

                        case '5':
                            Console.WriteLine();
                            PrintAllLoans();
                            break;

                        case '6':
                            Console.WriteLine();
                            ShowReportsAndAnalytics();
                            break;

                        case '0':
                            Console.WriteLine();
                            Console.WriteLine("Returning to the main menu...........\n");
                            return;
                          

                        default:
                            Console.WriteLine();
                            Console.WriteLine("INVALID OPTION! You need to choose between (0-6).\n\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("INVALID INPUT! You need to enter a single character (0-6).\n\n");
                }
            }

        }

        static void AddLoan()
        {

        }

        static void ReturnBook()
        {

        }

        static void DeleteLoan()
        {

        }

        static void EditLoan()
        {

        }

        static void PrintAllLoans()
        {

        }

        static void ShowReportsAndAnalytics()
        {

        }

        static void ShowStatistics()
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Member with the highest total late fees\n" +
                                  "2 - Member with the most loans\n" +
                                  "3 - Most borrowed book title\n" +
                                  "4 - Total late fees of all members\n" +
                                  "5 - Monthly loan count chart\n" +
                                  "0 - Return to the main menu\n\n");

                Console.Write("Option:");

                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            ShowMemberWithHighestLateFees();
                            break;

                        case '2':
                            Console.WriteLine();
                            ShowMemberWithMostLoans();
                            break;

                        case '3':
                            Console.WriteLine();
                            ShowMostBorrowedBook();
                            break;

                        case '4':
                            Console.WriteLine();
                            ShowTotalLateFees();
                            break;

                        case '5':
                            Console.WriteLine();
                            PrintMonthlyLoanChart();
                            break;

                        case '0':
                            Console.WriteLine();
                            Console.WriteLine("Returning to the main menu...........\n");
                            return;


                        default:
                            Console.WriteLine();
                            Console.WriteLine("INVALID OPTION! You need to choose between (0-5).\n\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("INVALID INPUT! You need to enter a single character (0-5).\n\n");
                }
            }

        
        }

        static void ShowMemberWithHighestLateFees()
        {

        }

        static void ShowMemberWithMostLoans()
        {

        }

        static void ShowMostBorrowedBook()
        {

        }

        static void ShowTotalLateFees()
        {

        }

        static void PrintMonthlyLoanChart()
        {

        }

    }
}
