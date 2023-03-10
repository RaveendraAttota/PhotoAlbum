
int choice = 0;

while (choice != 99)
{
    Console.WriteLine("Photo Album API");
    Console.WriteLine("Enter 1 to get the list");
    Console.WriteLine("Enter 2to get by user id");
    Console.Write("Enter 3 to exit");
    choice = Convert.ToInt32(Console.ReadLine());
    Console.Clear();
    if (choice == 1)
    { 
        //call the API
    }
    else if (choice == 2)
    {
        //call api with userId
    }
}

Console.Clear();