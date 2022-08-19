using Persistence;
using BL;

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;
int id;
char option;

Admin admin = new Admin();
Admin _admin = new Admin();
AdminBL adminBl = new AdminBL();

Customer customer = new Customer();
Customer _customer = new Customer();
CustomerBL customerBl = new CustomerBL();
List<Customer> listCustomer = new List<Customer>();

Book book = new Book();
Book _book = new Book();
BookBL bookBl = new BookBL();
List<Book> listBook = new List<Book>();

Category category = new Category();
Category _category = new Category();
CategoryBL categoryBl = new CategoryBL();

// ------------- tìm kiếm khách theo id
// Console.Write("Input id to search customer: ");
// int id;
// int.TryParse(Console.ReadLine(), out id);
// customer.setCustomerId(id);
// _customer = customerBl.GetCustomerById(customer.getCustomerId());
// Console.WriteLine($"{_customer}");

// ------------- tìm kiếm khách theo tên
// Console.Write("Input name to search customer: ");
// customer.setCustomerName(Console.ReadLine() ?? "");
// listCustomer = customerBl.GetCustomerByName(customer.getCustomerName() ?? "");
// foreach (var item in listCustomer)
// {
//     Console.WriteLine(item);
// }

// ------------- hiển thị tất cả khách hàng
// listCustomer = customerBl.GetAllCustomer();
// foreach (var item in listCustomer)
// {
//     Console.WriteLine(item);
// }

// ------------- hiển thị tất cả sách
// listBook = bookBl.GetAllBook();
// foreach (var item in listBook)
// {
//     Console.WriteLine(item);
// }

MainMenu();

void Line()
{
    Console.WriteLine("==============================");
}

void WaitForButton(string msg)
{
    Console.Write(msg);
    Console.ReadKey();
}

int Menu(string[] menu, string name)
{
    Console.Clear();
    Line();
    Console.WriteLine(name);
    Line();
    for (int i = 0; i < menu.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {menu[i]}");
    }
    Line();
    int choice;
    do
    {
        Console.Write("Your choice: ");
        int.TryParse(Console.ReadLine(), out choice);
    } while (choice < 1 || choice > menu.Length);
    return choice;
}

void MenuManageBook()
{
    string[] menu = { "Add Book", "Update book by id", "Delete book by id", "Exit" };
    string name = "Manage Books";
    int choice;
    do
    {
        choice = Menu(menu, name);
        switch (choice)
        {
            case 1:
                while (true)
                {
                    Book InputBook(Book book)
                    {
                        Console.Write("Input book id: ");
                        int.TryParse(Console.ReadLine(), out book.bookId);
                        Console.Write("Input category id: ");
                        int.TryParse(Console.ReadLine(), out book.categoryId.categoryId);
                        Console.Write("Input book name: ");
                        book.bookName = Console.ReadLine() ?? "";
                        Console.Write("Input book price: ");
                        decimal.TryParse(Console.ReadLine(), out book.bookPrice);
                        Console.Write("Input book description: ");
                        book.bookDescription = Console.ReadLine() ?? "";
                        Console.Write("Input author name: ");
                        book.authorname = Console.ReadLine() ?? "";
                        return book;
                    }
                    if (adminBl.InsertBook(InputBook(book)))
                    {
                        Console.WriteLine("Add success");
                    }
                    else
                    {
                        Console.WriteLine("Add failure");
                    }
                    Console.Write("Do you want to continue?(Y/N): ");
                    option = Convert.ToChar(Console.ReadLine() ?? "");
                    if (option == 'n' || option == 'N')
                    {
                        break;
                    }
                }
                WaitForButton("Press any key to continue");
                break;

            case 2:

                WaitForButton("Press any key to continue");
                break;

            case 3:
                Console.WriteLine("Input id to delete: ");
                int.TryParse(Console.ReadLine(), out id);
                if (adminBl.DeleteBookById(id))
                {
                    Console.WriteLine("delete success");
                }
                else
                {
                    Console.WriteLine("delete failure");
                }
                WaitForButton("Press any key to continue");
                break;

            default:
                break;
        }
    } while (choice != menu.Length);
}

void MenuStore()
{
    string[] menu = { "Search book by id", "Search book by name", "Create order", "Exit" };
    string name = "Manage Books";
    int choice;
    do
    {
        choice = Menu(menu, name);
        switch (choice)
        {
            case 1:
                Console.WriteLine("Input id to search book: ");
                int.TryParse(Console.ReadLine(), out id);
                bookBl.GetBookById(id);
                WaitForButton("Press any key to continue");
                break;

            case 2:
                Console.WriteLine("Input name to search book: ");
                book.bookName = Console.ReadLine() ?? "";
                listBook = bookBl.GetBookByName(book.bookName);
                foreach (var item in listBook)
                {
                    Console.WriteLine(item);
                }
                WaitForButton("Press any key to continue");
                break;

            case 3:

                WaitForButton("Press any key to continue");
                break;

            default:
                break;
        }
    } while (choice != menu.Length);
}

void MainMenu()
{
    string[] menu = { "Admin login", "Customer login", "Exit" };
    string name = "Bookstore Management System";
    int choice;
    do
    {
        choice = Menu(menu, name);
        switch (choice)
        {
            case 1:
                Console.Write("Input username: ");
                admin.userName = Console.ReadLine() ?? "";
                Console.Write("Input password: ");
                admin.password = Console.ReadLine() ?? "";
                _admin = adminBl.Login(admin);
                try
                {
                    if (admin.userName == _admin.userName && admin.password == _admin.password)
                    {
                        MenuManageBook();
                    }
                }
                catch
                {
                    Console.WriteLine("Login failed, please try again");
                }
                WaitForButton("Press any key to continue");
                break;

            case 2:
                Console.Write("Input username: ");
                customer.userName = Console.ReadLine() ?? "";
                Console.Write("Input password: ");
                customer.password = Console.ReadLine() ?? "";
                _customer = customerBl.Login(customer);
                try
                {
                    if (customer.userName == _customer.userName && customer.password == _customer.password)
                    {
                        MenuStore();
                    }
                }
                catch
                {
                    Console.WriteLine("Login failed, please try again");
                }
                WaitForButton("Press any key to continue");
                break;

            default:
                break;
        }
    } while (choice != menu.Length);
}

