using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Dynamic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            libraryMembers.Add(1, ("Ena", "Sabic", new DateOnly(2005, 2, 6), new List<int> { 4, 5 }));
            libraryMembers.Add(2, ("Lana", "Sabic", new DateOnly(2006, 5, 13), new List<int> { 6, 2, 1 }));
            libraryMembers.Add(3, ("Zrinka", "Vrgoc", new DateOnly(2005, 7, 23), new List<int> { 3 }));

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

                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", out dateOfBirth) && dateOfBirth < DateOnly.FromDateTime(DateTime.Today))
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
                Console.WriteLine("Write a name of the member:");

                firstName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("Your input can not be empty. Write a name of the memeber again.");

                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Write a last name of the member:");

                        lastName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(lastName))
                        {
                            Console.WriteLine("Your input can not be empty. Write a last name of the memeber again.");

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
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Delete member by id\n" +
                                  "2 - Delete member by first and last name\n" +
                                  "0 - Return to the previous menu\n\n");

                Console.Write("Option: ");
                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            DeleteById();
                            break;

                        case '2':
                            Console.WriteLine();
                            DeleteByFirstAndLastName();
                            break;

                        case '0':

                            Console.WriteLine();
                            Console.WriteLine("Returning to the previous menu...........\n");
                            return;

                        default:

                            Console.WriteLine();
                            Console.WriteLine("INVALID OPTION! You need to choose between (0-2).\n\n");
                            break;
                    }
                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine("INVALID INPUT! You need to enter a single character (0-2).\n\n");
                }
            }
        }

        static int CountNonReturnedBooks(int memberId)
        {
            var counterOfNonReturnedBooks = 0;

            foreach (var id in libraryMembers[memberId].loansOfMember)
            {
                if (loans[id].returnDate == null)
                {
                    counterOfNonReturnedBooks++;
                }
            }

            return counterOfNonReturnedBooks;

        }

        static void DeleteById()
        {

            while (true)
            {

                Console.WriteLine();
                Console.WriteLine("Enter the library member's ID to delete them: ");

                if (int.TryParse(Console.ReadLine(), out var memberId))
                {
                    if (libraryMembers.ContainsKey(memberId))
                    {
                        if (CountNonReturnedBooks(memberId) > 0)
                        {
                            Console.WriteLine("You can't delete a library member who has non returned book.");
                            return;
                        }

                        Console.Write("Are you sure you want to delete this member? (yes/no): ");
                        if (Console.ReadLine()?.Trim().ToLower() != "yes")
                        {
                            Console.WriteLine("Deletion cancelled.");
                            return;
                        }

                        libraryMembers.Remove(memberId);
                        Console.WriteLine("Member has been successfully deleted!");
                        return;
                    }
                }


                Console.WriteLine();
                Console.WriteLine("INVALID INPUT! You need to enter an existing ID number.\n\n");


            }

        }

        static void DeleteByFirstAndLastName()
        {
            string firstName = string.Empty, lastName = string.Empty;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter the library member's first and last name to delete them: ");

                CreateFirstNameLastName(ref firstName, ref lastName);

                var member = libraryMembers.FirstOrDefault(member =>
                                                        member.Value.firstName == firstName &&
                                                        member.Value.lastName == lastName);

                if (member.Value.firstName != firstName && member.Value.lastName != lastName)
                {
                    Console.WriteLine("Non existing member!");
                    continue;
                }
                if (CountNonReturnedBooks(member.Key) > 0)
                {
                    Console.WriteLine("You can't delete a library member who has non returned book.");
                    return;
                }


                Console.Write("Are you sure you want to delete this member? (yes/no): ");
                if (Console.ReadLine()?.Trim().ToLower() != "yes")
                {
                    Console.WriteLine("Deletion cancelled.");
                    return;
                }

                libraryMembers.Remove(member.Key);
                Console.WriteLine("Member has been successfully deleted!");
                return;



            }
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
            if (libraryMembers.Count == 0)
            {
                Console.WriteLine("There are no library members.\n");
                return;
            }

            decimal maxTotalLateFee = 0;
            var memberIdWithTotalMaxLateFee = 0;

            foreach (var member in libraryMembers)
            {
                var totalLateFee = CalculateTotalLateFee(member.Key);
                if (totalLateFee > maxTotalLateFee)
                {
                    maxTotalLateFee = totalLateFee;
                    memberIdWithTotalMaxLateFee = member.Key;
                }

            }

            if (memberIdWithTotalMaxLateFee == 0)
            {
                Console.WriteLine("Nobody has late fees.\n");
                return;
            }

            var memberWithTopFee = libraryMembers[memberIdWithTotalMaxLateFee];
            Console.WriteLine("Library member with the highest total late fees:");
            Console.WriteLine($"[{memberIdWithTotalMaxLateFee}] {memberWithTopFee.lastName}, {memberWithTopFee.firstName} " +
                              $"({CalculateAge(memberWithTopFee.dateOfBirth)}) - Number of loans:{memberWithTopFee.loansOfMember.Count}\n");

        }
        static decimal CalculateTotalLateFee(int memberId)
        {
            decimal totalLateFee = default;

            foreach (var loanId in libraryMembers[memberId].loansOfMember)
            {
                totalLateFee += loans[loanId].lateFee;
            }

            return totalLateFee;

        }

        static void ShowMemberWithMostLoans()
        {
            if (libraryMembers.Count == 0)
            {
                Console.WriteLine("There are no library members.\n");
                return;
            }

            var maxNumberOfLoans = libraryMembers.Values.Max(member => member.loansOfMember.Count);


            Console.WriteLine();
            Console.WriteLine("Library member(s) with most loans:\n");

            foreach (var member in libraryMembers)
            {
                if (member.Value.loansOfMember.Count == maxNumberOfLoans)
                {
                    Console.WriteLine($"[{member.Key}] {member.Value.lastName}, {member.Value.firstName} " +
                                      $"({CalculateAge(member.Value.dateOfBirth)}) - Number of loans:{member.Value.loansOfMember.Count}\n");
                }
            }
        }
        static void ShowMostBorrowedBook()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            var titleCounts = new Dictionary<string, int>();

            foreach (var loan in loans.Values)
            {
                if (titleCounts.ContainsKey(loan.bookTitle))
                {
                    titleCounts[loan.bookTitle]++;
                }

                else
                {
                    titleCounts[loan.bookTitle] = 1;
                }    
            }

            var maxCount = titleCounts.Values.Max();

            Console.WriteLine();
            Console.WriteLine("Most borrowed book title(s):");

            foreach(var title in titleCounts)
            {
                if(title.Value == maxCount)
                {
                    Console.WriteLine($"{title.Key} - borrowed {title.Value} time(s)");                
                }
            }
        }
        static void ShowTotalLateFees()
        {
            if (libraryMembers.Count == 0)
            {
                Console.WriteLine("There are no library members.\n");
                return;
            }

            decimal totalLateFees = 0;

            foreach (var member in libraryMembers)
            {
                totalLateFees += CalculateTotalLateFee(member.Key);
            }

            if (totalLateFees == 0)
            {
                Console.WriteLine("Nobody has late fees.\n");
                return;

            }

            Console.WriteLine();
            Console.WriteLine($"Total late fees of all library members: {totalLateFees:F2}");
        }
        static void PrintMonthlyLoanChart()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            var monthlyCounts = new Dictionary<DateOnly, int>();

            foreach (var loan in loans.Values)
            {
                var month = new DateOnly (loan.loanDate.Year, loan.loanDate.Month, 1 );

                if(monthlyCounts.ContainsKey(month))
                {
                    monthlyCounts[month]++;
                }

                else
                {
                    monthlyCounts[month] = 1;
                }

            }

            Console.WriteLine();

            foreach( var date in monthlyCounts.OrderBy(date => date.Key))
            {
                Console.WriteLine($"{date.Key.Year}-{date.Key.Month} {date.Value} {new string('*', date.Value)}\n");
            }




        }

    }    
}
