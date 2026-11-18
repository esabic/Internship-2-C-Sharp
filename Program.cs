using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Dynamic;

namespace LibraryManagementSystem
{
    internal class Program
    {

        static Dictionary<int, (string firstName, string lastName, DateOnly dateOfBirth, List<int> loansOfMember)> libraryMembers
                 = new Dictionary<int, (string firstName, string lastName, DateOnly dateOfBirth, List<int> loansOFMember)>();

        static Dictionary<int, (string bookTitle, string author, DateOnly loanDate, DateOnly? returnDate, int overdueDays, decimal lateFee)> loans
                 = new Dictionary<int, (string bookTitle, string author, DateOnly loanDate, DateOnly? returnDate, int overdueDays, decimal lateFee)>();

        static void Main()
        {
            InitializeStartingData();
            ShowMainMenu();
        }

        static void InitializeStartingData()
        {
            libraryMembers.Add(1, ("Ena", "Sabic", new DateOnly(2005, 2, 6), new List<int> { 1, 4, 5 }));
            libraryMembers.Add(2, ("Lana", "Sabic", new DateOnly(2006, 5, 13), new List<int> { 3, 2 }));
            libraryMembers.Add(3, ("Zrinka", "Vrgoc", new DateOnly(2005, 7, 23), new List<int> { 6 }));

            loans.Add(1, ("Pride and Prejudice", "Jane Austen", new DateOnly(2026, 11, 1), null, 0, 0M));
            loans.Add(2, ("Death on the Nile", "Agatha Christie", new DateOnly(2026, 11, 17), null, 0, 0M));
            loans.Add(3, ("Angels and Demons", "Dan Brown", new DateOnly(2026, 10, 1), new DateOnly(2026, 10, 12), 0, 0M));
            loans.Add(4, ("The Silence of the Lambs", "Thomas Harris", new DateOnly(2026, 9, 5), new DateOnly(2026, 9, 30), 4, 2M));
            loans.Add(5, ("Alice's Adventures in Wonderland", "Lewis Carroll", new DateOnly(2026, 8, 2), new DateOnly(2026, 8, 24), 1, 0.5M));
            loans.Add(6, ("Journey to the Center of the Earth", "Jules Verne", new DateOnly(2026, 10, 31), null, 0, 0M));

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

                Console.Write("Option: ");

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

                Console.Write("Option: ");

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
            string newFirstName = default,
                   newLastName = default;

            CreateFirstNameLastName(ref newFirstName, ref newLastName);

            DateOnly newDateOfBirth = new DateOnly();

            CreateNewDateOfBirth(ref newDateOfBirth);

            libraryMembers.Add(CreateLibraryMemberId(), (newFirstName, newLastName, newDateOfBirth, new List<int> { }));


            Console.WriteLine("\nNew member has been successfully created!\n\n");

        }

        static void CreateNewDateOfBirth(ref DateOnly dateOfBirth)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Write a date of birth for new member in format(YYYY-MM-DD): ");

                var input = Console.ReadLine();

                if (DateOnly.TryParseExact(input,"yyyy-MM-dd", out dateOfBirth)&& dateOfBirth < DateOnly.FromDateTime(DateTime.Today))
                { 
                    return;
                }

                else
                {
                    Console.WriteLine("Invalid input! You need to write a date of birth of new member in the past in format(YYYY-MM-DD).");
                }

            }
        }
        static void CreateFirstNameLastName(ref string firstName, ref string lastName)
        {
            var flag = true;

            while (flag)
            {
                Console.WriteLine();
                Console.WriteLine("Write a name of the new member:");

                firstName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("Your input can not be empty. Write a name of the new memeber again.");

                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Write a last name of the new member:");

                        lastName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(lastName))
                        {
                            Console.WriteLine("Your input can not be empty. Write a last name of the new memeber again.");

                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }

                }

            }

        }

        static int CreateLibraryMemberId()
        {
            return libraryMembers.Count == 0 ? 1 : libraryMembers.Keys.Max() + 1;
        }

        static void DeleteMember()
        {

        }

        static void EditMember()
        {

        }

        static void PrintAllMembers()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - All members (alphabetically by last name)\n" +
                                  "2 - Members under 18 years old\n" +
                                  "3 - Members with unreturned books\n" +
                                  "0 - Return to the previous menu\n\n");

                Console.Write("Option: ");
                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            PrintAllMembersAlphabetically();
                            break;

                        case '2':
                            Console.WriteLine();
                            PrintMembersUnder18();
                            break;

                        case '3':
                            Console.WriteLine();
                            PrintMembersWithUnreturnedBooks();
                            break;

                        case '0':
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Returning to the previous menu...........\n");
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
        static void PrintAllMembersAlphabetically()
        {
            var listOfMembers = new List<(int id, string firstName, string lastName, DateOnly dateOfBirth, List<int> loansOfMember)>();

            foreach (var libraryMember in libraryMembers)
            {
                
                    listOfMembers.Add((libraryMember.Key,
                                       libraryMember.Value.firstName,
                                       libraryMember.Value.lastName,
                                       libraryMember.Value.dateOfBirth,
                                       libraryMember.Value.loansOfMember));
            }

            listOfMembers.Sort((member1, member2) => member1.lastName.CompareTo(member2.lastName));

            foreach (var member in listOfMembers)
            {

                    Console.WriteLine($"[{member.id}] {member.lastName}, {member.firstName} ({CalculateAge(member.dateOfBirth)}) - Number of loans:{member.loansOfMember.Count}");

            }

        }

            static void PrintMembersUnder18()
            {
                var countMember = 0;

                foreach (var member in libraryMembers)
                {
                    if (CalculateAge(member.Value.dateOfBirth) < 18)
                    {
                        Console.WriteLine($"[{member.Key}] {member.Value.lastName}, {member.Value.firstName} ({CalculateAge(member.Value.dateOfBirth)}) - Number of loans:{member.Value.loansOfMember.Count}");
                        countMember++;
                    }

                }
                if (countMember == 0)
                    Console.WriteLine("\nNobody is under 18.\n\n");

            }

            static int CalculateAge(DateOnly dateOfBirth)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                var age = today.Year - dateOfBirth.Year;

                if (dateOfBirth.Month > today.Month || dateOfBirth.Month == today.Month && dateOfBirth.Day > today.Day)
                {
                    age--;
                }

                return age;

            }

            static void PrintMembersWithUnreturnedBooks()
            {
                var noMembersWithUnreturnedBooks = true;

                foreach (var member in libraryMembers)
                {
                    var count = 0;
                    foreach (var loan in member.Value.loansOfMember)
                    {
                        if (loans[loan].returnDate == null)
                        {
                            count++;
                            noMembersWithUnreturnedBooks = false;
                        }
                    }
                    if (count > 0)
                    {
                        Console.WriteLine($"[{member.Key}] {member.Value.lastName}, {member.Value.firstName} ({CalculateAge(member.Value.dateOfBirth)}) - Number of loans:{member.Value.loansOfMember.Count}");
                    }
                }

                if (noMembersWithUnreturnedBooks)
                {
                    Console.WriteLine("\nAll members has returned books!\n\n");
                }
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

                    Console.Write("Option: ");

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

                    Console.Write("Option: ");

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
