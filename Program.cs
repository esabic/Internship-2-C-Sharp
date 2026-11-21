using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Dynamic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryManagementSystem
{
    internal class Program
    {

        const int LoanPeriodInDays = 21;
        const decimal LateFeePerDay = 0.50M;

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

            libraryMembers.Add(CreateDictionaryId(libraryMembers), (newFirstName, newLastName, newDateOfBirth, new List<int> { }));


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

        static int CreateDictionaryId<T>(Dictionary<int, T> Dictionary)
        {
            return Dictionary.Count == 0 ? 1 : Dictionary.Keys.Max() + 1;
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

                        foreach(var loanId in libraryMembers[memberId].loansOfMember)
                        {
                            loans.Remove(loanId);
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

                if (member.Value.firstName != firstName || member.Value.lastName != lastName)
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

                foreach (var loanId in libraryMembers[member.Key].loansOfMember)
                {
                    loans.Remove(loanId);
                }

                libraryMembers.Remove(member.Key);
                Console.WriteLine("Member has been successfully deleted!");
                return;



            }
        }

        static void EditMember()
        {

            var id = WriteTheId(libraryMembers);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Change the first name of the member\n" +
                                  "2 - Change the last name of the member\n" +
                                  "3 - Change date of birth of the member\n" +
                                  "0 - Return to the previous menu\n\n");

                Console.Write("Option: ");
                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            ChangeMembersFirstName(id);
                            break;

                        case '2':
                            Console.WriteLine();
                            ChangeMembersLastName(id);
                            break;

                        case '3':
                            Console.WriteLine();
                            ChangeMembersDateOfBirth(id);
                            break;

                        case '0':

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

        static void ChangeMembersFirstName(int membersId)
        {
            while (true)
            {

                Console.WriteLine();
                Console.Write("Write new first name of the member: ");

                string newFirstName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newFirstName))
                {

                        Console.Write($"Are you sure you want to change members first name in {newFirstName}? (yes/no): ");
                        if (Console.ReadLine()?.Trim().ToLower() != "yes")
                        {
                            Console.WriteLine("\nEditing cancelled.");
                            return;
                        }

                        var libraryMember = libraryMembers[membersId];
                        libraryMember.firstName = newFirstName;
                        libraryMembers[membersId] = libraryMember;

                        Console.WriteLine($"Members first name has been successfully changed in {newFirstName}!");
                        return;
                    
                }


                Console.WriteLine();
                Console.WriteLine("INVALID INPUT! Input cannot be empty.\n\n");


            }
        }

        static void ChangeMembersLastName(int membersId)
        {
            while (true)
            {

                Console.WriteLine();
                Console.Write("Write new last name of the member: ");

                string newLastName = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(newLastName))
                {

                    Console.Write($"Are you sure you want to change members last name in {newLastName}? (yes/no): ");
                    if (Console.ReadLine()?.Trim().ToLower() != "yes")
                    {
                        Console.WriteLine("\nEditing cancelled.");
                        return;
                    }

                    var libraryMember = libraryMembers[membersId];
                    libraryMember.lastName = newLastName;
                    libraryMembers[membersId] = libraryMember;

                    Console.WriteLine($"Members last name has been successfully changed in {newLastName}!");
                    return;

                }


                Console.WriteLine();
                Console.WriteLine("INVALID INPUT! Input cannot be empty.\n\n");


            }
        }

        static void ChangeMembersDateOfBirth(int membersId)
        {
            while (true)
            {

                Console.WriteLine();
                Console.Write("Write new date of birth of the member in format (YYYY-MM-DD): ");

                if (DateOnly.TryParseExact(Console.ReadLine(),"yyyy-MM-dd", out DateOnly newDateOfBirth ) && newDateOfBirth < DateOnly.FromDateTime(DateTime.Today))
                {

                    Console.Write($"Are you sure you want to change members date of birth {newDateOfBirth}? (yes/no): ");
                    if (Console.ReadLine()?.Trim().ToLower() != "yes")
                    {
                        Console.WriteLine("\nEditing cancelled.");
                        return;
                    }

                    var libraryMember = libraryMembers[membersId];
                    libraryMember.dateOfBirth = newDateOfBirth;
                    libraryMembers[membersId] = libraryMember;

                    Console.WriteLine($"Members date of birth has been successfully changed in {newDateOfBirth}!");
                    return;

                }


                Console.WriteLine();
                Console.WriteLine("INVALID INPUT! You need to enter new date of birth in the past and in format (YYYY-MM-DD).\n\n");


            }
        }


        static int WriteTheId<T>(Dictionary<int, T> dictionary)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Write the Id: ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (dictionary.ContainsKey(id))
                    {

                        return id;
                    }

                    Console.WriteLine("Written id does not exist. Write an existing id.\n");
                }
                else
                {
                    Console.WriteLine("Invalid input! You need to write a number of id.\n");
                }
            }
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
                            Console.WriteLine("Returning to the previous menu...........\n");
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
            if(libraryMembers.Count == 0)
            {
                Console.WriteLine("There are no library members.\n");
                return;
            }

            var membersId = WriteTheId(libraryMembers);

            string bookTitle = default,
                   author = default;

            CreateBookTitle(ref bookTitle);
            CreateAuthor(ref author);

            DateOnly dateOfLoan = new DateOnly();

            CreateLoanDate(ref dateOfLoan);

            var loanId = CreateDictionaryId(loans);

            loans.Add(loanId, (bookTitle, author, dateOfLoan, null, 0, 0M));
            libraryMembers[membersId].loansOfMember.Add(loanId);
            

            Console.WriteLine("\nNew loan has been successfully created!");
            Console.WriteLine($"Return the book by: {dateOfLoan.AddDays(LoanPeriodInDays):yyyy-MM-dd}\n\n");

        }

        static void CreateBookTitle(ref string bookTitle)
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Write a book title:");

                bookTitle = Console.ReadLine().Trim();

                if (!string.IsNullOrWhiteSpace(bookTitle))
                {
                    return;

                }
                Console.WriteLine();
                Console.WriteLine("Your input can not be empty. Write a book title again.\n");

            }
        }

        static void CreateAuthor(ref string author)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Write a author of the book:");

                author = Console.ReadLine().Trim();

                if (!string.IsNullOrWhiteSpace(author))
                {
                    return;

                }
                Console.WriteLine();
                Console.WriteLine("Your input can not be empty. Write an author again.\n");

            }
        }

        static void CreateLoanDate(ref DateOnly dateOfLoan)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Write a date of loan in format(YYYY-MM-DD): ");

                var input = Console.ReadLine();

                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", out dateOfLoan) && dateOfLoan <= DateOnly.FromDateTime(DateTime.Today))
                {
                    return;
                }

                else
                {
                    Console.WriteLine("Invalid input! You need to write a date loan in format(YYYY-MM-DD) and not in the future.");
                }

            }
        }


        static void ReturnBook()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            int loanId = WriteTheId(loans);

            if (loans[loanId].returnDate!=null)
            {
                Console.WriteLine($"Book with id:[{loanId}] is already returned.\n");
                return;
            }

            var loan = loans[loanId];
            var returnDate = ReadReturnDate(loan.loanDate);
            var overdueDays = CalculateOverdueDays(loanId, returnDate);
            var lateFee = CalculateLateFee(overdueDays);

            loans[loanId] = (loan.bookTitle, loan.author, loan.loanDate, returnDate, overdueDays, lateFee);

            Console.WriteLine();
            Console.WriteLine($"Book returned. Overdue days: {overdueDays}, late fee: {lateFee:F2} \n");
        }

        static DateOnly ReadReturnDate(DateOnly loanDatae)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Write the return date (YYYY-MM-DD): ");

                if(DateOnly.TryParseExact(Console.ReadLine(), "yyyy-MM-dd" , out var returnDate) && returnDate >= loanDatae)
                {
                    return returnDate;
                }

                Console.WriteLine("Invalid input! Date must be in format (YYYY-MM-DD) and not before loan date.\n");
            }
        }
        static int CalculateOverdueDays(int loanId, DateOnly? returnDate)
        {
            var dueDate = loans[loanId].loanDate.AddDays(LoanPeriodInDays);
            var endDate = returnDate ?? DateOnly.FromDateTime(DateTime.Today);
            var overdueDays = endDate.DayNumber - dueDate.DayNumber;

            return overdueDays > 0 ? overdueDays : 0;

        }

        static decimal CalculateLateFee(int overdueDays)
        {
            return overdueDays * LateFeePerDay;
        }

       
        static void DeleteLoan()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Delete by Id\n" +
                                  "2 - Delete all returned loans of a member\n" +
                                  "3 - Delete all loans with late fee above an amount\n" +
                                  "0 - Return to the previous menu\n\n");

                Console.Write("Option: ");

                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            DeleteLoanById();
                            break;

                        case '2':
                            Console.WriteLine();
                            DeleteReturnedLoansOfMember();
                            break;

                        case '3':
                            Console.WriteLine();
                            DeleteLoansWithLateFeeAbove();
                            break;

                        case '0':
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

        static void DeleteLoanById()
        {
            if(loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            while (true)
            {

                Console.WriteLine();
                Console.WriteLine("Enter the loan's ID to delete it: ");

                if (int.TryParse(Console.ReadLine(), out var loansId))
                {
                    if (loans.ContainsKey(loansId))
                    {
                        if (loans[loansId].returnDate == null)
                        {
                            Console.WriteLine("You can't delete a loan with not returned book.");
                            return;
                        }

                        Console.Write("Are you sure you want to delete this loan? (yes/no): ");
                        if (Console.ReadLine()?.Trim().ToLower() != "yes")
                        {
                            Console.WriteLine("Deletion cancelled.");
                            return;
                        }

                        RemoveLoan(loansId);
                        
                        Console.WriteLine("Loan has been successfully deleted!");
                        return;
                    }
                }


                Console.WriteLine();
                Console.WriteLine("INVALID INPUT! You need to enter an existing ID number.\n\n");


            }
        }

        static void RemoveLoan(int loanId)
        {
            foreach (var member in libraryMembers)
            {
                member.Value.loansOfMember.Remove(loanId);
            }

            loans.Remove(loanId);

        }
        static void DeleteReturnedLoansOfMember()
        {
            if (libraryMembers.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            int membersId = WriteTheId(libraryMembers);

            var returnedLoansIds = new List<int>();

            foreach (var loanId in libraryMembers[membersId].loansOfMember)
            {
                if (loans[loanId].returnDate!=null)
                {
                    returnedLoansIds.Add(loanId);
                }
            }

            if(returnedLoansIds.Count == 0)
            {
                Console.WriteLine("This member has no retured loans.\n");
                return;
            }

            Console.Write("Are you sure you want to delete this loan? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() != "yes")
            {
                Console.WriteLine("Deletion cancelled.");
                return;
            }

            foreach (var loanId in returnedLoansIds)
            {
                RemoveLoan(loanId);
            }

            Console.WriteLine($"{returnedLoansIds.Count} loan(s) successfully deleted!\n");

        }

        static void DeleteLoansWithLateFeeAbove()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            var amount = ReadLateFeeAmount();

            var returnedLoansIds = new List<int>();

            foreach (var loan in loans)
            {
                
                    if (loan.Value.returnDate!=null && loan.Value.lateFee > amount)
                {
                    returnedLoansIds.Add(loan.Key);
                }
            }

            if (returnedLoansIds.Count == 0)
            {
                Console.WriteLine($"There are no returned loans with late fee grather than {amount}.\n");
                return;
            }

            Console.Write("Are you sure you want to delete this loan? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() != "yes")
            {
                Console.WriteLine("Deletion cancelled.");
                return;
            }

            foreach (var loanId in returnedLoansIds)
            {
                RemoveLoan(loanId);
            }

            Console.WriteLine($"{returnedLoansIds.Count} loan(s) successfully deleted!\n");
        }

        static decimal ReadLateFeeAmount()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter the late fee amount in euros: ");

                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0)
                {
                    return value;
                }

                Console.WriteLine("Invalid input! You need to enter a non-negative number.\n");
            }
        }

        static void EditLoan()
        {
            if(loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            int loanId = WriteTheId(loans);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Change the book title\n" +
                                  "2 - Change the author\n" +
                                  "3 - Change date of loan\n" +
                                  "4 - Change date of return\n"+
                                  "0 - Return to the previous menu\n\n");

                Console.Write("Option: ");
                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            ChangeBookTitle(loanId);
                            break;

                        case '2':
                            Console.WriteLine();
                            ChangeAuthor(loanId);
                            break;

                        case '3':
                            Console.WriteLine();
                            ChangeLoanDate(loanId);
                            break;

                        case '4':
                            Console.WriteLine();
                            ChangeReturnDate(loanId);
                            break;

                        case '0':

                            Console.WriteLine();
                            Console.WriteLine("Returning to the previous menu...........\n");
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

        static void ChangeBookTitle(int loanId)
        {
            string newBookTitle = default;

            CreateBookTitle(ref newBookTitle);

            Console.Write($"Are you sure you want to change book title in {newBookTitle}? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() != "yes")
            {
                Console.WriteLine("\nEditing cancelled.");
                return;
            }

            var temporaryLoan = loans[loanId];
            temporaryLoan.bookTitle = newBookTitle;
            loans[loanId] = temporaryLoan;

            Console.WriteLine($"Book title has been successfully changed in {newBookTitle}!");
            

        }

        static void ChangeAuthor(int loanId)
        {
           string newAuthor = default;

            CreateAuthor(ref newAuthor);

            Console.Write($"Are you sure you want to change author in {newAuthor}? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() != "yes")
            {
                Console.WriteLine("\nEditing cancelled.");
                return;
            }

            var temporaryLoan = loans[loanId];
            temporaryLoan.author = newAuthor;
            loans[loanId] = temporaryLoan;

            Console.WriteLine($"Author has been successfully changed in {newAuthor}!");

        }

        static void ChangeLoanDate(int loanId)
        {
            DateOnly newLoanDate = new DateOnly();

            CreateLoanDate(ref newLoanDate);

            var returnDate = loans[loanId].returnDate;

            if (returnDate != null && newLoanDate > returnDate)
            {
                Console.WriteLine("Loan date cannot be after the return date\n");
                return;
            }

            Console.Write($"Are you sure you want to change loan date in {newLoanDate}? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() != "yes")
            {
                Console.WriteLine("\nEditing cancelled.");
                return;
            }

            var temporaryLoan = loans[loanId];
            temporaryLoan.loanDate = newLoanDate;
            loans[loanId] = temporaryLoan;

            temporaryLoan.overdueDays = CalculateOverdueDays(loanId, loans[loanId].returnDate);
            temporaryLoan.lateFee = CalculateLateFee(temporaryLoan.overdueDays);
            loans[loanId] = temporaryLoan;

            Console.WriteLine($"Loan date has been successfully changed in {newLoanDate}!");

        }

        static void ChangeReturnDate(int loanId)
        {
            DateOnly newReturnDate = new DateOnly();

            newReturnDate = ReadReturnDate(loans[loanId].loanDate);

            Console.Write($"Are you sure you want to change return date in {newReturnDate}? (yes/no): ");
            if (Console.ReadLine()?.Trim().ToLower() != "yes")
            {
                Console.WriteLine("\nEditing cancelled.");
                return;
            }

            var temporaryLoan = loans[loanId];
            temporaryLoan.returnDate = newReturnDate;
            temporaryLoan.overdueDays = CalculateOverdueDays(loanId, temporaryLoan.returnDate);
            temporaryLoan.lateFee = CalculateLateFee(temporaryLoan.overdueDays);
            loans[loanId] = temporaryLoan;

            Console.WriteLine($"Return date has been successfully changed in {newReturnDate}!");
        }


        static void PrintAllLoans()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - All loans in saved order\n" +
                                  "2 - Sort loans by loan date\n" +
                                  "3 - Sort loans by late fee\n" +
                                  "4 - Display currently unreturned loans\n" +
                                  "5 - Search loans by title or author\n" +
                                  "6 - Group loans by borrowing month\n" +
                                  "0 - Return to the main menu\n\n");

                Console.Write("Option: ");

                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine();
                            DisplayAllLoans();
                            break;

                        case '2':
                            Console.WriteLine();
                            SortLoansByDate();
                            break;

                        case '3':
                            Console.WriteLine();
                            SortLoansByLateFee();
                            break;

                        case '4':
                            Console.WriteLine();
                            DisplayUnreturnedLoans();
                            break;

                        case '5':
                            Console.WriteLine();
                            SearchLoans();
                            break;

                        case '6':
                            Console.WriteLine();
                            GroupLoansByMonth();
                            break;

                        case '0':
                            Console.WriteLine();
                            Console.WriteLine("Returning to the previous menu...........\n");
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

        static void DisplayAllLoans()
        {
            if(loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return ;
            }

            Console.WriteLine();

            foreach(var loan in loans)
            {
                FormatLoanOutput(loan);
            }



        }

        static void SortLoansByDate()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            var typeOfSorting = ChooseTypeOfSorting();

            if(typeOfSorting == 1)
            {
                foreach(var loan in loans.OrderBy(loan => loan.Value.loanDate))
                {
                    FormatLoanOutput(loan);
                }
            }

            else
            {
                foreach (var loan in loans.OrderByDescending(loan => loan.Value.loanDate))
                {
                    FormatLoanOutput(loan);
                }
            }

        }

        static void SortLoansByLateFee()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            var typeOfSorting = ChooseTypeOfSorting();

            if (typeOfSorting == 1)
            {
                foreach (var loan in loans.OrderBy(loan => loan.Value.lateFee))
                {
                    FormatLoanOutput(loan);
                }
            }

            else
            {
                foreach (var loan in loans.OrderByDescending(loan => loan.Value.lateFee))
                {
                    FormatLoanOutput(loan);
                }
            }
        }

        static void FormatLoanOutput(KeyValuePair<int, (string bookTitle, string author, DateOnly loanDate, DateOnly? returnDate, int overdueDays, decimal lateFee)> loan)
        {
            var returnedText = loan.Value.returnDate == null ? "not returned" : $"returned: {loan.Value.returnDate:yyyy-MM-dd}";

            Console.WriteLine($"[{loan.Key}] \"{loan.Value.bookTitle}\" - {loan.Value.author} | " +
                              $"loaned: {loan.Value.loanDate:yyyy-MM-dd} | {returnedText} | " +
                              $"overdue days: {loan.Value.overdueDays} days | late fee: {loan.Value.lateFee}\n");
        }
        static int ChooseTypeOfSorting()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Choose type of sorting:\n\n");
                Console.WriteLine("1 - Ascending order\n" +
                                  "2 - Descending order\n");
                                  

                Console.Write("Option: ");

                if (char.TryParse(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case '1':

                            return 1;

                        case '2':

                            return 2;

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

        static void DisplayUnreturnedLoans()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            if (loans.Values.Any(loan => loan.returnDate == null))
            {
                Console.WriteLine();
                Console.WriteLine("Unreturned loan(s):\n");

                foreach (var loan in loans)
                {
                    if (loan.Value.returnDate == null)
                    {

                        var membersKey = libraryMembers.FirstOrDefault(member => member.Value.loansOfMember.Contains(loan.Key)).Key;

                        Console.WriteLine($"{libraryMembers[membersKey].firstName} {libraryMembers[membersKey].lastName}: [{loan.Key}] \"{loan.Value.bookTitle}\" - {loan.Value.author} | " +
                                          $"loaned: {loan.Value.loanDate:yyyy-MM-dd} | not returned | " +
                                          $"overdue days: {loan.Value.overdueDays} days | late fee: {loan.Value.lateFee}\n");
                    }
                }

                return;
                
            }

            Console.WriteLine("All loans are returned.\n");
           
        }

        static void SearchLoans()
        {
            if(loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("Write part of the book title or author: ");

                var input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var foundLoans = 0;

                    foreach(var loan in loans)
                    {
                        if(loan.Value.bookTitle.Contains(input, StringComparison.CurrentCultureIgnoreCase)||loan.Value.author.Contains(input, StringComparison.CurrentCultureIgnoreCase))
                        {
                            FormatLoanOutput(loan);
                            foundLoans++;
                        }
                    }

                    if(foundLoans == 0)
                    {
                        Console.WriteLine("No loans match you search.\n");
                    }

                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Invalid input! Input cannot be empty.\n");
            }
                    

        }

        static void GroupLoansByMonth()
        {
            if (loans.Count == 0)
            {
                Console.WriteLine("There are no loans.\n");
                return;
            }

            var dates = new List<DateOnly>();

            foreach (var loan in loans.Values)
            {
                var date = new DateOnly(loan.loanDate.Year, loan.loanDate.Month, 1);

                if(!dates.Contains(date))
                {
                    dates.Add(date);
                }
            }

            dates.Sort();

            foreach (var date in dates)
            {
                var count = 0;
                decimal totalFee = 0;

                Console.WriteLine();
                Console.WriteLine($"{date:yyyy-MM}\n\n");

                foreach (var loan in loans)
                {
                    if(loan.Value.loanDate.Year==date.Year && loan.Value.loanDate.Month==date.Month)
                    {
                        FormatLoanOutput(loan);
                        count++;
                        totalFee += loan.Value.lateFee;
                    }
                }

                Console.WriteLine($"Number of loans: {count} | Total late fees: {totalFee:F2}\n");
            }
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
            Console.WriteLine($"\n[{memberIdWithTotalMaxLateFee}] {memberWithTopFee.lastName}, {memberWithTopFee.firstName} " +
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
