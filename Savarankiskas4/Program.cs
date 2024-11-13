using Savarankiskas4.Core.Contracts;
using Savarankiskas4.Core.Models;
using Savarankiskas4.Core.Repo;
using Savarankiskas4.Core.Services;
using System;

namespace Savarankiskas4;

public class Program
{
    public static void Main(string[] args)
    {
        IUserRepository userRepository = new UserRepository("Server=localhost;Database=Savarankiskas4;Trusted_Connection=True;TrustServerCertificate=true;");
        IUserService userService = new UserService(userRepository);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Register new user");
            Console.WriteLine("2. Update user information");
            Console.WriteLine("3. Change user password");
            Console.WriteLine("4. Set user account status");
            Console.WriteLine("5. Show all users");
            Console.WriteLine("6. Search user by ID");
            Console.WriteLine("7. List users by role (Non-functional)");
            Console.WriteLine("8. Remove user");
            Console.WriteLine("9. Exit");
            Console.WriteLine();
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    string createUsername = "";
                    bool repeat = true;
                    while (repeat)
                    {
                        repeat = false;
                        Console.WriteLine("Enter username:");
                        createUsername = Console.ReadLine();
                        List<User> allUsers = userService.GetAllUsers();
                        foreach (User a in allUsers)
                        {
                            if (createUsername == a.Username)
                            {
                                Console.WriteLine("Username already in use. Please try again.");
                                Console.WriteLine();
                                repeat = true;
                            }
                        }
                    }
                    Console.WriteLine("Enter password:");
                    string createPassword = Console.ReadLine();

                    while (true)
                    {
                        Console.WriteLine("Do you wish to create an administrator, or a standard account?");
                        Console.WriteLine("1. Administrator");
                        Console.WriteLine("2. Standard User");
                        int accountType = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        if (accountType == 1)
                        {
                            Admin newUser = new Admin
                            {
                                Username = createUsername,
                                Password = createPassword,
                            };
                            userService.RegisterUser(newUser);
                            Console.WriteLine("User was successfully registered.");
                            break;
                        }
                        else if (accountType == 2)
                        {
                            StandardUser newUser = new StandardUser
                            {
                                Username = createUsername,
                                Password = createPassword,
                            };
                            userService.RegisterUser(newUser);
                            Console.WriteLine("User was successfully registered.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine();
                    continue;

                case 2:
                    while (true)
                    {
                        Console.WriteLine("Enter ID of the user you wish to update:");
                        int updateId = int.Parse(Console.ReadLine());
                        User updatedUser = userService.GetUser(updateId);
                        if (updatedUser == null)
                        {
                            Console.WriteLine("User not found, try again");
                            Console.WriteLine();
                            continue;
                        }
                        else
                        {
                            string newUsername = "";
                            bool repeat2 = true;
                            while (repeat2)
                            {
                                repeat2 = false;
                                Console.WriteLine("Enter username:");
                                newUsername = Console.ReadLine();
                                List<User> allUsers2 = userService.GetAllUsers();
                                foreach (User a in allUsers2)
                                {
                                    if (newUsername == a.Username)
                                    {
                                        Console.WriteLine("Username already in use. Please try again.");
                                        Console.WriteLine();
                                        repeat2 = true;
                                    }
                                }
                            }
                            Console.WriteLine("Enter new Password:");
                            string newPassword = Console.ReadLine();

                            while (true)
                            {
                                Console.WriteLine("Set account status (active/inactive):");
                                string newStatus = Console.ReadLine();
                                Console.WriteLine();
                                if (newStatus == "active")
                                {
                                    updatedUser.IsActive = true;
                                    break;
                                }
                                else if (newStatus == "inactive")
                                {
                                    updatedUser.IsActive = false;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice, please try again.");
                                }
                                Console.WriteLine();
                            }

                            updatedUser.Username = newUsername;
                            updatedUser.Password = newPassword;

                            userService.UpdateUser(updatedUser);
                            Console.WriteLine("User information updated");
                            Console.WriteLine();
                        }
                        break;
                    }
                    continue;

                case 3:
                    while (true)
                    {
                        Console.WriteLine("Enter ID of the user who's password you wish to change:");
                        int passwordId = int.Parse(Console.ReadLine());
                        User passwordUser = userService.GetUser(passwordId);
                        if (passwordUser == null)
                        {
                            Console.WriteLine("User not found, try again");
                            Console.WriteLine();
                            continue;
                        }
                        else
                        {
                            string passwordChange = "";
                            while (true)
                            {
                                Console.WriteLine("Enter new password:");
                                passwordChange = Console.ReadLine();
                                Console.WriteLine();
                                if (passwordChange == passwordUser.Password)
                                {
                                    Console.WriteLine("User already has this password. Please type in a new one.");
                                    Console.WriteLine();
                                    continue;
                                }
                                break;
                            }
                            userService.UpdatePassword(passwordId, passwordChange);
                            Console.WriteLine("Password successfully changed");
                            Console.WriteLine();
                            break;
                        }
                    }
                    continue;

                case 4:
                    while (true)
                    {
                        Console.WriteLine("Enter ID of the user who's status you wish to change:");
                        int statusId = int.Parse(Console.ReadLine());
                        User statusUser = userService.GetUser(statusId);
                        Console.WriteLine("Set new user status (active/inactive):");
                        string statusChange = Console.ReadLine();
                        Console.WriteLine();
                        if (statusChange == "active")
                        {
                            userService.ActivateUser(statusId);
                            Console.WriteLine("User status has been set to \"Active\".");
                            break;
                        }
                        else if (statusChange == "inactive")
                        {
                            userService.DeactivateUser(statusId);
                            Console.WriteLine("User status has been set to \"Inactive\".");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    continue;

                case 5:
                    foreach(User a in userService.GetAllUsers())
                    {
                        Console.WriteLine(a);
                    }
                    Console.WriteLine();
                    continue;

                case 6:
                    Console.WriteLine("Enter user ID:");
                    int searchId = int.Parse(Console.ReadLine());
                    User searchUser = userService.GetUser(searchId);
                    Console.WriteLine();
                    if (searchUser == null)
                    {
                        Console.WriteLine("User not found.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(searchUser);
                        Console.WriteLine();
                    }
                    continue;

                case 7:
                    while (true)
                    {
                        Console.WriteLine("Select role:");
                        Console.WriteLine("1. Administrators");
                        Console.WriteLine("2. Standard Users");
                        int rolePick = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        if(rolePick != 1 && rolePick != 2)
                        {
                            Console.WriteLine("Invalid choice, please try again.");
                            Console.WriteLine();
                            continue;
                        }
                        foreach(User a in userService.GetAllUsers())
                        {
                            if(rolePick == 1 && a is Admin)
                            {
                                Console.WriteLine(a);
                            }
                            else if(rolePick == 2 && a is StandardUser)
                            {
                                Console.WriteLine(a);
                            }
                        }
                        Console.WriteLine();
                        break;
                    }
                    continue;

                case 8:
                    while (true)
                    {
                        Console.WriteLine("Enter ID of the user you wish to remove:");
                        int removalId = int.Parse(Console.ReadLine());
                        User removalUser = userService.GetUser(removalId);
                        Console.WriteLine();
                        if (removalUser == null)
                        {
                            Console.WriteLine("User not found, try again");
                            Console.WriteLine();
                            continue;
                        }
                        else
                        {
                            userService.RemoveUser(removalId);
                            Console.WriteLine("User has been successfully removed");
                            Console.WriteLine();
                            break;
                        }
                    }
                    continue;

                case 9:
                    Console.WriteLine();
                    Console.WriteLine("Bye-bye!");
                    Console.WriteLine();
                    return;
            }
        }
    }
}