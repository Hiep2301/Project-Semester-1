using Persistence;
using BL;

Admin admin = new Admin();
AdminBL adminBL = new AdminBL();
// User customer = new Customer("huy123", "huy123", "Huy", "Nguyen", "123456789", "Hà Nội");

// Console.WriteLine($"{customer}");

admin.setUserName("admin");
admin.setPassword("admin");
Admin _admin = adminBL.Login(admin);
Console.WriteLine(_admin.getUserName());
Console.WriteLine(_admin.getPassword());
// if (_admin.getUserName() == "admin" && _admin.getPassword() == "admin")
// {
//     Console.WriteLine("Thành công");
// }
// else
// {
//     Console.WriteLine("Thất bại");
// }