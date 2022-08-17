using Persistence;
using BL;

Console.InputEncoding = System.Text.Encoding.Unicode;
Console.OutputEncoding = System.Text.Encoding.Unicode;

Admin admin = new Admin();
Admin _admin = new Admin();
AdminBL adminBl = new AdminBL();

Customer customer = new Customer();
Customer _customer = new Customer();
CustomerBL customerBl = new CustomerBL();
List<Customer> listCustomer = new List<Customer>();

// Console.Write("Input username: ");
// admin.setUserName(Console.ReadLine() ?? "");
// Console.Write("Input password: ");
// admin.setPassword(Console.ReadLine() ?? "");
// _admin = adminBl.Login(admin);

// try
// {
//     if (admin.getUserName() == _admin.getUserName() && admin.getPassword() == _admin.getPassword())
//     {
//         Console.WriteLine("Thành công");
//     }
// }
// catch
// {
//     Console.WriteLine("Thất bại");
// }

// Console.Write("Input id to search customer: ");
// int id;
// int.TryParse(Console.ReadLine(), out id);
// customer.setCustomerId(id);

// _customer = customerBl.GetCustomerById(customer.getCustomerId());
// Console.WriteLine($"{_customer}");

// Console.Write("Input name to search customer: ");
// customer.setCustomerName(Console.ReadLine() ?? "");

// _customer = customerBl.GetCustomerByName(customer.getCustomerName() ?? "");
// Console.WriteLine($"{_customer}");

listCustomer = customerBl.GetAllCustomer();
foreach (var item in listCustomer)
{
    Console.WriteLine(item);
}